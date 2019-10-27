using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyManager : MonoBehaviour
{
    //Initializations of enemyData.

     Enemy enemyData;
    public float rewardAmount;
    public float bossSpeedFactor;
    public float bossSize;
    private GameManager gameManager;
    public GameObject enemyInstance;
    public string enemyName;
    public float speed;
    public float damage;
    public float health;
    public float maxHealth;
    public bool IsBoss = true;
    public string[] weaknesses;
    public Transform UIHealth;
    public Transform objectiveObject;
   
    // Start is called before the first frame update
    void Start() //Retrieve the GameManager and initialize all the variables related to the enemy.
    {
        //Initialisation
        gameManager = GameManager.gameManager; 
       if (transform != null)
        {
            enemyData = new Enemy(enemyInstance, enemyName, speed, health, damage, IsBoss);
            maxHealth = enemyData.getHealth();
            enemyData.initWeakness();
            InitialiseEnemyElement();
            GiveBossQualities();
        }

        
    }

    

    void InitialiseEnemyElement() // Initialize the element dictionary in the element object and assign them accordingly.
    {
        if (weaknesses != null)
        {
            for (int index = 0; index < weaknesses.Length; index++)
            {
                if (enemyData.getElementalWeakness(weaknesses[index]) != false || weaknesses != null)
                {
             
                    enemyData.modifyWeakness(weaknesses[index], true);
                }
            }
        }
    }

    bool getWeakness(string element) //Retrieve weaknesses from the Enemy class on a dictionary.
    {
        return enemyData.getElementalWeakness(element);
    }
    
    void setWeakness(string element, bool isWeakness) // Allow weakness store in a dictionary to be changed
    {
        enemyData.modifyWeakness(element, isWeakness);
    }
    
    public void TakeDamage(float nDamage, string[] attributes = null) // allow the enemy to take damage
    {
        float damageMultiplier = 0; // damage multiplier which allow bonus damage

        if (attributes != null)
        {
            foreach (string element in attributes)
            {
                if (getWeakness(element) == true) // For every element that the enemy is weak and the user have as a projectile add 1 to the damageMultiplier
                {
                    damageMultiplier++;
                }
            }
        }
        float damageDelt = (((damageMultiplier * 10)/100) * nDamage) + nDamage; // store the damage in a variable

        //This retreive the enemy health and  change it accordingly it also called onDie once it health reaches 0 or under 0.
        float currentHealth = enemyData.getHealth();
        float resultingHealth = currentHealth - nDamage;
        Image healthBar = UIHealth.GetComponent<Image>();
        healthBar.fillAmount = resultingHealth / maxHealth;
        if (resultingHealth <= 0)
        {
            OnDied();
            return;
        } else
        {
            enemyData.setHealth(resultingHealth);
        }
    }

    

    void OnDied() // allow gold to be awarded and allow the enemy to be removed.
    {
        AwardGold(rewardAmount);
        Destroy(enemyInstance);
        
        Debug.Log("Dead");
        return;
       
    }

    void AwardGold(float nAmount) // Award gold by saving the value to the gameManager.
    {
        float currentGold = gameManager.inGameCurrency;
        float totalGold = currentGold + nAmount;

        if (IsBoss)
        {
            totalGold = (nAmount * 2) + currentGold;
            
        }
        Debug.Log("1" + totalGold.ToString());
        gameManager.inGameCurrency = totalGold;
        Debug.Log("2" + totalGold.ToString());
        

    }

    void GiveBossQualities() //If isBoss is set to true give the enemy a more stornger stats.
    {
        if ( enemyData.checkBoss() == true)
        {
            speed = enemyData.getSpeed() * bossSpeedFactor;
            transform.localScale = transform.localScale * bossSize;
            applySpeed();
        }
    }

    void applySpeed() // Allow speed to be applied to the NavMeshAgent
    {
        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    public void damageObjective() // Allow defence objective to be damaged
    {
        ObjectiveManager objData = objectiveObject.GetComponent<ObjectiveManager>();
        if (objData)
        {
            objData.takeDamage(damage);
        }
    }

   
}
