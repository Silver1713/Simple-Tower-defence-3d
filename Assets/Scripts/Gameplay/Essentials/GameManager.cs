using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Initialization of Game Data to allow values to be transfered as stored gobally
    public static GameManager gameManager; //This make the GameManager Class gobal to alllow script to ascess it across all scope.
   
    public float inGameCurrency;
    public float playerMana = 100f;
    public float shopGold;
    public int unlockedItem;
    public float winGameGold;
    public float winScaleFactor;
    public int numOfStars;
    public string towerInstanceName;
     public GameObject towerInstance;
    public string towerUnlockKey;
    public bool isInGame;
    public string enemyFolder;
    public string playerShopKey;
    public string GameMenuContainerName;
    public string MainMenuContainerName;
    public int numUpgrades = 0;
    public string upgradeKeys;
    private GameObject GameMenu;
    private GameObject MainMenu;
    public GameObject gameOverUI;
    public float playerHealth = 100f;
    public string starKey;


    //Make GameManager a singleton.
    private void Awake()
    {
       
        if (gameManager == null)
        {
            gameManager = this;

        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }
    void Start() //Find and Store UICointainer Values of the star menu and the game menu.
    {
        Debug.Log("Loaded");

        if (GameObject.Find(MainMenuContainerName) != null && GameObject.Find(GameMenuContainerName) != null)
        {
            MainMenu = GameObject.Find(MainMenuContainerName);
            GameMenu = GameObject.Find(GameMenuContainerName);
        }
        if (isInGame == true) //Check if user is playing game or at menu.
        {
            isInGame = false;
            if (MainMenu != null && GameMenu != null) //Show the respective UIs if users just came back from a game
            {
                MainMenu.SetActive(false);
                GameMenu.SetActive(true);
            } else
            {
                isInGame = true;
            }

        } 

        
        
        

        if (GameObject.Find(towerInstanceName) != null) //Find the towers folder and assign it to towerINstance allwo it to be used across scences.
        {
            towerInstance = GameObject.Find(towerInstanceName);
        }

        if (PlayerPrefs.HasKey(upgradeKeys) == false) //Set PlayerPrefs to allow value to be saved.
        {
            PlayerPrefs.SetInt(upgradeKeys, numUpgrades);

        } else
        {
            numUpgrades = PlayerPrefs.GetInt(upgradeKeys);
        }
       
        if (PlayerPrefs.HasKey(playerShopKey) == false) //Set PlayerPrefs to allow value to be saved.
        {
            PlayerPrefs.SetFloat(playerShopKey, shopGold);
            shopGold = PlayerPrefs.GetFloat(playerShopKey);
        } else
        {
            shopGold = PlayerPrefs.GetFloat(playerShopKey); //Set PlayerPrefs to allow value to be saved.
        }
        if (PlayerPrefs.HasKey(towerUnlockKey) == false)
        {
            setTowerUnlockHistory(unlockedItem);

        }

        if (PlayerPrefs.HasKey(starKey) == false) //Set PlayerPrefs to allow value to be saved.
        {
            PlayerPrefs.SetInt(starKey, 0);
        } else
        {
            numOfStars = PlayerPrefs.GetInt(starKey);
        }

        inGameCurrency = 0;
        DontDestroyOnLoad(gameObject); //Allow object to pass daa over scences as it prevent it from being destroy when a new scence is loaded
    }

    public void setTowerUnlockHistory(int index) //Allow upgrade histories to be saved in the game manager.
    {
        PlayerPrefs.SetInt(towerUnlockKey, index);
    }

    public int getTowersHistories() // allow value to be retrieved.
    {
        unlockedItem = PlayerPrefs.GetInt(towerUnlockKey);
        return unlockedItem;
    }

   void setMana(float mana) //Allow energy to be set
    {
        playerMana = mana;
    }

    //This get and set methods allow variable to be retrieve and to save in game manager.
    public float getInGameGold() 
    {
        return inGameCurrency;
    }
    public void setInGameGold(float nGold)
    {
        inGameCurrency = nGold;
    }

    public float retrieveShopCurrency()
    {
        return shopGold;
    }
    public void setShopCurrency(float amount)
    {
        shopGold = amount;
        PlayerPrefs.SetFloat(playerShopKey, amount);
    }

    public void saveAll() //Save the values to playerPref to allow future use.
    {
        PlayerPrefs.Save();
        
    }
    
    public void WinGame(float currentHealth = 100f) // Allow Game to win and using user health to award the number of stars.
    {
        if (currentHealth > 80f) 
        {
            numOfStars++;
        }
        if (currentHealth > 60f)
        {
            numOfStars++;
        }
        if (currentHealth > 30f)
        {
            numOfStars++;
        }
        if (isInGame == true)
        {
            
            setShopCurrency(winGameGold + (winScaleFactor * numOfStars)); //Set shop currency allow changes to be made to the shop currency.

            saveAll();
            WinLoseHandler winLose = gameOverUI.GetComponentInChildren<WinLoseHandler>();
            winLose.isWin = true;
            winLose.LoadUI();
            displayWinLoseUI(gameOverUI);
            
        }
    }
    public void LoseGame() //Called when player lose the game, this allow the game over UI to be displayed and facilate user back to menu.
    {
        if (isInGame == true)
        {
            

            saveAll();
            WinLoseHandler winLose = gameOverUI.GetComponentInChildren<WinLoseHandler>();
            winLose.isWin = false;
            winLose.LoadUI();
            displayWinLoseUI(gameOverUI);
        }
    }

    void displayWinLoseUI(GameObject WinUI) //The UI to facilalitate user back to menu.
    {
        WinUI.SetActive(true);
        
    }

    public bool getGameStatus() //Get Status
    {
        return isInGame;
    }
    public void setStatus(bool v) // Set status.
    {
        isInGame = v;
    }
}
