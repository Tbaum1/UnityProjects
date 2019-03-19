using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public GameObject impactEffect;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public float damage = 50;
    
    /// <summary>
    /// sets the target to the current target of the turret
    /// </summary>
    /// <param name="_target"></param>
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //if target is gone bullet gets destroyed
        if (target == null)
        {
            Destroy(gameObject);
            return; //gives destroy time to finish
        }

        Vector3 dir = target.position - transform.position; //distance from bullet to target
        float distanceThisFrame = speed * Time.deltaTime;  //speed at which bullets moves

        //if true then bullet hit something
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);  //moves bullet toward enemy
        transform.LookAt(target);  //makes bullet/missile lookat at the enemy

    }

    /// <summary>
    /// handles the process of determing if a enemy got hit and which enemy
    /// </summary>
    void HitTarget()
    {
        GameObject effectInsance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInsance, 5f);

        if (explosionRadius > 0f)
        {
            Explode();  //handles the explosion radius for projectiles
        }
        else
        {
            Damage(target);  //applies damage to the enemy
        }
        
        Destroy(gameObject);  //destroys the bullet
        Debug.Log("Hit Enemy");
    }

    /// <summary>
    /// handles the effects of explosions
    /// </summary>
    void Explode()
    {
        Collider[] radiusColliders = Physics.OverlapSphere(transform.position, explosionRadius);  //creates Collider array to hold the objects that are in the radius of the explosion
        foreach (Collider collider in radiusColliders)
        {
            //if projectile hits object tagged Enemy it will destroy it
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform); 
            }
        }
    }

    /// <summary>
    /// when projectile damages a enemy this function is called to destroy the enemy
    /// </summary>
    /// <param name="enemy"></param>
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }                  
    }

    /// <summary>
    /// show the radius of the explosion
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
