using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PressureController : MonoBehaviour
{
    private float forcePlayer;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player steps on pressure force player in air
        if (other.gameObject.tag == "Player")
        {
            //Random range
            int randInt = Random.Range(5, 15);
            forcePlayer = randInt;
            //Player in air
            CharacterController playerController = other.GetComponent<CharacterController>();
            playerController.Move(Vector3.up * forcePlayer);
        }
    }
    
}
