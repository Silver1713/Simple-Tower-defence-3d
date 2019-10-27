using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleGamePurchase : MonoBehaviour, IPointerClickHandler //Implements (Interface)IPointerClickHandler to allow it function to be called
{
   //Initialize configuration and statistic
    public bool Unlocked;
    public int itemIndex;
    public GameObject towerInstance;
    public GameObject gameData;
    GameManager gm;
    void Start()
    {

        for (int i = 0; i < towerInstance.transform.childCount; i++)
        {
            if (towerInstance.transform.GetChild(i).name == transform.parent.name && towerInstance.transform.GetChild(i).name.Contains("default") == false)//Check if the number of tower and initialize the UI accordingly. 
            {
                itemIndex = i;
                
            }
        }


        checkIfUnlocked(); //Check if current tower is unlicked
        if (Unlocked == true) //If true change text to purchased.
        {
            Text text = transform.GetComponentInChildren<Text>();
            if (text)
            {
                text.text = "Purchased";
            }


        } else
        {
            Text text = transform.GetComponentInChildren<Text>();
            for (int i = 0; i < towerInstance.transform.childCount; i++)
            {
                if (towerInstance.transform.GetChild(i).name == transform.parent.name)
                {
                    TowerManager towerData = towerInstance.transform.GetChild(i).GetComponent<TowerManager>();
                    if (towerData != null)
                    {
                        text.text = towerData.Cost.ToString(); //Else show the coins required.
                    }
                }
            }
        }
    }
    void checkIfUnlocked()
    {
        gm = GameManager.gameManager;
        int index =  gm.getTowersHistories();
        if (index < itemIndex)
        {
            Unlocked = true;
        } else
        {
            Unlocked = false;
        }
    }

    void handlePurchase(Text btnText) //This allow purchase to be handled
    {
        if (btnText)
        {
            gm.setTowerUnlockHistory(itemIndex); //GameManger.setTowerUnlockHistory which allow the num of upgrade to be saved.
            btnText.text = "Purchased";
            Unlocked = true;
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)  //This detect if user have click on it and handle the purchases by comparing the retrieve coin with its current cost.
    {
        checkIfUnlocked();
        if (Unlocked == true)
        {
            Debug.Log("Stuff have Already been Purchased");
        } else if (Unlocked == false)
        {
            float currentShopCoin = gm.retrieveShopCurrency();
            for (int i = 0; i < towerInstance.transform.childCount; i++)
            {
                if (towerInstance.transform.GetChild(i).name == transform.parent.name)
                {
                    TowerManager towerData = towerInstance.transform.GetChild(i).GetComponent<TowerManager>();
                    if (towerData != null)
                    {
                        float cost = towerData.Cost;
                        if (gm.retrieveShopCurrency() > towerData.Cost)
                        {
                            
                            handlePurchase(transform.GetComponentInChildren<Text>());
                        }
                    }
                }
            }
        }
    }

    
}
