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

	int hungerValue = 100;
	
	Vector3 destination;
	Vector3 startPosition;
	

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

		startPosition = this.transform.position;

		Vector3 destination = new Vector3 (foodSource.transform.position.x, 0.36f, foodSource.transform.position.z);

		float newPos = 0;
		float rate = 1.0f;

		newPos += Time.deltaTime * rate;

		transform.position = Vector3.MoveTowards(transform.position, destination, newPos);

		//transform.rotation = Quaternion.Lerp(transform.rotation, foodSource.transform.rotation, newPos);

		yield return null;
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
