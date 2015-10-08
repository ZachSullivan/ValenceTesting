using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class AgentPathfinder : MonoBehaviour {
    
    //The point to move to
    Vector3 targetPosition;
    private Seeker seeker;
    private CharacterController controller;
    
    //The calculated path
    public Path path;
    
    //The AI's speed per second
    public float speed = 100;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    //Stores the current time when a new path begins
    float _start_time;

    //Stores the elapsed time from when a path begins to current time
    float elapsed;


    public enum actionState{
        Default,
        Wandering,
        Hungry
    }

    public actionState aState;

    //Keep track if there is a current path being followed
    bool hasPath = false;

    public void Start(){

        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();

        //Request a new target for the agent to move to
        //StartCoroutine(RequestNewTarget(0.0f));

        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        //seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void OnTargetReached()
    {

        hasPath = false;
    }

    public void Update() {
        print(aState);
        //TODO Update this to be a state machine
        //If the agent is wandering then find him a random target
        switch (aState){

            case actionState.Wandering:
                Wander();
                break;
            
        }

        //Check if there is no assigned path
        if (path == null) {
            //If no path, then do nothing
            return;
        }

        //Check if the path has been completed
        if ((currentWaypoint == path.vectorPath.Count) && (aState == actionState.Wandering))
        {

            Debug.Log("End Of Path Reached");
            //hasPath = false;
            //StartCoroutine(RequestRandTarget(2.0f));
            //Once the path is completed, reset the elapsed travel time
            elapsed = 0;
            hasPath = false;
            return;
        }


        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.deltaTime;
        controller.SimpleMove(dir);
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }

        //Calcuate the elapsed time from when the path started to current time
        elapsed = Time.time - _start_time;

        //If the travel time exceeds a predefined amount ie. 10 seconds, then assing a new path
        if (elapsed >= 10) {

            //Find the agent a new path with no wait time
            StartCoroutine(RequestRandTarget(0.0f));
        }


    }

    void Wander() {
        if (hasPath == false) {
            hasPath = true;
            StartCoroutine(RequestRandTarget(2.0f));
        }

      

    }

    void Hungry() {
        RequestFoodTarget();
    }

    //Provide a new target location on request, must be passed a wait time before returning
    IEnumerator RequestRandTarget(float _time) {
        
        //If the call has requested a wait time, then pause
        yield return new WaitForSeconds(_time);
        
        //When a new path is created, save the current time
        _start_time = Time.time;

        targetPosition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-15, 15));
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        
    }

    void RequestFoodTarget() {
        targetPosition = GameObject.FindWithTag("Foodsource").transform.position;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void OnPathComplete(Path p) {

        path = p;

        //Reset the waypoint counter
        currentWaypoint = 0;

    }
}