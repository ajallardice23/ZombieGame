using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderPickup : MonoBehaviour
{
    private PlayerController pController;

    private int gliderLife = 15;

    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        Destroy(gameObject, gliderLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterController cController = other.GetComponent<CharacterController>();
            cController.Move(Vector3.up * 20f);
            pController.glider = true;
            Destroy(gameObject);
        }
    }
}