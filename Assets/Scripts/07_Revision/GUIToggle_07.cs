using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIToggle_07 : MonoBehaviour {

    /* Set this script on a building */
    protected bool showMenu = false;
    // Set showing the menu to false

    //Assign agent fsm to this GUI
    public AgentLogic_07 agentLogic;

    void OnMouseUp(){

        // When you click, change the variables value
        if (showMenu)
            showMenu = false;
        else
            showMenu = true;
    }

    void OnGUI()
    {

        if (showMenu)
        {

            if (GUI.Button(new Rect(10, 10, 100, 20), "Assign Farmer"))
            {

                agentLogic.workWaypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("FarmWaypoint"));
                agentLogic.aState = AgentLogic_07.agentState.Working;
                agentLogic.jobState = AgentLogic_07.jobSubState.Farmer;
            }

            if (GUI.Button(new Rect(10, 30, 100, 20), "Assign Medic"))
            {

                agentLogic.workWaypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("HospitalWaypoint"));
                agentLogic.aState = AgentLogic_07.agentState.Working;
                agentLogic.jobState = AgentLogic_07.jobSubState.Medic;
            }

        }
    }

}