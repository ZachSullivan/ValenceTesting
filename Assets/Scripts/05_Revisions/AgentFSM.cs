using UnityEngine;
using System.Collections;

public class AgentFSM : AIPath {

    //Used to control the movement of the agent
    AIPath _agentPathfinder;

    bool canAddTarget = true;

    //Create a series of possible states for the agent to be in
    enum agentState
    {
        Wandering,
        Hungry
    }

    agentState aState;

    void Awake() {

        //Find the agent movement controller attached
        _agentPathfinder = GetComponent<AIPath>();

        //When an agent spawns, he should start by wandering
        aState = agentState.Wandering;

        //When an agent spawns, start updating his hunger level
        updateHunger();
    }

    void Update() {

        print("aState");

        switch (aState) {
            case agentState.Wandering:

                if (canAddTarget) {
                    _agentPathfinder.RequestNewTarget();

                    canAddTarget = false;
                }

                OnTargetReached();

                
                // _agentPathfinder.RequestNewTarget();
                //Wander();
                //aState = agentState.Wandering;
                break;
            case agentState.Hungry:
                SearchForFood();
                break;
            default:
                print("Default reached in AgentFSM Update");
                break;
        }
    }


    public override void OnTargetReached()
    {
        base.OnTargetReached();

        StartCoroutine(AssignTarget(2.0f));
    }

    IEnumerator AssignTarget(float _waitTime) {
        target = new Vector3(Random.Range(-50, 50), 0, Random.Range(-15, 15));
        yield return new WaitForSeconds(_waitTime);
    }

    void Wander() {
        print("Currently Wandering");
        /*if (_agentPathfinder.canPlaceTarget == true)
        {
            _agentPathfinder.RequestNewTarget();
            _agentPathfinder.canPlaceTarget = false;
        }*/
        //_agentPathfinder.aState = AgentPathfinder.actionState.Wandering;
        
    }

    void SearchForFood() {
        print("Currently Hungry");
        //_agentPathfinder.aState = AgentPathfinder.actionState.Hungry;
    }

    void updateHunger() {

    }
}
