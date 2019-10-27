using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    [HideInInspector] public static Transform[] Nodes;// Allow the nodes to be ascess gobally


    private void Awake()
    {
        Nodes = new Transform[transform.childCount];
        //Gather all the nodes into an array.
        for (int i = 0; i < Nodes.Length; i++)
        {
            //Store the node in the Nodes empty gameobject to the array allow gobal ascess.
            Nodes[i] = transform.GetChild(i);
            Debug.Log(transform.GetChild(i));
        }

    }
}
