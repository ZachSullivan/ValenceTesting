using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class WorkLogic : MonoBehaviour {

    //Point agent will move to
    //public Transform target;

    //List of all work waypoints
    public List<GameObject> workWaypoints = new List<GameObject>();

    //Keep track of the current waypoint the agent moves to when working
    //Counter associated to workWaypoints list index
    int waypointIndex;

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
        //target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;

        aiFollow = GetComponent<AIFollow>();

        workWaypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WorkWaypoint"));

        waypointIndex = 0;

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
            if (waypointIndex == 0){
                aiFollow.target = workWaypoints[waypointIndex].transform;
                waypointIndex += 1;
            }
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


            /*if(PointInsideSphere(transform.position,GameObject.FindGameObjectWithTag ("WorkWaypoint").transform.position, 1.0f) != true){
				target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;
				aiFollow.target = target;
			}else{
				//target.position = new Vector3 (Random.Range (-2, 2), 0, Random.Range (-2, 2));
				int tempPosx = Random.Range(-2, 2);
				int tempPosz = Random.Range(-2, 2);
				target.position = new Vector3(transform.position.x + tempPosx,transform.position.y,transform.position.z + tempPosz);
			}*/

            /*if (waypointIndex <= workWaypoints.Count) {

                waypointIndex += 1;
                aiFollow.target = workWaypoints[waypointIndex].transform;

            }   else {
                waypointIndex = 0;
                aState = agentState.Working;
            }*/

            waypointIndex = Random.Range(0, workWaypoints.Count);
            aiFollow.target = workWaypoints[waypointIndex].transform;


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
