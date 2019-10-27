using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBarManager : MonoBehaviour
{
    //Initialization of public variables to allow other script to ascess it.
    public  float healthAmount;
    public GameObject healthBar;
    public GameObject energyBar;
    public Transform goldUI;
    Text goldUIText;
    public float energyAmount;
    public float maxEnergy;
    public float maxHealth;
     public GameManager gameData;
    // Start is called before the first frame update
    void Start()
    {
        //Store the starting health and energy as maxHealth and energy respectively.
        healthAmount = maxHealth;
        energyAmount = maxEnergy;
        
        gameData = GameManager.gameManager;  //Gets the gameManager
        goldUIText = goldUI.GetComponent<Text>();
        goldUIText.text = "Gold: " + gameData.getInGameGold().ToString();
    }

   public void takeDamage(float nDamage = 0f) //This function allows other classs to call this. This allow enemies to take damage and also resize the healthbar.
    {
        if (nDamage != 0f)
        {
            healthAmount = healthAmount - nDamage;
            if (nDamage > 0f)
            {
                resizeHealthBar(healthAmount / maxHealth);
            } else
            {
                resizeHealthBar(0);
            }

        }
    }

    void resizeHealthBar(float nAmount) //This function allow the energy bar to be resized acccordingly
    {
        if (nAmount != 0)  //This checks to prevent the health bar from going below 0
        {
            Image hpBarImage = healthBar.GetComponent<Image>();
            hpBarImage.fillAmount = nAmount;
            Text detail = healthBar.GetComponentInChildren<Text>();
            detail.text = (nAmount * maxHealth).ToString() + "/" + maxHealth.ToString();
        } else
        {
            Image hpBarImage = healthBar.GetComponent<Image>();
            hpBarImage.fillAmount = nAmount; //Change the fillamount to allow an healthbar sprite to be resized
            Text detail = healthBar.GetComponentInChildren<Text>(); //GetComponentInChildren allow component in children like material to be retrieved.
            detail.text = "Failed"; //Change the display to fail when it reaches 0 or go below it,  thus calling a gameover function.
        }
    }

    void resizeManaBar(float nAmount) // This function allow the energy bar to be resized it have the same logic as resizeHealthBar()
    {
        if (nAmount != 0)
        {
            Image energyBarSprite = energyBar.GetComponent<Image>();
            energyBarSprite.fillAmount = nAmount;
            Text detail = energyBar.GetComponentInChildren<Text>();
            detail.text = (nAmount * maxHealth).ToString() + "/" + maxHealth.ToString();
        }
        else
        {
            Image energyBarSprite = energyBar.GetComponent<Image>();
            energyBarSprite.fillAmount = nAmount;
            Text detail = energyBar.GetComponentInChildren<Text>();
            detail.text = "No more Energy";
        }
    }



  public  void useEnergy(float nEnergyUsed) //This checks if users have enough energy before resizing.
    {
        if (nEnergyUsed != 0f)
        {

            energyAmount = energyAmount - nEnergyUsed;
            Debug.Log("--");
            Debug.Log(nEnergyUsed);
            
            Debug.Log(energyAmount);
            if (nEnergyUsed > 0f)
            {
                resizeManaBar(energyAmount / maxEnergy);
            }
            else
            {
                resizeHealthBar(0);
            }

        }
   }

   

    public void ItemPurchased(float cost) //This allow the gold to be change whenever an item is purchased.
    {
        goldUIText.text = "Gold: " + cost.ToString();
    }

}
