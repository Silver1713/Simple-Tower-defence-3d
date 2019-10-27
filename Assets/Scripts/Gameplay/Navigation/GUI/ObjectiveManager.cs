using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    //Allow game objective to be managed
    //Allow stats to be managed in unity inspector
    public float objectiveHealth;
    public float manaAmount = 200f;
    float maxHealth;
    public GameObject gameBarManagerHolder;
    GameBarManager gameBarManager;
    GameManager manager;
    public GameObject hpBar;
    public GameObject manaBar;

    // Assign the starting amount as max health and mana respectively 
    //get the game manager and game bar managers
    //which allow us to check it variables and ascess its public function
    void Start()

    {
        manager = GameManager.gameManager;
        maxHealth = objectiveHealth;

        gameBarManager = gameBarManagerHolder.GetComponent<GameBarManager>();

        gameBarManager.maxHealth = objectiveHealth;
        gameBarManager.healthAmount = objectiveHealth;
    }
    public void takeDamage(float nDamage) // Allow damage to be taken by then objective and allow the newHealth to be saved in the manager.
    {
        float resultingHealth = objectiveHealth - nDamage;
        Image hpSprite = hpBar.GetComponent<Image>();
        if (hpSprite)
        {
            
            gameBarManager.takeDamage(nDamage);
            objectiveHealth = resultingHealth;
        }
        if (nDamage > objectiveHealth)
        {
            manager.LoseGame();
            Time.timeScale = 0f;
        }
    }

    public void castSpell(float manaCost) // Allow mana changes to be reflected in the gamebar manager which allow it UI to be changed
    {
        float resultingMana = manaAmount - manaCost;
        Image manaSprite = manaBar.GetComponent<Image>();
       
        if (manaSprite)
        {
            
            gameBarManager.useEnergy(manaCost);
            manaAmount = resultingMana;
            Debug.Log(resultingMana);
        }
    }

    public bool healHealth(float nAmt) //Allow the objective to heal, check if it is max if not then heal.
    {
        Debug.Log(nAmt);
        if (nAmt + objectiveHealth < maxHealth)
        {
            
            objectiveHealth += nAmt;
            return true;
        } else
        {
            return false;
        }
    }
   
}
