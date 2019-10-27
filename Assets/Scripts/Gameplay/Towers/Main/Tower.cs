using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower //Tower class Object as a cointainer to store tower data
{
    private GameObject ObjectType;
    public float cost;
    private string name;
    private float health;
    private float damage;
    private float attackCooldown;
    private float fireRate;
    private Dictionary<string, bool> attributes;
    private int piority;
    public Tower(float price, GameObject tower, string towerName, float towerHealth, float towerDamage,float nfireRate, float cooldown, int piorities) //Constructor to assign and store variables.
    {
        cost = price;
        ObjectType = tower;
        name = towerName;
        health = towerHealth;
        damage = towerDamage;
        attackCooldown = cooldown;
        piority = piorities;
        fireRate = nfireRate;
    }

    //Get and set method
    public void setCost(float nCost)
    {
        cost = nCost;
    }
    public float getCost()
    {
        return cost;
    }
    public void setName(string towerName1)
    {
        name = towerName1;
    }
    public void setHealth(float nTowerHealth)
    {
        health = nTowerHealth;

    }
    public void setDamage(float nDamage)
    {
        damage = nDamage;
    }
    public void setCooldown(float nCooldown)
    {
        attackCooldown = nCooldown;
    }
    public void setPriority(int nPiority)
    {
        piority = nPiority;
    }
    public void setFireRate(float nFireRate)
    {
        fireRate = nFireRate;
    }

    public float getRate()
    {
        return fireRate;
    }

    public string getName()
    {
        return name;
    }
    public float getDamage()
    {
        return damage;

    }
    public float getHealth()
    {
        return health;

    }
    public int getPiority()
    {
        return piority;
    }
    public float getCooldown()
    {
        return attackCooldown;
    }

    public void initAttributes() //Initialise element and the tower strength
    {
        attributes = new Dictionary<string, bool>()
        {
            {"Nova", false},
            { "Quake", false },
            { "Crystal", false },
            { "Pure", false }
        };
    }

    public void modifyAttributes(string key, bool value) //Change element in the  dictionary using Key:Value
    {
        if (attributes.ContainsKey(key))
        {
            attributes[key] = value;
        }
        else Debug.LogError("Key not found.");
    }

    public bool getAttributes(string attribute) //search the dictionary and returnt he result if matches else return false.
    {
        bool def = false;
        bool result = attributes.TryGetValue(attribute, out def);
        return result;
    }

}
