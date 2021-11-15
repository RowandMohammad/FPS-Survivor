using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Movement Movement;
    public Vector3 MoveDirection;


    [Header("Movement Attributes/Magnitudes")]
    public float movementSpeed = 5f;
    public float jumpForce = 7.5f;
    public float movementMultiplier = 4f;
    float groundedDrag = 6f;
    float notGroundedDrag = 3f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    public float horizontalMove;
    public float verticalMove;

    [Header("Player Attributes")]
    public float playerHeight = 2f;
    public Rigidbody rb;

    [Header("Camera Orientation")]
    [SerializeField] Transform orientation;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Detection for Ground")]
    [SerializeField] Transform checkPlayerGrounded;
    public bool playerIsGrounded;
    [SerializeField] LayerMask groundMask;
    float distancetoGround = 0.1f;
    RaycastHit slopeDetect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }


    private void Start()
    {
        Movement = new Movement(movementSpeed);
        

        rb.freezeRotation = true;
    }

    private void Update()
    {
    
        Movement = new Movement(movementSpeed);
        playerIsGrounded = Physics.CheckSphere(checkPlayerGrounded.position, distancetoGround, groundMask);
        print(playerIsGrounded);
        playerInput();
        dragControl();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeDetect.normal);
    }

    //Detects player input.
    public void playerInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && playerIsGrounded )
        {
            Jump(rb);
        }
    }

    //Detects whether player is actively on a slope or not.
    private bool slope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeDetect, playerHeight / 2 + 0.5f))
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

    //Adjusts the drag for the user for when theyre grounded and mid air.
    void dragControl()
    {
        if (playerIsGrounded)
        {
            rb.drag = groundedDrag;
        }
        else
        {
            rb.drag = notGroundedDrag;
        }
    }

    //Adds upwards force to user to jump.
    public void Jump(Rigidbody rb)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        PlayerMover();
    }

    //Checks  possible user states and allows user to move around correctly for that state.
    void PlayerMover()
    {
        moveDirection = orientation.forward * Movement.calculate(horizontalMove, verticalMove).z + orientation.right * Movement.calculate(horizontalMove, verticalMove).x;

        if (playerIsGrounded)
        {
            rb.AddForce(moveDirection * movementMultiplier, ForceMode.Acceleration);
        }
        else if (playerIsGrounded && slope())
        {
            rb.AddForce(slopeMoveDirection * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!playerIsGrounded)
        {
            rb.AddForce(moveDirection * movementMultiplier * 0.3f, ForceMode.Acceleration);
        }
    }
}