using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseHandler : MonoBehaviour
{
    //This script allow Winning and losing  event to be managed
    //Below initialise the variables and retrieve statitic to control how things are when winning or losing Occured.
    GameManager gameData;
    public static WinLoseHandler winLose;
    public GameObject StarsFolder;
    public string winMessage;
    public string LoseMessage;
    public bool isWin;
    //To make this a singleton so as to not initalise it every time it booted in and allow for only one winLoseUI
    private void Awake()
    {
        if (winLose == null)
        {
            winLose = this;
        } else if (winLose != this)
        {
            Destroy(transform.parent.parent.gameObject); //Prevent it from being destroyed.
        }
    }
    void Start()
    {
       
    }

    public void LoadUI()
    {
        gameData = GameManager.gameManager; //Get the game manager allowning ascess to most of the game data.
        if (gameData.getGameStatus() == true)
        {
            //Initialize the number of stars.
            if (isWin)
            {
                int numStar = gameData.numOfStars;
                for (int i = 0; i < numStar; i++)
                {
                    if (StarsFolder.transform.GetChild(i) != null && i < StarsFolder.transform.childCount)
                    {
                        Transform stars = StarsFolder.transform.GetChild(i);
                        stars.gameObject.SetActive(true);
                    }
                }
            }
            //Initialize the winning and losing text.
            Text windowText = transform.GetComponentInChildren<Text>();
            if (windowText && isWin)
            {
                windowText.text = winMessage;
            }
            else if (windowText && isWin == false)
            {
                windowText.text = LoseMessage;
            }
        }
    }

    
}
