using UnityEngine;
using System.Collections;

public class AutoAI : MonoBehaviour {

	//Speed at which an agent will rotate
	public int rotateSpeed = 10;
	
	//Speed at which an agent will move
	public int agentSpeed = 1;
	
	//Distance which agent will detect obsticals
	public int raycastDistance = 1;

	//Allow user to choose a wandering radius for the agent
	public float wanderRange = 3.0f;

	public bool wandering = true;

	private float agentRotation;

	public GameObject foodSource;

	public int hungerSearch = 50;
	
	//Keeps track if an agent has a current goal. ie if they are hungry their goal will be to go to the food source
	bool agentGoal = false;
	
	float startTime;
	float speed = 0.05f;


	int hungerValue = 100;
	int targetIndex;

	Vector3 destination;
	Vector3 startPosition;
	Vector3[] path;

	public enum State {
		Wander,
		Hunger
		//States go here
	}

	public State state;

	void Awake (){
		startTime = Time.deltaTime;

		
		state = State.Wander;

		StartCoroutine(FSM());
		InvokeRepeating("AgentHunger", 1, 1);
	}

	void AgentHunger() {
		
		if (hungerValue > 0){
			hungerValue--;
		}
		
		//Seek food source
		if (hungerValue <= hungerSearch) {
			//Vector3 destination = foodSource.transform.position;
			state = State.Hunger;
		}
		
		Debug.Log(hungerValue);
	}
	
	IEnumerator FSM() {

		while(true){

			StartCoroutine(DirectionChoice());
			
			if(!Physics.Raycast(transform.position, transform.forward, raycastDistance)){
	
				yield return StartCoroutine(state.ToString());

			}
			else {
				transform.Rotate(Vector3.up, agentRotation * rotateSpeed * Time.smoothDeltaTime);
			}


		}

	}

	IEnumerator Wander () {

		transform.Translate(Vector3.forward * agentSpeed * Time.smoothDeltaTime);

		yield return null;
	}


	IEnumerator Hunger () {

		PathRequestManager.RequestPath(transform.position, foodSource.transform.position, OnPathFound);

		yield return null;
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
		
	}

	IEnumerator FollowPath(){
		Vector3 currentWaypoint = path[0];
		
		while (true){
			if(transform.position == currentWaypoint){
				targetIndex++;
				if(targetIndex >= path.Length){
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
			
			yield return null;
		}
	}



	// Update is called once per frame
	/*void Update () {

		StartCoroutine(DirectionChoice());

		if(!Physics.Raycast(transform.position, transform.forward, raycastDistance)){
			if (wandering == true) {

				Wander();

				//InvokeRepeating("Wander", 1f, 5f);
			}
		}
		else {
			transform.Rotate(Vector3.up, agentRotation * rotateSpeed * Time.smoothDeltaTime);
		}
		
	}*/

	/*void Wander() {

		transform.Translate(Vector3.forward * agentSpeed * Time.smoothDeltaTime);
		
		//NewDestination(destination);
	}*/

	//Assign the agent a target to move towards
	public void NewDestination(Vector3 targetPosition) {

		float timeSinceStarted = 0f;

		while (true)
		{
			timeSinceStarted += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, targetPosition, timeSinceStarted);
			

		}
	}

	IEnumerator DirectionChoice (){
		agentRotation = Random.Range(-360.0f, 360.0f);

		yield return 0;
	}
}
