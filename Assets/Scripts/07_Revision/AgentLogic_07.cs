using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class AgentLogic_07 : MonoBehaviour {

    //Point agent will move to
    //public Transform target;

    //List of all work waypoints
    public List<GameObject> workWaypoints = new List<GameObject>();

    //List of all waypoints agent can wander to
    public List<Vector3> wanderWaypoints = new List<Vector3>();

    //Dictates the max number of waypoints agent can wander to
    public int wanderListSize;

    //Keep track of the current waypoint the agent moves to when working
    //Counter associated to workWaypoints & wanderWaypoints list index
    int workWaypointIndex, wanderWaypointIndex;

    //Check if a target has been assigned, if yes wait, otherwise assign a new target
    bool assignedTarget;

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

        
        aiFollow = GetComponent<AIFollow>();

        workWaypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WorkWaypoint"));
        wanderWaypoints = new List<Vector3>();

        while (wanderWaypoints.Count < wanderListSize) {
            wanderWaypoints.Add(new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));
        }

        workWaypointIndex = 0;
        wanderWaypointIndex = 0;
        assignedTarget = false;

        
        //When an agent spawns, he should start by wandering
        aState = agentState.Wandering;

	}

	void Update() {
		switch (aState) {
            case agentState.Default:

                print("Default reached in AgentFSM Update");

                break;
            case agentState.Wandering:

                aiFollow.target = wanderWaypoints[wanderWaypointIndex];

                break;
		    case agentState.Hungry:
						
			    break;
			
		    case agentState.Working:
                
                aiFollow.target = workWaypoints[workWaypointIndex].transform.position;
                
            
			    //aState = agentState.Working;
			    break;
		
		}
	}

	bool PointInsideSphere(Vector3 point, Vector3 center, float radius) {
		print ("Dis" + Vector3.Distance(point, center));
		print ("inside: " + (Vector3.Distance(point, center) < radius));
		return Vector3.Distance(point, center) < radius;
	}

    
	public void TargetReached(){


        if (aState == agentState.Wandering){
            //aiFollow.target.position = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            //assignedTarget = false;

            wanderWaypointIndex = Random.Range(0, wanderWaypoints.Count);
            aiFollow.target = wanderWaypoints[wanderWaypointIndex];

        }   else if (aState == agentState.Working){


            /*if(PointInsideSphere(transform.position,GameObject.FindGameObjectWithTag ("WorkWaypoint").transform.position, 1.0f) != true){
				target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;
				aiFollow.target = target;
			}else{
				//target.position = new Vector3 (Random.Range (-2, 2), 0, Random.Range (-2, 2));
				int tempPosx = Random.Range(-2, 2);
				int tempPosz = Random.Range(-2, 2);
				target.position = new Vector3(transform.position.x + tempPosx,transform.position.y,transform.position.z + tempPosz);
			}*/

            workWaypointIndex = Random.Range(0, workWaypoints.Count);
            aiFollow.target = workWaypoints[workWaypointIndex].transform.position;

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
