using UnityEngine;
using System.Collections;

public class AgentState : MonoBehaviour {
	
	GameObject foodSource;
	public AgentController _agentController;
	//Variable keeps track of an agents hunger level, this value represents a 0-100% 
	// where 100% represents starving and 0% is full
	public int hungerValue = 100;
	public bool feeding = false;
	
	public int hungerSearch = 90;
	
	void Awake () {
		feeding = false;
		foodSource = GameObject.FindWithTag("Foodsource");
		_agentController = gameObject.GetComponent<AgentController>();
		//Increase the agent's hunger level every 5 seconds
		
	}
	
	void Start(){
		//StartCoroutine(AgentHunger());
		InvokeRepeating("AgentHunger", 1, 1);
	}
	
	void Update(){
		
	}
	
	
	//IEnumerator AgentHunger() {
	void AgentHunger(){
		if(!feeding){
			//while(!feeding){
			if (hungerValue > 0)
			{
				hungerValue--;
				
				//yield return new WaitForSeconds(1.0f);
				
			}
			
			
			if (hungerValue <= hungerSearch)
			{
				
				_agentController.hungry = true;
				//gameObject.GetComponent<TestMovement>().hungry = true;
			}
		}
		//}
		
		
		else{
			//while(feeding){
			if(hungerValue < 100){
				hungerValue++;
				//yield return new WaitForSeconds(1.0f);
			}
		}
		//}
		
		if (hungerValue == 100){
			
			feeding = false;
			//StartCoroutine(AgentHunger());
		}
		
		if (hungerValue == 0) {
			Destroy(this.gameObject);
		}
		
	}
	
	public void Feed(){
		feeding = true;

		//if(hungerValue < 100){
			_agentController.hungry = false;
			//hungerValue ++;
			
		//} 	
		
		
	}
}
