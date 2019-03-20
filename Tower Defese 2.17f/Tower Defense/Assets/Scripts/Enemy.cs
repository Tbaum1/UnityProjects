using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f; //sets the startSpeed of enemy

    [HideInInspector]  //hides the variable in the inspector but be serialized
    public float speed = 10f; //sets the speed of enemy

    public float startHealth = 100f;
    public float health = 0f;    
    
    public int coinsOnEnemyDeath = 50;  //amount of coins an enemy pays

    public GameObject deathEffect;

    private bool isDead = false;  //tracks if the enemy is dead or alive

    [Header("Unity Stuff")]
    public Image healthbar;

    private void Start()
    {
        health = startHealth;
        speed = startSpeed; //when game starts set enemy startSpeed
    }

    /// <summary>
    /// handles the damage of enemies and sets the healthBar
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        Debug.Log("Health before hit" + health);
        health -= amount;  //takes away the amount of damage from the enemy health

        healthbar.fillAmount = health / startHealth;  //sets how much the health bar is filled
        Debug.Log("Health left: " + health);  

        //checks to see if enemy health is less than or equal to zero and dead
        if (health <= 0 && !isDead)
        {
            Debug.Log("Helath at zero");
            EnemyDie();
        }
    }

    /// <summary>
    /// handles the the destruction of enemy and coins earned upon its death
    /// </summary>
    void EnemyDie()
    {
        isDead = true;  //sets isDead to true to let the game know enemy is dead

        PlayerStats.Coins += coinsOnEnemyDeath;  //adds coins to the players total when en enemy dies

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);  //plays the deathEffect for the enemy that died
        Destroy(effect, 3f);

        WaveSpawn.EnemiesAlive--;  //when enemy dies then take away 1 from our EnemiesAlive counter in the WaveSpawn script

        Destroy(gameObject);
    }

    /// <summary>
    /// handles the slowing down of the enemy target while being shot by the laserTurret per/frame
    /// </summary>
    /// <param name="slowAmount"></param>
    public void Slow(float slowAmount)
    {
        speed = startSpeed * (1f - slowAmount);  //lowers the enemies speed only from the start speed and not the curret speed so the effect does't stack, keeps steady percent decrease
    }   
}
