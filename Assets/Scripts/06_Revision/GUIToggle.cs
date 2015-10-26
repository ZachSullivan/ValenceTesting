﻿using UnityEngine;
using System.Collections;

public class GUIToggle : MonoBehaviour {

    /* Set this script on a building */
    protected bool showMenu = false;
    // Set showing the menu to false

    void OnMouseUp(){

        // When you click, change the variables value
        if (showMenu)
            showMenu = false;
        else
            showMenu = true;
    }

    void OnGUI(){

        if (showMenu)

            if (GUI.Button(new Rect(10, 10, 100, 20), "Assign Agent")){

                print("CLICKED");
            }

    }

}