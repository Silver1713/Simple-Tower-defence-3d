using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityHandler : MonoBehaviour, IPointerClickHandler
{
    //Initialization of variables
    GameObject towerInstance;
    GameBarManager gameBar;
    GameManager dataManager;
    public GameObject Objective;
    public float abilityParams = 20f; // The ability variable to affect things like heal amount and damage amount
    public bool cooldownState = false;
    public float coolDownTime = 3f;
    public float abilityCost = 20f;
    
    public int abilityType;
    public string enemyHolderName;
     GameObject timer;
    Text timeCount;


   
    void Start() // Get the countdown timer for the ability UI to indicate cooldown.
        //Also initilalize the game manager.
    {
        timer = transform.GetChild(0).gameObject;
        dataManager = GameManager.gameManager;
        timeCount = timer.GetComponent<Text>();

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) //To detect a mouse click.
    {
        Debug.Log("Skill Clicked");
        if (abilityType == 1) // This allow one script to execute a different type of abilities.
        {
            if (cooldownState == false)
            {
                heal(Objective); //Heal the objective by calling heal()
                Debug.Log(Objective.GetComponent<ObjectiveManager>().manaAmount); 
            }
            
        } else if (abilityType == 2)
        {
            if (cooldownState == false)
            {
                multiDamage(Objective); // Damage enemies current in the game.
            }
            
        }
    }

    void heal(GameObject objectiveObject) //This get the objectiveManager and change its health.
        //This also reduce your mana.
    {
        if (objectiveObject != null)
        {
            ObjectiveManager manager = objectiveObject.GetComponent<ObjectiveManager>();
            bool res = false;
            if (manager.manaAmount - abilityCost >= abilityCost)
            {
                
                 res = manager.healHealth(abilityParams);
                manager.castSpell(abilityCost); // This allow the reduction to displayed and allow the UI to be resized,
                Debug.Log(res);
            }
            if (res)
            {
                
                timer.SetActive(true); // Show the counter
                StartCoroutine(startCooldown(coolDownTime)); // Start the cooldown
                
            }
        }
    }

    void multiDamage(GameObject objective) // Allow multi damage to the opponenet by looping across enemies in the current enemies folder.s
    {
        GameObject enemyContainer = GameObject.Find(enemyHolderName);
        if (enemyContainer != null && cooldownState == false)
        {
            ObjectiveManager manager = objective.GetComponent<ObjectiveManager>();
            
            for (int i = 0; i < enemyContainer.transform.childCount; i++)
            {
                if (manager.manaAmount - abilityCost >= abilityCost)
                {

                    string[] elements = new string[] { "Nova", "Quake", "Crystal", "True" };
                    Transform currentInstance = enemyContainer.transform.GetChild(i);
                    enemyManager enemyManagement = currentInstance.GetComponent<enemyManager>();
                    enemyManagement.TakeDamage(abilityParams, elements);
                }
            }
            manager.castSpell(abilityCost);
            timer.SetActive(true);
            StartCoroutine(startCooldown(coolDownTime));
            

        }
    }
   

    IEnumerator startCooldown(float waitTimeInSeconds) // this allow cooldown to be initiated and allow the buttons to be disactivated.
    {
        cooldownState = true;
        
        float currentTime = waitTimeInSeconds;
        for (int i = 0; i < waitTimeInSeconds; i++)
        {
            
            transform.GetComponentInChildren<Text>().text = currentTime.ToString();
            currentTime -= 1;


            if (i == waitTimeInSeconds - 1)
            {
                cooldownState = false;
                timer.SetActive(false);
            }
            yield return new WaitForSeconds(waitTimeInSeconds);

        }
    }
}
