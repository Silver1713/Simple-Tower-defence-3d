using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //Initialization of enemy data.
    //This allow enemyManager to be stored which allow us to get the enemu
    private enemyManager enemyHandle;
    private Transform currentTarget;
    public float projectileSpeed;
    public int projectileType;
    public bool straight;
    public float AoERadius = 0;
    public string targetLayer;
     List<string> element;
    Vector3 direction;
    public Transform towerObject;
   private TowerManager tower;


    
    void Start() //get the tower manager script and it element.
    {
        tower = towerObject.GetComponent<TowerManager>();
        element = tower.element;
    }

   
    void Update() //Check if there is a target and handle the bullet movement.
    {
        
            if (currentTarget == null)
            {
                Destroy(transform.gameObject);
                return;
            }
             direction = currentTarget.transform.position - transform.position;
            float currentFrameDistance = projectileSpeed * Time.deltaTime;
            if (direction.magnitude <= currentFrameDistance)
            // if the distance of the enemy to the projectile is smaller then the distance that the bullet is going to move in  the currentframe.
            //The bullet has hit the target.
            {
                

                targetHit();
            }
        transform.LookAt(currentTarget);
        if (straight)
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime, Space.Self);
        } else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
        
    }

    public void SeekAt(Transform target = null) //If the target is null remove the bullet.
    {
        if (target == null)
        {
            Destroy(gameObject);
            
            return;
        }

        currentTarget = target;
    }

    void targetHit() //Thsi allow different type of attack to be initalized under the same script
    {
        if (projectileType == 1) 
        {
            if (currentTarget != null)
            {
                if (element != null)
                {
                    damageTarget(tower.towerDamage, currentTarget, element);
                } else
                {
                    damageTarget(tower.towerDamage, currentTarget);
                }
            }
            Destroy(gameObject);
            return;
        }

        if (projectileType == 2) 
        {
            //Using overlap sphere to detect collider on enemy layer detuct their health based on the damage.
            Collider[] detectedEnemy = Physics.OverlapSphere(transform.position, AoERadius, 1 << LayerMask.NameToLayer(targetLayer), QueryTriggerInteraction.UseGlobal);
            foreach (Collider enemyCollider in detectedEnemy)
            {
                if (detectedEnemy != null)
                {
                    if (element != null)
                    {
                        damageTarget(tower.towerDamage, enemyCollider.transform, element);
                    }
                    else
                    {
                        damageTarget(tower.towerDamage, enemyCollider.transform);
                    }
                    Debug.Log("Run");
                }
            }
            Destroy(gameObject);
            return;


        }
        

 
        
        
    }

    void damageTarget(float nDamage, Transform target, List<string> optionalElement = null)  //Allow target to be damage
    {
        if (currentTarget != null)
        {
            enemyHandle = target.GetComponent<enemyManager>();
            enemyHandle.TakeDamage(nDamage, optionalElement.ToArray());
        }
    }

    
}
