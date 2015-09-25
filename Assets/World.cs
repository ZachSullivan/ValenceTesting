using UnityEngine;
using System.Collections;
using Pathfinding;

public class World : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(30,30,200,30), "Update Graph")){
			GameObject obstical = GameObject.Find ("Test");
			AstarPath.active.UpdateGraphs(obstical.gameObject.GetComponent<Collider>().bounds);
		}
	}
}
