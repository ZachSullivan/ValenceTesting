using UnityEngine;
using System.Collections;

public class UserSetDesination : MonoBehaviour{
    public GameObject agent;
    private bool enableHalo;
    public Behaviour halo;

    void Update() {
        halo = (Behaviour)this.GetComponent("Halo");
        if (enableHalo == true && Input.GetButtonDown("LeftButton"))
        {
            halo.enabled = true;
        }
        if (Input.GetButtonUp("LeftButton")){
            halo.enabled = false;
        }
    }

    void OnMouseDown()   {

        if (agent != null) {

            Vector3 destination = this.transform.position;

            WanderLogic agentObj = (WanderLogic)agent.GetComponent(typeof(WanderLogic));

            //if (Input.GetButtonDown("LeftButton")){


                agentObj.wandering = false;
                agentObj.NewDestination(destination);

                enableHalo = true;
            //}
        }

    }
}