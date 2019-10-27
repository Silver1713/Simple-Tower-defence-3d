using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandle : MonoBehaviour
{
    public GameObject startingMenu;
    GameManager data;
   
    void Start()
    {
        data = GameManager.gameManager;
        if (data.getGameStatus() == true) //If true means that user have just return from a game.
        {
            data.setStatus(false); //reset it to false
            for (int i = 0; i < transform.childCount; i++) //this loops through the UI container and retrieve the game Menu not the start menu.
            {
                if (transform.GetChild(i) != null)
                {
                    Transform currentUI = transform.GetChild(i);
                    if (currentUI.gameObject == startingMenu)
                    {
                        currentUI.gameObject.SetActive(true);
                    } else
                    {
                        currentUI.gameObject.SetActive(false);
                    }
                }
            }
        } else
        {
            
            data.setStatus(false); // if it is false do nothing(set it to false).
            
        }
    }

   
}
