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
	enum agentState
	{
		Wandering,
		Hungry,
		Working
	}
	
	agentState aState;
	
	void Awake() {
		
		//Find the agent movement controller attached
		_agentPathfinder = GetComponent<AgentPathfinder>();

		WorkWaypoint = GameObject.FindGameObjectWithTag ("WorkWaypoint");

		//When an agent spawns, he should start by wandering
		aState = agentState.Wandering;
		
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

			_agentController.target = WorkWaypoint.transform.position;

			if(hasFinished2 = false){
				Vector3 tempTarget = new Vector3 (Random.Range (-workRadius, workRadius), 0, Random.Range (-workRadius, workRadius));

				if (Vector3.Distance(tempTarget, WorkWaypoint.transform.position) < workRadius)
					_agentController.target = tempTarget;
					hasFinished2 = true;
			}
			break;
		default:
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
		if (hasFinished == true) {
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
