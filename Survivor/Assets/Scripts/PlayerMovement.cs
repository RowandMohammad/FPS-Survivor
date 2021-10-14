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

    public float jumpForce = 5f;

    public float movementMultiplier = 10f;
    float groundedDrag = 6f;

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

        rb.drag = groundedDrag;

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
        rb.AddForce(Movement.calculate(horizontalMove, verticalMove) * movementMultiplier, ForceMode.Acceleration);

    }


}







