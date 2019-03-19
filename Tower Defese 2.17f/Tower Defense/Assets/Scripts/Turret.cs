using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    //Quaternion originalRotation; //instantiate quaternion for the original turret position

    [SerializeField]
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;  //range variable
    
    [Header("Use Bullets (defualt)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;  //rate of fire
    private float fireCountdown = 0f; //countdown between firing

    [Header("Use Laser")]
    public bool useLaser = false;  //sets using laser to false 
    public int damageOverTime = 30;  //sets the damage over time to 30/sec
    public float slowAmount = .5f;  //sets the amount the laserTurret slows the target
    public LineRenderer lineRenderer;  
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Field")]
    public string enemyTag = "Enemy";  //tag for enemies
    public Transform rotateTurret;  //instantiate rotateTurret transfom
    public float turnSpeed = 10f;  //turn speed of the turret


    
    public Transform[] firePoint;


    private void Start()
    {
        //originalRotation = rotateTurret.rotation;  //sets the original rotation of the turret
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //updates target 
    }

    private void Update()
    {
        //if no target don't do anything
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            //rotateTurret.rotation = Quaternion.Lerp(rotateTurret.rotation, originalRotation, Time.time * turnSpeed);  //set the turretRotation back to the original rotation if no target
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;  //lets us fire 2 bullets each second
            }

            fireCountdown -= Time.deltaTime; //makes sure each frame bullet is reduced by 1
        }     
    }

    /// <summary>
    /// shoots the turrets
    /// </summary>
    void Shoot()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);  //casting prefab as gameobject and saving it as gameobject
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
            Debug.Log("Shoot");
        }
        
    }

    void LockOnTarget()
    {
        //target lock on enemy
        Vector3 enemyDir = target.position - transform.position; //gets the direction from the enemy to the turret
        Quaternion lookRotation = Quaternion.LookRotation(enemyDir);  //gets the lookRotation of the turret
        Vector3 turretRotation = Quaternion.Lerp(rotateTurret.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;  //puts the lookRotation into the turretRotation eulerAngles(degrees of which it turns on the Y axis) using the Lerp function(smooths transition/rotation)
        rotateTurret.rotation = Quaternion.Euler(0f, turretRotation.y, 0f); //sets the y rotation of the turret and zero rotation to the x and z
    }

    /// <summary>
    /// handles the lineRenderer for the laser turret
    /// </summary>
    void Laser()
    {
        targetEnemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);  //sends the damage to the enemy per second to the TakeDamage function in the Enemy script
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;  //sets lineRenderer enabled to true makes the laser visible
            impactEffect.Play();  //plays the impactEffect
            impactLight.enabled = true;  //turns on the light for the impact effect
        }
        lineRenderer.SetPosition(0, firePoint[0].position);
        //lineRenderer.SetPosition(0, firePoint[0].position);  //sets the position for element 0 to the firePoint of turret
        lineRenderer.SetPosition(1, target.position);  //set the position for element 1 to the target enemy's position to complete the line

        Vector3 dir = firePoint[0].position - target.position;  //get the distance to the target from the turret

        impactEffect.transform.position = target.position + dir.normalized;  //sets the  position of the impactEffect

        impactEffect.transform.rotation = Quaternion.LookRotation(dir); //rotates the impact dir toward the turret
        
    }

    /// <summary>
    /// search through objects marked enemy, find closets one then set that object as target
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);  //create GameObject array to hold objects tagged Enemy
        float shortestDistance = Mathf.Infinity;  //if no enemy found then it has infinit distance to the enemy
        GameObject nearestEnemy = null;  //variable for the nearestEnemy

        foreach (GameObject enemy in enemies)
        {
            //set the distance to the enemy from the turret
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //see if the distance to the enemy is closer than the shortestDistance
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;  //sets shortestDistance to the closest enemy
                nearestEnemy = enemy;  //sets the nearest enemy object
            }
        }

        //sets the target to the nearestEnemy by checking if nearestEnemy is not equal to shortestDistance if its in range of turret
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;  //sets target to the nearestEnemy transform
            targetEnemy = nearestEnemy.GetComponent<Enemy>();  //sets the targetEnemy to the nearestEnemy
        }
        else
        {
            target = null;  //sets target to null
        }
    }

    /// <summary>
    /// creates a wireSphere around the turret, the turrets range for shooting targets
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  //change Gizmos colors to red
        Gizmos.DrawWireSphere(transform.position, range);  //draw wireSphere on turret and set range
        
    }
    
}
