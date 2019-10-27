using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour, IPointerClickHandler
{
    //Initialization of the required class and the variables.
    GameManager data;
    float currentCoin;
    string element;
    public int minUpgrade;
    public float upgradeCost;
    bool upgraded;
    public Text coins;
    public string TowerFolderName;
    float baseDamage;
    Transform towerFolder;
    
    void Start() // Get data managers and initialise the tower and the variables.
    {
        
        data = GameManager.gameManager;
        currentCoin = data.shopGold;
        element = transform.parent.parent.name;
        if (data.towerInstance != null)
        {
            towerFolder = data.towerInstance.transform;
        }
        for (int index = 0; index < towerFolder.childCount; index++)
        {

            GameObject towerTransform = towerFolder.GetChild(index).gameObject;
            towerTransform.SetActive(true);
        }
        


    }

    
    

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) //OnClick event
    {
       if (upgraded == false)
        {
            currentCoin = data.retrieveShopCurrency();
            //Check if user have the required number of upgrade.
            if (data.numUpgrades >= minUpgrade)
            {
                //Check if user can buy the upgrade.
                if (upgradeCost <= currentCoin)
                {
                    float remainder = currentCoin - upgradeCost;
                    coins.text = remainder.ToString();
                    data.setShopCurrency(remainder);
                    data.saveAll();
                    data.numUpgrades++;
                    upgraded = true;
                    Text currentText = transform.GetComponentInChildren<Text>();
                    currentText.text = "Upgraded";
                    upgrades();

                }
            } else
            {
                 upgraded = false;
                Text current = transform.GetComponentInChildren<Text>();
                Text prev = current;
                current.text = "Locked.";
                current.text = prev.text;
            }
        }
        
    }

    void upgrades() //Get the upgrades and apply it to the towerManager.
    {
        int upgrade = data.numUpgrades;

       
        
            for (int index = 0; index < towerFolder.childCount; index++)
            {
                
            GameObject towerTransform = towerFolder.GetChild(index).gameObject;
            towerTransform.SetActive(true);
            TowerManager towerManager = towerFolder.GetChild(index).GetComponent<TowerManager>();
            if (towerManager)
                {
                string upgradeElement = "None";
                

                if (upgrade >= 1 && upgrade <= 3)
                {
                    upgradeElement = "Nova";
                    towerManager.modifyElement(upgradeElement, true);
                    bool res = towerManager.element.Contains(upgradeElement);
                    if (res == false)
                    {
                        towerManager.element.Add(upgradeElement);
                    }
                    float increasedDamge = towerManager.towerDamage * ((upgrade * 10f) / 100f);
                    towerManager.towerDamage += increasedDamge;
                    
                        
                } else if (upgrade >= 4 && upgrade <= 6)
                {
                    upgradeElement = "Quake";
                    towerManager.modifyElement(upgradeElement, true);
                    bool res = towerManager.element.Contains(upgradeElement);
                    if (res == false)
                    {
                        towerManager.element.Add(upgradeElement);
                    }
                    float increasedDamge = towerManager.towerDamage * ((upgrade * 10f) / 100f);
                    towerManager.towerDamage += increasedDamge;
                    
                } else if (upgrade >= 7 && upgrade <= 9)
                {
                    upgradeElement = "Crystal";
                    towerManager.modifyElement(upgradeElement, true);
                    bool res = towerManager.element.Contains(upgradeElement);
                    if (res == false)
                    {
                        towerManager.element.Add(upgradeElement);
                    };
                    float increasedDamge = towerManager.towerDamage * ((upgrade * 10f) / 100f);
                    towerManager.towerDamage += increasedDamge;
                    

                }
                

            }
            towerTransform.SetActive(false); 
            }
        

    }
}
