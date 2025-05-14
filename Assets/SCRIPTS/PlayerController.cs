using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Creating a character controller for player
    public CharacterController playerController;
    //Setting up the camera and sensitivity
    [Header("Camera")] 
    public Transform playerCam;
    public float xSensitivity = 100f;
    public float ySensitivity = 100f;
    private float _maxRotation = 0f;
    
    //Setting movement speeds
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 5f;
    public float movementSpeed = 5f;
    
    //Creating variables for the gravity
    [Header("Gravity")] 
    public float gravity = -9.8f;
    public float gliderGravity = -5f;
    private float _gSpeed = 0;
    
    //Setting glide feature
    [Header("Glider")]
    public bool glider = false;
    public GameObject Glider;
    
    //Setting up a coins system
    [Header("Coins")] 
    public int playerCoins = 30;
    
    //Setting up a coins system
    [Header("MiniMap")] 
    public bool mapActive = false;
    public Transform MiniMap;
    public GameObject playerPos;
    
    //Setting up a coins system
    [Header("GunSelected")] 
    public int gunSelected = 1;
    
    //Setting up a Health system
    [Header("Health")] 
    public int playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    { 
        playerController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        movementSpeed = walkSpeed; 
        Cursor.visible = false;
    } 

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////////////////////////////////////////////////////
        //CAMERA MOVEMENT
        //Getting the mouse input from the user
        float mouseXaxis = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        float mouseYaxis = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        
        //Allowing the player to look around on the Xaxis (this should move the whole body)
        transform.Rotate(Vector3.up * mouseXaxis);
        
        //Setting the rotations of just the camera on the Y axis
        _maxRotation -= mouseYaxis;
        //Clamping the camera so it only goes 90 degrees on the Y axis
        _maxRotation = Mathf.Clamp(_maxRotation, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(_maxRotation, 0f, 0f);
        ///////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////
        //PLAYER MOVEMENT
        //Getting an input from the player (WASD)
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        
        //Making sure the camera is facing forward with the player (took a long time to get this right as camera would move but player controls would stay put)
        Vector3 fb = playerCam.forward;
        Vector3 lr = playerCam.right;
        fb.y = 0f;
        lr.y = 0f;
        fb.Normalize();
        lr.Normalize();
        
        //Checking movement direction
        Vector3 playermove = (lr * hInput + fb * vInput) * movementSpeed * Time.deltaTime;
        //Moving the player
        playerController.Move(playermove);
        //////////////////////////////////////////////////////////////////////////////////////
        
        //////////////////////////////////////////////////////////////////////////////////////
        //Creating a glider variable to set the right gravity whether the player is grounded or not
        if (playerController.isGrounded)
        {
            glider = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////////////////////////////////////////// 
        //ADDING GRAVITY TO THE PLAYER
        //Checking if the player is touching the ground,  if not, applying the full force gravity to pull it down, also applying glider gravity
        if (playerController.isGrounded && !glider)
        {
            _gSpeed = -1f;
        }

        else if (!playerController.isGrounded && !glider)
        {
            _gSpeed += gravity * Time.deltaTime;
        }
        //If the gliders active player gravity much lower
        else if (!playerController.isGrounded && glider)
        {
            _gSpeed = gliderGravity;
        }
        Vector3 gravityV = new Vector3(0, _gSpeed, 0);
        playerController.Move(gravityV * Time.deltaTime);
        //////////////////////////////////////////////////////////////////////////////////////
        
        //////////////////////////////////////////////////////////////////////////////////////
        //SPRINT VARIABLE
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }

        else
        {
            movementSpeed = walkSpeed;
        }
        ////////////////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////////////////////////////////////////
        //Creating glider function so glider is shown when true
        if (glider == true)
        {
            Glider.SetActive(true);
        }

        else
        {
            Glider.SetActive(false);
        }
        ////////////////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////////////////////////////////////////
        //CREATING MINIMAP
        //On keypress M, switch camera to minimap camera
        if (Input.GetKey(KeyCode.M)) 
        {
            mapActive = true;
        }

        else
        {
            mapActive = false;
        }

        
        if (mapActive == true)
        {
            playerCam.gameObject.SetActive(false);
            MiniMap.gameObject.SetActive(true);
            playerPos.SetActive(true);
        }

        else 
        {
            playerCam.gameObject.SetActive(true);
            MiniMap.gameObject.SetActive(false);
            playerPos.SetActive(false);
        }
        ////////////////////////////////////////////////////////////////////////////////////

        if (playerHealth <= 0)
        {
            playerCam.gameObject.SetActive(false);
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////
    //CREATING THE COINS AND HEALTH SUBTRACTION AND ADDITION SYSTEM
    //I found creating it this way was much easier, as it allows me to use other scripts to decide how many coins to add and subtract. This made it easier to buy other guns.
    public void MinusCoins(int coinMinus)
    {
        playerCoins -= coinMinus;
    }
    
    public void AddCoins(int coinAdd)
    {
        playerCoins += coinAdd;
    }

    public void AddHealth(int healthAdd)
    {
        playerHealth += healthAdd;
    }
    
    public void MinusHealth(int healthMinus)
    {
        playerHealth += healthMinus;
    }
        
    ////////////////////////////////////////////////////////////////////////////////////
}
