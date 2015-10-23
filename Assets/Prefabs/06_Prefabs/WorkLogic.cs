using UnityEngine;
using System.Collections;
using Pathfinding;

public class WorkLogic : MonoBehaviour {

	//Point agent will move to
	public Transform target;

	AIFollow aiFollow;

	public enum agentState
	{
		Wandering,
		Hungry,
		Working,
		Default
	}

	public agentState aState;

	void Start(){
		target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;

		aiFollow = GetComponent<AIFollow>();

		//When an agent spawns, he should start by wandering
		aState = agentState.Working;

	}

	void Update() {
		switch (aState) {
		case agentState.Wandering:

			break;
		case agentState.Hungry:
						
			break;
			
		case agentState.Working:
			
			aiFollow.target = target;
			//aState = agentState.Working;
			break;
		case agentState.Default:
			print("Default reached in AgentFSM Update");
			break;
		}
	}

	bool PointInsideSphere(Vector3 point, Vector3 center, float radius) {
		print ("Dis" + Vector3.Distance(point, center));
		print ("inside: " + (Vector3.Distance(point, center) < radius));
		return Vector3.Distance(point, center) < radius;
	}

	public void TargetReached(){

		if(aState == agentState.Working){


			if(PointInsideSphere(transform.position,GameObject.FindGameObjectWithTag ("WorkWaypoint").transform.position, 1.0f) != true){
				target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;
				aiFollow.target = target;
			}else{
				//target.position = new Vector3 (Random.Range (-2, 2), 0, Random.Range (-2, 2));
				int tempPosx = Random.Range(-2, 2);
				int tempPosz = Random.Range(-2, 2);
				target.position = new Vector3(transform.position.x + tempPosx,transform.position.y,transform.position.z + tempPosz);
			}

			aState = agentState.Working;
		}

		/*
		 * if State1 
		 * 	do this
		 * if State2
		 * 	do this
		 * if State3
		 * 	do this
		 * if State4
		 * 	do this
		 * 
		 */ 
	}
}
