using UnityEngine;
using System.Collections;

public class AgentState : MonoBehaviour {
	
	GameObject foodSource;
	public AgentController _agentController;
	//Variable keeps track of an agents hunger level, this value represents a 0-100% 
	// where 100% represents starving and 0% is full
	public int hungerValue = 100;
	public bool feeding = false;
	
	public int hungerSearch = 90;

	GameObject WorkWaypoint;

	//The defining radius which an agent will move around within a building
	public int workRadius;

	bool hasFinished = true;
	bool hasFinished2 = false;
	/*void Awake () {
		feeding = false;
		foodSource = GameObject.FindWithTag("Foodsource");
		_agentController = gameObject.GetComponent<AgentController>();
		//Increase the agent's hunger level every 5 seconds
		
	}*/

	//Used to control the movement of the agent
	AgentPathfinder _agentPathfinder;
	
	//Create a series of possible states for the agent to be in
	public enum agentState
	{
		Wandering,
		Hungry,
		Working,
		Default
	}
	
	public agentState aState;
	
	void Awake() {
		
		//Find the agent movement controller attached
		_agentPathfinder = GetComponent<AgentPathfinder>();

		WorkWaypoint = GameObject.FindGameObjectWithTag ("WorkWaypoint");

		//When an agent spawns, he should start by wandering
		aState = agentState.Default;
		
		//When an agent spawns, start updating his hunger level
		updateHunger();
	}

	void Start(){
		//StartCoroutine(AgentHunger());
		InvokeRepeating("AgentHunger", 1, 1);
	}

	void OnGUI() {
		if (GUI.Button(new Rect(10,30, 100, 20),"Work")) {
			aState = agentState.Working;
		}
	}
	
	void Update() {
		switch (aState) {
		case agentState.Wandering:
			Wander();
			aState = agentState.Wandering;
			break;
		case agentState.Hungry:

			if(!feeding && hungerValue <= hungerSearch){
				_agentController.hungry = true;
			}



			break;

		case agentState.Working:

			if(hasFinished2 == false){
				_agentController.target = WorkWaypoint.transform.position;
				hasFinished2 = true;
				aState = agentState.Working;
			}

			if(transform.position == WorkWaypoint.transform.position)
				_agentController.OnTargetReached();
			//aState = agentState.Working;


			break;
		case agentState.Default:
			print("Default reached in AgentFSM Update");
			break;
		}
	}

	//IEnumerator AgentHunger() {
	void AgentHunger(){
		if(!feeding){
			//while(!feeding){
			if (hungerValue > 0)
			{
				hungerValue--;
				
				//yield return new WaitForSeconds(1.0f);
				
			}
			
			
			if (hungerValue <= hungerSearch)
			{
				aState = agentState.Hungry;
				//_agentController.hungry = true;
				//gameObject.GetComponent<TestMovement>().hungry = true;
			}
		}
		//}
		
		
		else{
			//while(feeding){
			if(hungerValue < 100){
				hungerValue++;
				//yield return new WaitForSeconds(1.0f);
			}
		}
		//}
		
		if (hungerValue == 100){
			
			feeding = false;
			//StartCoroutine(AgentHunger());
		}
		
		if (hungerValue == 0) {
			Destroy(this.gameObject);
		}
		
	}
	
	public void Feed(){
		feeding = true;
		
		//if(hungerValue < 100){
		_agentController.hungry = false;
		//hungerValue ++;
		
		//} 	
		

	}
	


	
	void Wander() {
		print("Currently Wandering");
		if (hasFinished == true && aState == agentState.Wandering) {
			_agentController.target = new Vector3 (Random.Range (-50, 50), 0, Random.Range (-50, 50));
			hasFinished = false;
		}
	}
	
	void SearchForFood() {
		print("Currently Hungry");
		_agentPathfinder.aState = AgentPathfinder.actionState.Hungry;
	}
	
	void updateHunger() {
		
	}
	
}
