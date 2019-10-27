using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEssential : MonoBehaviour
{
    public bool isActivated;
    public bool isActive = true;


    void Start() //This script prevent object from being destory when new scences is loaded.
    {

        if (isActivated)
        {
            transform.gameObject.SetActive(true);
            DontDestroyOnLoad(transform.gameObject);
            transform.gameObject.SetActive(isActive);


        }
    }
}

  
