using UnityEngine;
using System.Collections;

public class AgentFSM : MonoBehaviour {

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

    void Update() {
        switch (aState) {
            case agentState.Wandering:
                Wander();
                aState = agentState.Wandering;
                break;
            case agentState.Hungry:
                SearchForFood();
                break;
            default:
                print("Default reached in AgentFSM Update");
                break;
        }
    }

    void Wander() {
        print("Currently Wandering");
        _agentPathfinder.aState = AgentPathfinder.actionState.Wandering;
        
    }

    void SearchForFood() {
        print("Currently Hungry");
        _agentPathfinder.aState = AgentPathfinder.actionState.Hungry;
    }

    void updateHunger() {

    }
}
