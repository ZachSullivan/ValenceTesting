using UnityEngine;
using System.Collections;

public class AgentNeeds : MonoBehaviour {

    public Light hungerUI;
    //Variable keeps track of an agents hunger level, this value represents a 0-100% 
    // where 100% represents starving and 0% is full
    int hungerValue = 100;

	void Awake () {
        //Increase the agent's hunger level every 5 seconds
        InvokeRepeating("AgentHunger", 1, 1);

        

    }

    void Update() {

        StartCoroutine(FadeTo(100));


    }

    void AgentHunger() {

        if (hungerValue > 0){
            hungerValue--;
        }

		//Seek food source
        if (hungerValue <= 25) {
			//Vector3 destination = this.transform.position;
			
			//MoveTo agentObj = (MoveTo)agent.GetComponent(typeof(MoveTo));
		}

        Debug.Log(hungerValue);
    }

    IEnumerator FadeTo(float aTime)
    {

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            hungerUI.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }
    }

}
