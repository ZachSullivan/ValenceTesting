using UnityEngine;
using System.Collections;
using Pathfinding;

public class WorkLogic : MonoBehaviour {

	//Point agent will move to
	public Transform target;

	AIFollow aiFollow;


	void Start(){
		target = GameObject.FindGameObjectWithTag ("WorkWaypoint").transform;

		aiFollow = GetComponent<AIFollow>();

		aiFollow.target = target;
	}

	public void TargetReached(){
		print ("Here");
	}
}
