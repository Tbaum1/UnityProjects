using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    //public Transform enemyPrefab;
    public Transform spawnPoint;

    public Text waveCountdownText;  //refrence to the coutdownTimer text
    public Text waveNumberText;    

    public float waveSpawnCounter = 15f;  //tracks time in between wave spawns
    private float spawnCountDown = 2f;  //initial time for first wave

    public GameMan gameManager;

    //public Enemy enemySimple;
    //public Enemy enemyBoss;
    //public Enemy enemyFast;

    private int waveNumber = 0;  //tracks the wave number

    private void Start()
    {
        EnemiesAlive = 0;
    }

    private void Update()
    {
        waveNumberText.text = "Wave " + waveNumber.ToString();  //sets the wave counter each frame

        //if an enemy is still on the board then do nothing else return out
        if (EnemiesAlive > 0)
        {
            return;  
        }

        //if the wave number is equal to the length of total waves array then user won, calls WinLevel function from the GameMan script
        if (waveNumber == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        
        //if the spawnCountDown timer has reached zero then spawns a wave
        if (spawnCountDown <= 0f)
        {
            StartCoroutine(SpawnWave());  //use the coroutine with the IEnumerator function SpawnWave to control respawn rates
            spawnCountDown = waveSpawnCounter;  //resets countDown to the waveCounter        
            return;
        }

        spawnCountDown -= Time.deltaTime;  //reduce countDown by 1 every second

        spawnCountDown = Mathf.Clamp(spawnCountDown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", spawnCountDown);  //use Mathf.Floor to get rid of decimal place

    }

    /// <summary>
    /// controls the spawning of the waves
    /// </summary>
    IEnumerator SpawnWave()
    {
        PlayerStats.Waves++;

        Wave wave = waves[waveNumber];  //get the index of the wave stored in the waves array
        CountEnemies(wave);
        


        //interate through the number of enemies in the wave
        for (int i =0; i < wave.enemySimpleCount; i++)
        {
            if (wave.enemySimplePrefab.name == "Enemy_Simple" || wave.enemySimplePrefab.name == "Enemy_Simple1")
            {
                SpawnEnemy(wave.enemySimplePrefab); //calls the SpawnEnemy function and passes the enemy prefab to use
                yield return new WaitForSeconds(1f / wave.enemySimpleRate);  //pauses the respawn of each enemy so they don't spawn on each other
            }            
        }

        for (int i = 0; i < wave.enemyFastCount; i++)
        {            
            if (wave.enemyFastPrefab.name == "Enemy_Fast" || wave.enemyFastPrefab.name == "Enemy_Fast1")
            {
                SpawnEnemy(wave.enemyFastPrefab); //calls the SpawnEnemy function and passes the enemy prefab to use
                yield return new WaitForSeconds(1f / wave.enemyFastRate);  //pauses the respawn of each enemy so they don't spawn on each other
            }
        }

        for (int i = 0; i < wave.enemyToughCount; i++)
        {
            if (wave.enemyToughPrefab.name == "Enemy_Tough" || wave.enemyToughPrefab.name == "Enemy_Tough1")
            {
                SpawnEnemy(wave.enemyToughPrefab); //calls the SpawnEnemy function and passes the enemy prefab to use
                yield return new WaitForSeconds(1f / wave.enemyToughRate);  //pauses the respawn of each enemy so they don't spawn on each other
            }
        }

        for (int i = 0; i < wave.enemyBossCount; i++)
        {
            if (wave.enemyBossPrefab.name == "Enemy_Boss" || wave.enemyBossPrefab.name == "Enemy_Boss1")
            {
                SpawnEnemy(wave.enemyBossPrefab); //calls the SpawnEnemy function and passes the enemy prefab to use
                yield return new WaitForSeconds(1f / wave.enemyBossRate);  //pauses the respawn of each enemy so they don't spawn on each other
            }
        }

        waveNumber++;  //increments the wave number
        if (waveNumber == waves.Length && EnemiesAlive == 0)
        {
            gameManager.WinLevel();
            this.enabled = false;  //disables this script when the level is won
        }
        Debug.Log("Incoming Wave");
    }    

    private void CountEnemies(Wave wave)
    {
        EnemiesAlive = wave.enemyBossCount + wave.enemyFastCount + wave.enemySimpleCount + wave.enemyToughCount;  //when enemy is spawned then add 1 to our EnemiesAlive counter in the WaveSpawn script
    }

    /// <summary>
    /// creates the enemy on the board
    /// </summary>
    void SpawnEnemy(GameObject enemy)
    {        
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);  //instantiate enemies at start/spawn location

        
        //enemy.AddHealth(waveNumber);
    }
}
