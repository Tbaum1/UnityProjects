using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    public Canvas healthCanvas;
    Quaternion rotation;

    public float smooth = 2f;  //variable to control how fast the enemy turns to face the next waypoint/target

    private Transform target;
    private Vector3 pos;

    private int wavepointIndex;  //instantiate variable for wavepointIndex

    private Enemy enemy;

    private void Awake()
    {
        rotation = healthCanvas.transform.rotation;
        healthCanvas.transform.rotation = rotation;
        
    }

    private void Start()
    {        
        enemy = GetComponent<Enemy>();  //gets the enemy prefab 
        target = Waypoints.waypoints[0];  //sets the waypoint/target for the enemy to start moving toward at start   
        pos.y -= 1f;
    }

    /// <summary>
    /// updates the movement of the enemy each frame
    /// </summary>
    private void Update()
    {
        Vector3 direction = target.position - transform.position;  //direction to point to move toward waypoint by minus the object position from the target position
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * smooth);  //rotates the enemy so that it faces the next target position while moving
        healthCanvas.transform.rotation = rotation;

        //Translate moves object transform distance and direction of translation
        //.normalized makes it so that the enemy moves at a constant rate
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(transform.position, target.position) <= .2f)
        {
            GetNextWaypoint();  //calls function GetNextWaypoint
        }

        enemy.speed = enemy.startSpeed;  //sets the start speed of the enemy
    }
    
    /// <summary>
    /// gets and sets the next waypoint for the target
    /// </summary>
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return; //exit if
        }
        wavepointIndex++;  //increments wavepointIndex by 1
        target = Waypoints.waypoints[wavepointIndex];  //sets target to Waypoints script waypoints array index
    }

    /// <summary>
    /// when an enemy makes it to the end point this function is called to handle the event
    /// </summary>
    void EndPath()
    {
        PlayerStats.Lives--;  //take away one live
        WaveSpawn.EnemiesAlive--;  //when enemy reaches end point then take away 1 from our EnemiesAlive counter in the WaveSpawn script
        Destroy(gameObject);  //destroy enemy
    }
}
