using UnityEngine;
using System.Collections;

public class RoleGUI : MonoBehaviour {

    public TestMovement _testMovement;
    public GameObject RoleTarget;


    void Start()
    {
        _testMovement = GetComponent<TestMovement>();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Assign new role"))
        {
          
        }
    }
}
