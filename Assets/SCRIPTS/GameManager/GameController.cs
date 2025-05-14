using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject zombie;
    public int randX = 1;
    public int randZ = 1;
    public int gameRound = 1;
    public int zombieCount = 1;
    public int zombieSpawn = 1;
    public int zombieroundSpawn = 0;
    public float roundTime = 0;
    public float previousroundTime = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //If the zombie dies, and theres none left it goes to next round
   public void ZombieDeath()
   {
       zombieCount--;
       
        if (zombieCount <= 0)
        {
            NextRound();
            previousroundTime = roundTime;
            roundTime = 0;
        }
    }
    
    void NextRound()
    {
        //Gameround and number in the debug log
        Debug.Log("Next Round");
        Debug.Log("Round- " + gameRound.ToString());
        //Next round
        gameRound++;

        //Increasing zombie spawn rate based on how well a player does the previous round. This is to help the player and add some AI feature.
        //Less than 30s spawn 3
        if (previousroundTime < 30)
        {
            zombieSpawn += 3;
        }
        //Less than 60s spawn 2
        else if (previousroundTime > 30 && previousroundTime < 60)
        {
            zombieSpawn += 2;
        }
        //More than 60s spawn 1
        else if (previousroundTime > 30 && previousroundTime > 60)
        {
            zombieSpawn += 1;
        }


        //Spawning zombies in random locations in the square
        zombieroundSpawn = zombieSpawn;
        while (zombieroundSpawn > 0)
        {
            //Getting random spawn points for the zombies
            randX = Random.Range(74, 94);
            randZ = Random.Range(42, 60);
            //Counting down amount of zombies spawning this round
            //Counting down amount of zombies spawning this round
            zombieroundSpawn--;
            Instantiate(zombie, new Vector3(randX, -33, randZ), Quaternion.identity);
        }
        
        zombieCount = zombieSpawn;
        
        roundTime += Time.deltaTime;
    }
}
