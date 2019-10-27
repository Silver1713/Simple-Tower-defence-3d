using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy //This is a container to store Enemy Data
    //This is a Enemy Object/Class.
{
    //Initialization of Stats for an enemy
    private GameObject ObjectType;
    private string name;
    private float health;
    private float damage;
    private float speed;
    private Dictionary<string, bool> resistant;
    private bool boss;
    

    public Enemy(GameObject enemyType, string enemyName, float enemySpeed, float enemyHealth, float enemyDamage, bool isBoss) //Comstructor to intialize and assign variables.
    {
        name = enemyName;
        health = enemyHealth;
        damage = enemyDamage;
        speed = enemySpeed;
        boss = isBoss;
      
    }
    //Get and set method
    public void setName(string enemyName)
    {
        name = enemyName;

    }
    public void setSpeed(float fEnemySpeed)
    {
        speed = fEnemySpeed;

    }
    public void setHealth(float fEnemyHealth)
    {
        health = fEnemyHealth;

    }
    public void setObject(GameObject objectType)
    {
        ObjectType = objectType;
    }
    public void setBoss(bool bIsBoss)
    {
        boss = bIsBoss;

    }
    public void setDamage(float nDamage)
    {
        damage = nDamage;

    }
    
    public string getName()
    {
        return name;
    }
    public float getHealth()
    {
        return health;
    }
    public float getDamage()
    {
        return damage;
    }
    public float getSpeed()
    {
        return speed;
    }
    public bool checkBoss()
    {
        return boss;
    }
    //Element and resistant
     public void initWeakness() //This allow data for weakness to be intialize
    {
        Dictionary<string, bool> initDictionary = new Dictionary<string, bool>()
        {
            {"Quake", false },
            { "Crystal", false},
            { "Nova", false },
            { "Pure", true }
        };
        resistant = initDictionary;

    }

    public void modifyWeakness(string key, bool value) //allow weakness to be modified externally by other script
    {
        if (resistant.ContainsKey(key))
        {
            resistant[key] = value;
        }
        else Debug.LogError("Key not found.");
    }

    public bool getElementalWeakness(string element) //Get weakness.
    {
        bool def = false;
        bool result = resistant.TryGetValue(element, out def);
        return result;
    }

    

    
}
