using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public float sprintSpeed = 10f;
    public float walkSpeed = 6f;
    public float crouchSpeed = 2f;


    void Awake()
    {

        

    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            playerMovement.movementSpeed = Sprint(playerMovement.movementSpeed);


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerMovement.movementSpeed = Walk(playerMovement.movementSpeed);

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
}
