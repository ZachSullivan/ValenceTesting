using UnityEngine;
using System.Collections;


public class AgentRoles : MonoBehaviour {
	
	public TestMovement _testMovement;
	public GameObject RoleTarget;


	void Start(){
		_testMovement = GetComponent<TestMovement>();
	}

	void OnGUI(){
		if(GUI.Button(new Rect(10,10,150,100), "Assign new role")){
			print ("New role assigned");
			_testMovement.ResetTime();

			_testMovement.target = RoleTarget.transform.position;
			_testMovement.path.Claim(this);
		}
	}
}
