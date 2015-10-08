﻿using UnityEngine;
using System.Collections;

public class AgentSpawner : MonoBehaviour {

    //Reference agent for spawner, user must assign this in the Inspector
    public GameObject Agent;

    void Start() {
        SpawnAgent();
    }

    //On call, spawn a new agent from 05_Revision prefab folder
    void SpawnAgent() {

        GameObject newAgent = (GameObject)Instantiate(Agent, new Vector3(0, 0.51f, 0), Quaternion.identity);

        newAgent.name = "Agent" + newAgent.GetInstanceID();
    }

    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 20),"Create Agent")) {
            SpawnAgent();
        }
    }
}