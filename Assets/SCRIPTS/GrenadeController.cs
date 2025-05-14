using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    //Creating variable for the bullet and gun
    public Transform playerHand;

    public GameObject grenadeModel;

    //Setting the speed of the grenade to 10
    public float throwspeed = 10f;

    // Update is called once per frame
    void Update()
    {
        //Getting mouse input from user
        if (Input.GetKey(KeyCode.G))
        {
            //Creating the bullet, setting up the firing method and location
            var grenade = Instantiate(grenadeModel, playerHand.position, playerHand.rotation);
            grenade.GetComponent<Rigidbody>().velocity = playerHand.forward * throwspeed;
        }
    }
}