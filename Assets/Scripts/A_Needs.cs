using UnityEngine;
using System.Collections;

public class A_Needs : MonoBehaviour {

	GameObject foodSource;
	GameObject agent;
	//Variable keeps track of an agents hunger level, this value represents a 0-100% 
	// where 100% represents starving and 0% is full
	int hungerValue = 100;
	
	public int hungerSearch = 50;
	
	void Awake () {
		foodSource = GameObject.FindWithTag("Foodsource");
		//Increase the agent's hunger level every 5 seconds
		InvokeRepeating("AgentHunger", 1, 1);
	}
		
	void AgentHunger() {
		
		if (hungerValue > 0){
			hungerValue--;
		}
		
		//Seek food source
		if (hungerValue <= hungerSearch) {
			//Vector3 destination = foodSource.transform.position;
			
			AutoAI agentObj = (AutoAI)agent.GetComponent(typeof(AutoAI));
			
			//agentObj.StartCoroutine(Hunger());
		}
	}
}
