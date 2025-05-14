using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrierdelete : MonoBehaviour
{
    
    public GameObject BuyText;
    public GameObject NoCoinText;
    private PlayerController pController;
    private int barrierCost = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        BuyText.SetActive(false);
        pController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //When player enters the box collider, it will show them the buy
            BuyText.SetActive(true);
            //If player presses "E", and has enough money, it will buy the gun
            if (Input.GetKey(KeyCode.E) && pController.playerCoins >= barrierCost)
            {
                //Minus user coins and delete barrier
                pController.MinusCoins(barrierCost);
                NoCoinText.SetActive(false);
                BuyText.SetActive(false);
                Destroy(gameObject);
            }
            //If player does not have enough coins, do not delete barrier
            if (Input.GetKey(KeyCode.E) && pController.playerCoins < barrierCost)
            {
                NoCoinText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BuyText.SetActive(false);
        NoCoinText.SetActive(false);
    }
}
