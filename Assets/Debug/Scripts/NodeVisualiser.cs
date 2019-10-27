using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisualiser : MonoBehaviour
///<summary>
///Used on nodes object, to visualise the movement of the enemy.
///</summary>
{
    [HideInInspector] public Vector3 size = new Vector3(2f, 0.1f, 2f);
    
    
    public bool disableMessage = true;
    public bool enableWarns = false;
    public bool isStart;
    

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
        if (isStart)
        {

        }

     }

    void checkForNextNode()
    {
        //Check for path choices
        if (transform.childCount != 0)
        {
            //If there is a child, there is a path choice
            //for loop
            for (int i = 0; i < transform.childCount; i++)
            {
                //Link the line
                Gizmos.DrawLine(transform.position, transform.GetChild(i).position);

            }
        }
    }
        
       
        
 


   
}

