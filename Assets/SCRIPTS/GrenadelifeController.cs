using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadelifeController : MonoBehaviour
{
    public float grenadelife = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, grenadelife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
