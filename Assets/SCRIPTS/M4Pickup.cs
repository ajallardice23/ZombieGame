using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4Pickup : MonoBehaviour
{
    //Getting the guns on the player
    public GameObject GunOnPlayer;
    public GameObject M4OnPlayer;
    //Getting the text for the gun and coins
    public GameObject M4Text;
    public GameObject NoCoinText;

    public GameObject GunEquippedText;
    //Setting the cost for the M4 as 10 coins
    private int M4Cost = 25;
    //Seting variable which I will use for player script
    private PlayerController pController;
    
    // Start is called before the first frame update
    void Start()
    {
        M4Text.SetActive(false);
        pController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //When player enters the box collider, it will show them the M4Text
            M4Text.SetActive(true);
            //If player presses "E", and has enough money, it will buy the gun
            if (Input.GetKey(KeyCode.E) && pController.playerCoins >= M4Cost && !M4OnPlayer.activeSelf)
            {
                //Minus user coins and give M4
                pController.MinusCoins(M4Cost);
                GunOnPlayer.SetActive(false);
                M4OnPlayer.SetActive(true);
                pController.gunSelected = 2;
            }
            //If player does not have enough coins, do not give the player the gun
            if (Input.GetKey(KeyCode.E) && pController.playerCoins < M4Cost && !M4OnPlayer.activeSelf)
            {
                NoCoinText.SetActive(true);
            }
            //If player already has the gun, tell them they have gun already
            if (Input.GetKey(KeyCode.E) && M4OnPlayer.activeSelf)
            {
                GunEquippedText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        M4Text.SetActive(false);
        NoCoinText.SetActive(false);
        GunEquippedText.SetActive(false);
    }
}
