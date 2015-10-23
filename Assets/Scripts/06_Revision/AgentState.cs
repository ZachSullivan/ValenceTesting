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

	bool hasFinished = true;

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
		Hungry
	}
	
	agentState aState;
	
	void Awake() {
		
		//Find the agent movement controller attached
		_agentPathfinder = GetComponent<AgentPathfinder>();
		
		//When an agent spawns, he should start by wandering
		aState = agentState.Wandering;
		
		//When an agent spawns, start updating his hunger level
		updateHunger();
	}

	void Start(){
		//StartCoroutine(AgentHunger());
		InvokeRepeating("AgentHunger", 1, 1);
	}

	
	void Update() {
		switch (aState) {
		case agentState.Wandering:
			Wander();
			aState = agentState.Wandering;
			break;
		case agentState.Hungry:

			if(!feeding){
				_agentController.hungry = true;
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
