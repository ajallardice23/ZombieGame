using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private PlayerController pController;
    private M4Controller m4Controller;
    private GunController pistolController;
    private int ammoLife = 15;
    private int ammoAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        m4Controller = FindObjectOfType<M4Controller>();
        pistolController = FindObjectOfType<GunController>();
        Destroy(gameObject, ammoLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Note for future self (just spent ages trying to get this working then realised I had the gun controllers opposite lol). If its null it will stop the whole code.
            if (pController.gunSelected == 1)
            {
                pistolController.addAmmo(ammoAmount);
            }
            
            if (pController.gunSelected == 2)
            {
                m4Controller.addAmmo(ammoAmount);
            }
            Destroy(gameObject);
        }
    }
}
