using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    //Creating variable for the bullet and gun
    public Transform gunBarrel;
    public GameObject bulletModel;
    public GameObject ammoText;
    //Setting the speed of the bullet to 30
    private float firespeed = 30f;
    private float ammoCount = 10;
    public float ammoLeft = 50;
    private bool reloading = false;
    private float reloadTime = 0f;
    
    // Update is called once per frame
    void Update()
    {
        //Getting mouse input from user
        if (Input.GetMouseButtonDown(0) && ammoCount > 0 )
        {
                //Creating the bullet, setting up the firing method and location
                var bullet = Instantiate(bulletModel, gunBarrel.position, gunBarrel.rotation);
                bullet.GetComponent<Rigidbody>().velocity = gunBarrel.forward * firespeed;
                ammoCount--;
        }

        if (Input.GetKey(KeyCode.R) && ammoCount < 10 && reloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        //Telling user they're out of ammo
        if (ammoLeft <= 0)
        {
            ammoText.SetActive(true);
            yield return new WaitForSeconds(2);
            ammoText.SetActive(false);
        }
        
        //Setting reloading to true so it wont reload again, and cant shoot while reloading
        reloading = true;
        //Making it take 2s to reload the gun (pistol different from M4)
        reloadTime = 2;
        yield return new WaitForSeconds(reloadTime);
        //Debug.Log("1");
        
        //ammoLeft is basically a count for how much ammo the player has left. This if statement is basically to say if the player has a full clip, reload it all
        //else reload only what the player has left
        if (ammoLeft > 10 && ammoCount <= 0)
        {
            ammoCount = 10;
            ammoLeft -= 10;
            reloading = false;
            //Debug.Log("2");
        }
        
        else if (ammoLeft > 10 && ammoCount > 0)
        {
            ammoLeft -= 10-ammoCount;
            ammoCount = 10;
            reloading = false;
            //Debug.Log("2");
        }
        
        //Example: if the user only has 8 ammo left, then it will only load 8 ammo into the gun 
        else
        {
            ammoCount = ammoLeft;
            ammoLeft = 0;
            reloading = false;
            //Debug.Log("3");
        }
    }
    
    
    public void addAmmo(int ammoAmount)
    {
        ammoLeft += ammoAmount;
    }
    
}
