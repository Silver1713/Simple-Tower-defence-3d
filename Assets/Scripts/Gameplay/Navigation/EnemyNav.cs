using UnityEngine;
using UnityEngine.AI;
public class EnemyNav : MonoBehaviour

   
{

    //Initialisation of variables and configuration values.
    private Vector3 destination; 
    private Transform targetNode;
    private Rigidbody rg;
    private NavMeshAgent agent;
    private float checkDistance = 1f; 
    private int CurrentNodeIndex = 0;
    bool check = true;
    float offset;

    //Set the target position to the first node and set the target node to the first which allow the code in Updates() to handle its transition.
    void Start()
    {
        offset = transform.localScale.x;
        rg = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        targetNode = NodesManager.Nodes[CurrentNodeIndex];
        agent.SetDestination(targetNode.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ran");
        
        if (Vector3.Distance(transform.position, targetNode.position) <= checkDistance) //Check for distance between the nodes and the transform if 
            //it overshoot the limit set in checkDistance then move to next node.
        {
            
            
            //Check for choice
            if (targetNode.childCount != 0)
            {
                if (transform)
                {
                    //Choose a path
                    
                    targetNode = targetNode.GetChild(Random.Range(0, targetNode.childCount));
                    Debug.Log("Choosen: " + targetNode.name);
                    //Assign to destination
                    agent.SetDestination(targetNode.position);
                }
            } else
            {
                getNextNode();
                if (check)
                {
                    agent.SetDestination(targetNode.position);
                }
            }
            
        }   
    }

    void getNextNode() //Allow next node to be retrieved in the Nodes folder.
    {
        if (CurrentNodeIndex < NodesManager.Nodes.Length-1)
        {
            CurrentNodeIndex++; //Increment the value by 1
            targetNode = NodesManager.Nodes[CurrentNodeIndex];
        } else
        {
            //If the enemy reach the end as the node index is the last one, call damageObjective and destroy the object immediately.
            transform.GetComponent<enemyManager>().damageObjective();
            DestroyImmediate(transform.gameObject, false);
            check = false;
            return;
        }
    }

    
}
