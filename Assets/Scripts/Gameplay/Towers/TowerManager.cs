using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    //Initialization of tower statistic and it configurations.
    private Tower towerData;
    public int numAttack;
    public string layerName; //Layer name for Overlap sphere to detect enemies.
    public Transform currentTarget;
    public Transform firePosition;
    private GameObject projFolder;
    public int minRequisite;

    [Header("Tower Data")]
    public float Cost;
    public float attackRadius;
    public float spinSpeed;
    public string towerName;
    public float towerHealth;
    public float towerDamage;
    public float towerCoolDown;
    public float towerFireRate;
    public List<string> element;
    public int towerPiorities;
    public GameObject towerProjectiles;
   
   //This initialize variables in the Tower class and get important element like the FirePosition under it children
    void Start()
    {
        
        towerData = new Tower(Cost,transform.gameObject, towerName, towerHealth, towerDamage, towerFireRate, towerCoolDown, towerPiorities);
        towerData.initAttributes();
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "FirePosition")
                {
                    firePosition = transform.GetChild(i);
                }
                if (transform.GetChild(i).name.Substring(transform.GetChild(i).name.Length -4) == "Proj") //This allow projectiles to be retrieved.
                {
                    towerProjectiles = transform.GetChild(i).gameObject;
                }
            }
        }

       if (GameObject.Find("Projectiles") == null) //If there are no folder called projectiles create a new instance.
        {
            projFolder = new GameObject("Projectiles");

        } else
        {
            projFolder = GameObject.Find("Projectiles"); ///Assign it if found.
        }
    }

    public bool getElement(string Element) //Retrieve the element in the Tower class
    {
        return towerData.getAttributes(Element);

    }
   public void modifyElement(string elements, bool isAttributes) //modify the element in the tower class in a dictionary using Key: Value
    {
        towerData.modifyAttributes(elements, isAttributes);
    }

    
    void Update()
    {
        Collider[] collide = Physics.OverlapSphere(transform.position, attackRadius, 1 << LayerMask.NameToLayer(layerName));//Allow a range to be set up to detect enemies
        // As it is being cast in the enemy layer
        foreach (Collider enemy in collide) // Retrieve the enemy in the radius
        {
            Debug.Log("Detected");
            GameObject target = enemy.gameObject; 
            
            if (currentTarget == null)
            {
                currentTarget = target.transform;
            }
            if (currentTarget != null)
            {
                bool result = false;
                foreach (Collider entities in collide)
                {
                    if (entities.transform == currentTarget)
                    {
                        result = true;
                        break;
                    }
                  
                }
                if (result == false)
                {
                    currentTarget = null;
                }
            }
            
        }
        

        if (currentTarget != null && collide.Length == 0)
        {
            currentTarget = null;
        }
        
        if (currentTarget != null)
        {

            Vector3 direction = currentTarget.position - transform.position; //Get the directions of the enemy
            Quaternion lookDirection = Quaternion.LookRotation(direction); //Get the look rotation of the enemy , allow it to point toward the enemy
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookDirection, spinSpeed * Time.deltaTime).eulerAngles; //Lerp it to make it more smoother.
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotation.y, 0f); //get Eular transformation data.

            if (towerCoolDown <= 0) //If there is no more cooldown
            {
                ShootAt(currentTarget); // Fire at enemy
                towerCoolDown = 1f / towerData.getRate();

            }
            towerCoolDown -= Time.deltaTime;
        }

    }

    public bool CheckIfPurchasable(int currentLevel) //Check if tower is purchasable based on your upgrade level.
    {
        return (currentLevel >= minRequisite);
    }

    void ShootAt(Transform target = null) //Shoot at enemy.
    {
        Debug.Log("Shoot");
        GameObject proj = Instantiate(towerProjectiles, firePosition.position, firePosition.rotation, projFolder.transform) as GameObject;
        proj.SetActive(true);
        proj.transform.localScale = firePosition.localScale;
        Projectiles projectile = proj.GetComponent<Projectiles>();
        projectile.SeekAt(currentTarget); //This call seekAt inside projectiles.
        

    }

   

    
      
    
    
}
