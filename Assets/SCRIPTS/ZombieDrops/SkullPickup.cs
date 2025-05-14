using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPickup : MonoBehaviour
{

    private PlayerController pController;
    private BulletController bulletController;
    private int skullLife = 15;

    // Start is called before the first frame update
    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        bulletController = FindObjectOfType<BulletController>();
        Destroy(gameObject, skullLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Skull());
        }
    }
    //Insta kill (just make bullets high damage for 10s)
    private IEnumerator Skull()
    {
        bulletController.AddDamage(100);
        yield return new WaitForSeconds(10);
        bulletController.MinusDamage(100);
        Destroy(gameObject);
    }
}
