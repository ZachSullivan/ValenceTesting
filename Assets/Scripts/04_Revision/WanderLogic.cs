using UnityEngine;
using System.Collections;

public class WanderLogic : MonoBehaviour {

    private Vector3 startPosition;

    //Is the agent wandering at the moment?
    public bool wandering = true;

    //Allow user to choose the speed an agent will wander at
    public float wanderSpeed = 0.5f;

    //Allow user to choose a wandering radius for the agent
    public float wanderRange = 3.0f;



    private NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        startPosition = this.transform.position;

        if (wandering == true) {
            InvokeRepeating("Wander", 1f, 5f);
        }
        //agent.destination = goal.position;
    }

    void Wander() {
        Vector3 destination = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange),0, Random.Range(-wanderRange, wanderRange));

		/*if(!Physics.Raycast(transform.position,Vector3.forward,5)){
			transform.Rotate(Vector3.up, rotateSpeed * Time.smoothDeltaTime);
		}*/

        NewDestination(destination);
    }

    public void NewDestination(Vector3 targetPosition) {
        agent.SetDestination(targetPosition);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
