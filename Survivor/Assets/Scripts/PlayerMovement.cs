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
    
    [SerializeField] Transform orientation;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;


    float horizontalMove;
    float verticalMove;

    Rigidbody rb;

    public bool playerIsGrounded;
    [SerializeField] LayerMask groundMask;
    float distancetoGround = 0.4f;

    RaycastHit slopeDetect;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Movement = new Movement(movementSpeed);
        rb.freezeRotation = true;


    }

    private void Update()
    {
        playerIsGrounded=Physics.CheckSphere(transform.position - new Vector3(0,1,0), distancetoGround, groundMask);
        print(playerIsGrounded);
        playerInput();
        dragControl();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeDetect.normal);
    }

    void playerInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && playerIsGrounded){

            Jump(rb);
        }
    }

    private bool slope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeDetect, playerHeight / 2 + 0.5f))
        {
            if (slopeDetect.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
         

        }
        return false;
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

    public void Jump(Rigidbody rb){
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        PlayerMover();


    }

    void PlayerMover()
    {
        moveDirection = orientation.forward * Movement.calculate(horizontalMove, verticalMove).z + orientation.right * Movement.calculate(horizontalMove, verticalMove).x;
        
        if (playerIsGrounded){
            rb.AddForce(moveDirection * movementMultiplier, ForceMode.Acceleration);
            
        }
        else if(playerIsGrounded && slope())
        {
            rb.AddForce(slopeMoveDirection * movementMultiplier, ForceMode.Acceleration);

        }
        else if(!playerIsGrounded)
        {
            rb.AddForce(moveDirection * movementMultiplier * 0.3f, ForceMode.Acceleration);  
        }

    }


}







