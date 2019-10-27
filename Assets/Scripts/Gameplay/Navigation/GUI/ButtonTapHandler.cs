using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonTapHandler : MonoBehaviour, IPointerClickHandler
{
    
    [Header("Configurations")]
    public GameObject CoinDisplay;
    
    public bool isPauseBtn = false;
    public GameObject cointainerTarget;
    public static bool isPaused = false;
    public GameObject currentContainer;
    GameManager data;
    public bool isNewGame;
    public bool Exit;
    public float startingGold;
    public float currentBtn;
    
    void Start()
    {
        
        getManager(); //Retrieve the game manager
    }
    void getManager() // As it is static, it will be unique and retrievable across all scope.
    {
        data = GameManager.gameManager;
    }

   
  
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) // Onclick or Ontapped event.
    {
        if (isNewGame) // Check if it is a new game
        {
            PlayerPrefs.DeleteAll(); // delete all preferences
            cointainerTarget.SetActive(true);
            currentContainer.SetActive(false);
            data.setInGameGold(startingGold); //Give starting gold, saving it to the GameManager.
            data.inGameCurrency = startingGold;
        }
        
        handleNav(cointainerTarget);

        if (isPauseBtn) //If the button is a pause Button change its time scale in accordance to the status of the game using a bool value isPaused.
        {
            float currentScale = Time.timeScale;
            if (ButtonTapHandler.isPaused == false) // static var isPaused to be able to utilize across all scope.
            {
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    void handleNav(GameObject target = null) // This allow navigation to be handle between a target and a current Cointainer assignable through a inspector.
    {
        if (target != null)
        {
            //Check for CoinDisplay and assign the current coin value to it
            for (int i = 0; i < cointainerTarget.transform.childCount; i++)
            {
                if (cointainerTarget.transform.GetChild(i).gameObject == CoinDisplay)
                {
                    
                    float goldCoin = 0f;
                    if (isNewGame == false) // Check if it is a new game
                    {
                        goldCoin = data.retrieveShopCurrency(); //retrieve the shop currency in the game manager
                    }
                    else {
                        data.setShopCurrency(goldCoin); // set the shop currency in the game manager
                    } 
                    
                    if (CoinDisplay != null) // check if it is  null, if not set the text to the gold value.
                    {
                        for (int z = 0; z < CoinDisplay.transform.childCount; z++)
                        {
                            Text coinText = CoinDisplay.transform.GetChild(z).GetComponent<Text>();
                            if (coinText != null)
                            {
                                coinText.text = goldCoin.ToString();
                                
                            }
                        }
                    }
                   
                    
                } else if (Exit) //If the button is an exit button quit the application.
                {
                    Application.Quit();
                } 
            }
            target.SetActive(true);
            currentContainer.SetActive(false); //This allow navigations between different UIs by hiding and unhiding elements.

        }
    }

   
   
}
