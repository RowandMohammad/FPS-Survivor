using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;

    [Header("State Speeds")]
    public float sprintSpeed = 10f;
    public float walkSpeed = 5f;
    public float crouchSpeed = 3f;

    [Header("State Heights")]
    public float crouchedHeightModifier = -0.5f;
    public float standingHeight;

    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;

    private bool isCrouching;

    [SerializeField] Transform cameraHeight;
    
    public CapsuleCollider playerCollider;


    void Awake()
    {
        CrouchingCamera c1 = new CrouchingCamera();
        standingHeight = c1.standingHeight;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GameObject.Find("Capsule").GetComponent<CapsuleCollider>();


    }
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            
            playerMovement.movementSpeed = Sprint(playerMovement.movementSpeed);


        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.movementSpeed = Walk(playerMovement.movementSpeed);

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Crouch(crouchedHeightModifier);
            playerMovement.movementSpeed = crouchSpeed;
            
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Crouch(standingHeight);
            playerMovement.movementSpeed = walkSpeed;
            

        }
    }

    public float Sprint(float currentSpeed)
    {
        currentSpeed = sprintSpeed;
        return currentSpeed;
        

    }
    public float Walk(float currentSpeed)
    {
        currentSpeed = walkSpeed;
        return currentSpeed;
        
    }

    public float Crouch(float height)
    {
        Vector3 lTemp = playerCollider.center;
        cameraHeight.localPosition = new Vector3(0f, height, 0f);
        if (!isCrouching)
        {
            isCrouching = true;
            playerCollider.height = 1f;
            lTemp.y -= 0.5f;
            playerCollider.center = lTemp;


        }
        else
        {
            isCrouching = false;
            playerCollider.height = 2f;
            lTemp.y *= 0f;
            playerCollider.center = lTemp;
        }

        return playerCollider.height;

    }
}
