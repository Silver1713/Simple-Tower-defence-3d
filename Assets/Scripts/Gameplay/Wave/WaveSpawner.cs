using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class WaveSpawner : MonoBehaviour
{
    [Header("Settings")]
    //Initialization of variable and configuration
    //Allow spawn variable to be managed by unity inspector.
    public Text text;
    public float numberOfWave;
    private GameObject EnemyContainers;
    private int enemyPerSpawn;
    private float spawnBetweenWave = 5f;
    public float spawnTimeMin = 5f;
    public float spawnTimeMax = 10f;
    int currentWave = 0;
    public float waitPerSpawn = 0.2f;
     public Transform enemyToSpawn;
    public Transform enemyFolder;
    public int enemySpawnScale;
    ObjectiveManager objective;
    public GameObject defenceObj;
    


    
    
    void Start() // set the ui to display the game information.
    {
        objective = defenceObj.GetComponent<ObjectiveManager>();
        text.text = "Time: 0\nWave: 1";
        enemyPerSpawn = 1;
        EnemyContainers = new GameObject("Enemies"); // Create a folder for enemy to spawn in.
    }

    
    void Update()
    {
        if (spawnBetweenWave < 0) // This check if spawn is smaller then 0
        {
            spawnBetweenWave = Random.Range(spawnTimeMin, spawnTimeMax); // Generate random wait time based on the configured min and max.
            if (currentWave == numberOfWave)
            {
                StopAllCoroutines(); 
            } else
            {
                StartCoroutine(Spawn()); // Allow enemy to be spawn.
                

            }
            

           
           
        }

        if (currentWave != numberOfWave)
        {
            
            if (currentWave == 0)
            {
                text.text = "Time: " + Mathf.Floor(spawnBetweenWave).ToString() + "\n" + "Wave: " +"Intermission";
            } else
            {
                
                text.text = "Time: " + Mathf.Floor(spawnBetweenWave).ToString() + "\n" + "Wave: " + currentWave.ToString() + "/" + numberOfWave.ToString();
            }
            spawnBetweenWave -= Time.deltaTime;

        } else
        {
            //Check for winning conditions
            text.text = "Time: " + Mathf.Infinity.ToString() + "\n" + "Wave: " + "Boss Fight";
            if (EnemyContainers.transform.childCount == 0)
            {
                GameManager.gameManager.playerHealth = objective.objectiveHealth;
                GameManager.gameManager.WinGame();
            }
        }
    }

   

    IEnumerator Spawn()
    {
        currentWave++;
        enemyPerSpawn = currentWave * enemySpawnScale;
        
        
       
        if (numberOfWave != currentWave) //if the currentwave is not the number of wave continue spawning enemy 
        {
            Debug.Log(currentWave);
            for (int i = 0; i < enemyPerSpawn; i++)
            {
                // Find a way to get the enemy
                enemyToSpawn = getEnemy(false);
                GameObject spawnEnemy = Instantiate(enemyToSpawn.gameObject, transform.position, Quaternion.identity, EnemyContainers.transform) as GameObject;
                spawnEnemy.name = "Enemy - " + System.Convert.ToString(i + 1);
                spawnEnemy.SetActive(true);
                yield return new WaitForSeconds(waitPerSpawn);

            }
        } else
        {
            for (int z = 0; z < enemyPerSpawn; z++) //if it is the number of ways and if there are no enemies , user have won.
            {
                // Find a way to get the enemy
                if (z == enemyPerSpawn - 1)
                {
                    enemyToSpawn = getEnemy(true);
                }
                GameObject spawnEnemy = Instantiate(enemyToSpawn.gameObject, transform.position, Quaternion.identity, EnemyContainers.transform) as GameObject;
                spawnEnemy.name = "Enemy - " + System.Convert.ToString(z + 1);
                spawnEnemy.SetActive(true);
                yield return new WaitForSeconds(waitPerSpawn);

            }
        }
       

    }

    

    Transform getEnemy(bool isBoss) // get a random enemy in the enemy folder to spawn it.
    {
     if (isBoss != true) // Check if it a boss
        {
            Transform randomEnemy = enemyFolder.GetChild(Random.Range(0, enemyFolder.childCount - 1));
            return randomEnemy;

        } else
        {
            Transform BossEnemy = enemyFolder.GetChild(enemyFolder.childCount - 1);
            return BossEnemy;
        }
        
    }
}
