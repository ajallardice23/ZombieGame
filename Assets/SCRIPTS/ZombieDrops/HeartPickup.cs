using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private PlayerController pController;
    private int heartLife = 15;

    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        Destroy(gameObject, heartLife);
    }
    //On trigger enter (when player walks into it), add health to player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.AddHealth(25);
            Destroy(gameObject);
        }
    }
}
