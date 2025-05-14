using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private PlayerController pController;
    private int coinCost = 2;

    private int coinLife = 15;
    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        Destroy(gameObject, coinLife);
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.AddCoins(coinCost);
            Destroy(gameObject);
        }
    }
}
