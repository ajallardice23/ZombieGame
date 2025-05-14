using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BulletController : MonoBehaviour
{
    public float bulletlife = 2;
    public float bulletpush = 8;
    public int bulletdamage = 30;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletlife);
    }
    
    void OnCollisionEnter(Collision collision) 
    {
        //Detecting if it has a NavMesh (if Zombie)
        NavMeshAgent enemyNavmesh = collision.collider.GetComponent<NavMeshAgent>();
        //Detecting if its hit a rigidbody and pushing it if it has
        Rigidbody collisionObject = collision.collider.GetComponent<Rigidbody>();
        if (collisionObject == null)
        {
            
        }

        else
        { 
            //Push object on collision if rigidbody
            collisionObject.AddForce(transform.forward * bulletpush, ForceMode.Impulse);
        }
        
        Damage(collision.gameObject);
        
        Destroy(gameObject);
    }

    //Setting up damage for zombie
    void Damage(GameObject Zombie)
    {
        //Accessing zombie script
        ZombieController zController = Zombie.GetComponent<ZombieController>();
        //Basically saying if theres a zombie, access the bulletdamage in the zombiescript
        if (zController != null)
        {
            zController.TakeDamage(bulletdamage);
        }
    }
    
    public void AddDamage(int damageAdd)
    {
        bulletdamage += 100;
    }
    
    public void MinusDamage(int damageMinus)
    {
        bulletdamage -= 100;
    }
    
}
