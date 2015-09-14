using UnityEngine;
using System.Collections;

public class AutoAI : MonoBehaviour {

	//Speed at which an agent will rotate
	public int rotateSpeed = 10;

	//Speed at which an agent will move
	public int agentSpeed = 1;

	//Distance which agent will detect obsticals
	public int raycastDistance = 1;

	private float agentRotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		StartCoroutine(DirectionChoice());

		if(!Physics.Raycast(transform.position, transform.forward, raycastDistance)){
			transform.Translate(Vector3.forward * agentSpeed * Time.smoothDeltaTime);
		}
		else {
			transform.Rotate(Vector3.up, agentRotation * rotateSpeed * Time.smoothDeltaTime);
		}

	}

	IEnumerator DirectionChoice (){
		agentRotation = Random.Range(-360.0f, 360.0f);

		yield return 0;
	}
}
