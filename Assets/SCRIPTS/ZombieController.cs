using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum State
{
    Idle, Wander, Seek, Attack, Knocked, Death
}
public class ZombieController : MonoBehaviour
{ 
    //Setting player and creating NavMesh for zombie
    public Transform player;
    private NavMeshAgent zombieNav;
    //Setting Animator
    private Animator zombieAnimator;
    //Setting zombies state
    private State enemyState;
    //Setting health speed etc
    private int zombieHealth = 100;
    private int zombieSpeed = 3;
    private float attackCooldown = 0f;
    private float zombieRange = 10f;
    private float zombieAttackrange = 2f;
    //ZombieDrops
    public GameObject gliderModel;
    public GameObject coinModel;
    public GameObject ammoModel;
    public GameObject skullModel;
    public GameObject healthModel;
    
    // Start is called before the first frame update
    void Start()
    {
        //Getting NavMesh from Zombie
        zombieNav = GetComponent<NavMeshAgent>();
        PlayerController pcontroller = GameObject.FindObjectOfType<PlayerController>();
        enemyState = State.Seek;
        zombieAnimator = GetComponent<Animator>();
        //Had to use this as I couldn't attach transform in inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        //State Machine
        switch (enemyState)
        {

            case State.Idle: 
                Idle();
                break;
            
            case State.Wander:
                Wander();
                break;

            case State.Seek:
                Seek();
                break;  

            case State.Attack:
                Attack();
                break;
            
            case State.Knocked: 
                Knocked();
                break;
            
            case State.Death: 
                Death();
                break;
        }
        
        float playerdistance = Vector3.Distance(transform.position, player.position);
        PlayerController pcontroller = GameObject.FindObjectOfType<PlayerController>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }
        //Saying death on 0 health
        if (zombieHealth <= 0)
        {
            enemyState = State.Death;
        }
        //Nerfing the zombies when below a certain health
        if (zombieHealth < 10 && zombieHealth > 0)
        {
            enemyState = State.Knocked;
        }
    }
    
    void Idle()
    {
        
    }
    
    void Wander()
    {
        float playerdistance = Vector3.Distance(transform.position, player.position);
        zombieAnimator.SetBool("isSeek", true);
        
        if (zombieRange <= playerdistance && zombieAttackrange > playerdistance) {
            enemyState = State.Seek;
        }

        if (zombieAttackrange < playerdistance)
        {
            enemyState = State.Attack;
        }
    }
    
    void Seek()
    {
        //Seeking player
        float playerdistance = Vector3.Distance(transform.position, player.position);
        zombieNav.SetDestination(player.position);
        zombieNav.speed = zombieSpeed;
        //Animations bool
        zombieAnimator.SetBool("isSeek", true);
        zombieAnimator.SetBool("isBite", false);
        zombieAnimator.SetBool("isStrike", false);
        zombieAnimator.SetBool("isDive", false);
        if (zombieRange < playerdistance) {
            enemyState = State.Wander;
        }
        
        else if (zombieAttackrange >= playerdistance) {
            enemyState = State.Attack;
        }
    }
    
    void Attack()
    {
        float playerdistance = Vector3.Distance(transform.position, player.position);
        
        if (zombieRange >= playerdistance && zombieAttackrange < playerdistance) {
            zombieAnimator.SetBool("isSeek", true);
            enemyState = State.Seek;
        }
        //Attack cooldown and selecting an attack, showing animation for this attack
        if (attackCooldown <= 0)
        {
            zombieNav.SetDestination(player.position);
            Debug.Log("Test");
            zombieAnimator.SetBool("isSeek", false);
            zombieAnimator.SetBool("isBite", false);
            zombieAnimator.SetBool("isStrike", false);
            zombieAnimator.SetBool("isDive", false);
            int randInt = Random.Range(1, 3);
            //Getting a random number between 1-3 and picking it for the attack
            if (randInt == 1)
            {
                zombieAnimator.SetBool("isBite", true);
            }
            
            else if (randInt == 2)
            {
                zombieAnimator.SetBool("isStrike", true);
            }
            
            else if (randInt == 3)
            {
                zombieAnimator.SetBool("isDive", true);
            }
            attackCooldown = 2;
        }

        else
        {
            attackCooldown -= Time.deltaTime;
        }
        
        
    }
    
    void Knocked()
    {
        zombieSpeed = 1;
        zombieAnimator.SetBool("isKnocked", true);
    }

    void Death()
    {
        //Creating a chance of item/perk dropping
        int DropChance = Random.Range(1, 3);
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        if (DropChance == 2)
        {
            Vector3 zombiePosition = transform.position;
            //Selecting which item drops by random number
            int DropSelect = Random.Range(1, 4);
            //Creating the models based on the selection. Essentially this is just saying create(Model, at zombie (+1 height because it spawns at feet), standard rotation)
            if (DropSelect == 1)
            {
                var gliderDrop = Instantiate(gliderModel, new Vector3 (zombiePosition.x, zombiePosition.y + 1, zombiePosition.z), Quaternion.identity);
            }

            if (DropSelect == 2)
            {
                var coinDrop = Instantiate(coinModel, new Vector3 (zombiePosition.x, zombiePosition.y + 1, zombiePosition.z), Quaternion.identity);
            }
            
            if (DropSelect == 3)
            {
                var skullDrop = Instantiate(skullModel, new Vector3 (zombiePosition.x, zombiePosition.y + 1, zombiePosition.z), Quaternion.identity);
            }
            
            if (DropSelect == 4)
            {
                var healthDrop = Instantiate(healthModel, new Vector3 (zombiePosition.x, zombiePosition.y + 1, zombiePosition.z), Quaternion.identity);
            }
        }

        else
        {
            Vector3 zombiePosition = transform.position;
            var ammoDrop = Instantiate(ammoModel, new Vector3 (zombiePosition.x, zombiePosition.y + 1, zombiePosition.z), Quaternion.identity);
        }
        Destroy(gameObject);
        gameController.ZombieDeath();
    }

    //TakeDamage Script
    public void TakeDamage(int damageAmount)
    {
        zombieHealth -= damageAmount;
    }

    //Damage to player if collider hits. Zombie has a collider on hands and head due to attacks
    private void OnCollisionEnter(Collision other)
    {
        PlayerController pcontroller = GameObject.FindObjectOfType<PlayerController>();
        if (other.gameObject.tag == "Player")
        {
            pcontroller.playerHealth -= 10;
        }
    }
}
