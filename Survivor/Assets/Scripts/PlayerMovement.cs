using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Movement Movement;

    [Header("Movement Types")]
    public float movementSpeed = 6f;

    [Header("Keybinds")]
    [SerializeField]KeyCode jumpKey = KeyCode.Space;

    public float jumpForce = 10f;

    public float movementMultiplier = 10f;
    float groundedDrag = 6f;
    float notGroundedDrag = 1f;
    float playerHeight = 2f;
    



    float horizontalMove;
    float verticalMove;

    Rigidbody rb;

    bool playerIsGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Movement = new Movement(movementSpeed);
        rb.freezeRotation = true;


    }

    private void Update()
    {
        playerIsGrounded=Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);
        print(playerIsGrounded);
        playerInput();
        dragControl();
   
    }

    void playerInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && playerIsGrounded){

            Jump();
        }
    }

    void dragControl()
    {
        if (playerIsGrounded){
            rb.drag = groundedDrag;
        }
        else{
            rb.drag=notGroundedDrag;
        }
    }

    void Jump(){
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        PlayerMover();


    }

    void PlayerMover()
    {
        if (playerIsGrounded){
            rb.AddForce(Movement.calculate(horizontalMove, verticalMove) * movementMultiplier, ForceMode.Acceleration);
            
        }
        else
        {
            rb.AddForce(Movement.calculate(horizontalMove, verticalMove) * movementMultiplier * 0.3f, ForceMode.Acceleration);  
        }

    }


}







