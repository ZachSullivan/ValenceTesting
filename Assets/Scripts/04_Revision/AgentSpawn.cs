using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AgentSpawn : MonoBehaviour
{

    public GameObject Agent;

    public List<GameObject> agentList;
    //public GameObject[] agentArr;


    //Speed at which an agent will rotate
    public int rotateSpeed = 10;

    //Speed at which an agent will move
    public int agentSpeed = 1;

    //Distance which agent will detect obsticals
    public int raycastDistance = 1;


    public int hungerSearch = 50;

    int hungerValue = 100;

    private float agentRotation;

    public int spawnCounter;

    float startTime;

    public int currentSpawnCount;

    int targetIndex;

    Vector3[] path;

    GameObject foodSource;

    public State state;

    public enum State
    {
        Wander,
        Hunger
        //States go here
    }

    void Awake()
    {
        startTime = Time.deltaTime;


        state = State.Wander;
        //agentArr = new GameObject[spawnCounter];
        agentList = new List<GameObject>();

        StartCoroutine(InstantiateAgent());

        //Uncomment to enable hunger actions
        //InvokeRepeating("AgentHunger", 1, 1);

        //StartCoroutine(FSM());
    }

    void Start()
    {

        foodSource = GameObject.FindWithTag("Foodsource");
    }

    void AgentHunger()
    {

        if (hungerValue > 0)
        {
            hungerValue--;
        }
        if (hungerValue <= hungerSearch)
        {
            //Vector3 destination = foodSource.transform.position;
            state = State.Hunger;
        }
        //Seek food source


        print(hungerValue);
    }

    IEnumerator FSM(int _agentPos)
    {


        while (true)
        {
            
            StartCoroutine(DirectionChoice());
 
            if (!Physics.Raycast(agentList[_agentPos].transform.position, agentList[_agentPos].transform.forward, raycastDistance)){

                yield return StartCoroutine(state.ToString(), (_agentPos));

                StartCoroutine(Hunger(_agentPos));
                //PathRequestManager.RequestPath(agentList[_agentPos].transform.position, foodSource.transform.position, OnPathFound);

            }
            else
            {
                agentList[_agentPos].transform.Rotate(Vector3.up, agentRotation * rotateSpeed * Time.smoothDeltaTime);
            }

            


        
        }
    }

    IEnumerator Wander(int _agentPos)
    {

        agentList[_agentPos].transform.Translate(Vector3.forward * agentSpeed * Time.smoothDeltaTime);
        yield return null;


    }

    IEnumerator Hunger(int _agentPos)
    {
        
        PathRequestManager.RequestPath(agentList[_agentPos].transform.position, foodSource.transform.position, OnPathFound);

        yield return null;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }

    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, 0.05f);

            yield return null;
        }
    }

    IEnumerator InstantiateAgent()
    {
        //Add agent spawn

        for (int i = 0; i <= spawnCounter - 1; i++)
        {

            GameObject newAgent = (GameObject)Instantiate(Agent, new Vector3(0, 0.51f, 0), Quaternion.identity);

            newAgent.name = "Agent" + newAgent.GetInstanceID();

            agentList.Add(newAgent);
            StartCoroutine(FSM(i));

            yield return new WaitForSeconds(1.0f);
        }

        /* while (spawnCounter < 10) {
            GameObject newAgent = (GameObject)Instantiate(Agent, new Vector3(0, 0.51f, 0), Quaternion.identity);

             newAgent.name = "Agent" + newAgent.GetInstanceID();

             spawnCounter++;

             yield return new WaitForSeconds(5.0f);
         }
     }*/
        yield return null;
    }

    IEnumerator DirectionChoice()
    {
        agentRotation = Random.Range(-360.0f, 360.0f);

        yield return 0;
    }

}
