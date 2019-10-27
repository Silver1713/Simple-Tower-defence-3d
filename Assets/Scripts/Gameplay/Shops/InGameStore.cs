using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStore : MonoBehaviour
{
    GameObject[] Towers;
    public GameObject TowerFolder;
    public string towerFolderName;
    GameObject gameData;
    GameManager gameManager;
    public GameObject GameUIContainer;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        if (getManager() != null)
        {
            gameData = getManager();
        }
        if (Towers == null)
        {
            TowerFolder = getFolder();
            if (TowerFolder != null)
            {
                Towers = new GameObject[TowerFolder.transform.childCount];

                for (int i = 0; i < Towers.Length; i++)
                {
                    if (TowerFolder.transform.GetChild(i) != null)
                    {
                        Towers[i] = TowerFolder.transform.GetChild(i).gameObject;
                    }
                }
            }
        }
    }

    GameObject getFolder()
    {
        if (gameManager.towerInstance != null)
        {
            return gameManager.towerInstance;
        }
        return null;
    }

    GameObject getManager()
    {
        if (GameObject.Find("gameManager") != null)
        {
            return GameObject.Find("gameManager");
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool purchaseTurret(GameObject tower)
    {
        bool resultCheck = checkTowerInstance(tower);
        if (resultCheck == true)
        {
            TowerManager towerData = tower.GetComponent<TowerManager>();
            if (towerData != null)
            {
                float towerCost = towerData.Cost;
                GameManager manager = gameData.GetComponent<GameManager>();
                if (towerCost <= manager.getInGameGold() )
                {
                    
                    manager.setInGameGold(manager.getInGameGold() - towerCost);
                    GameUIContainer.GetComponent<GameBarManager>().ItemPurchased(manager.getInGameGold());

                    return true;
                }

            }
        }
        return false;
    }

    public bool getPurchaseResult(GameObject item)
    {
        bool res = checkTowerInstance(item);
        if (res)
        {
            TowerManager towerData = item.GetComponent<TowerManager>();
            GameManager game = gameData.GetComponent<GameManager>();
            float towerCostage = towerData.Cost;
            if (game.getInGameGold() >= towerCostage)
            {
                return true;
            }
            else return false;
        }
        return false;
    }

     bool checkTowerInstance(GameObject Tower)
    {
        foreach (GameObject t in Towers)
        {
            if (t.gameObject.name == Tower.name.Substring(0, Tower.name.Length - 7)) 
            {
                return true;
            }
        }
        return false;
    }
}
