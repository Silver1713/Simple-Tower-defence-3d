using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelHandle : MonoBehaviour, IPointerClickHandler
{
    //Initialize the game manager and allow starting gold amount to be edited
    GameManager manager;
    public float goldAmt;
    
    // Start is called before the first frame update
    void Start()
    {
       if (transform.name.Contains("Container"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                childObject.AddComponent<LevelHandle>().goldAmt = goldAmt ; //Add this scirpt to the level selection button which allow scences to be loaded based on the transform name.
            }
        }
    }
    

    

     void IPointerClickHandler.OnPointerClick(PointerEventData eventData) //This load the sences and sets the starting gold amount.
    {
        Debug.Log("Changing.. Sence...");
        if (SceneManager.GetSceneByName(transform.name) != null)
        {
           manager =  GameManager.gameManager;
            manager.setInGameGold(goldAmt);
            manager.setStatus(true);
            SceneManager.LoadScene(transform.name);
        }
    }
}
