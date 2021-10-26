using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Movement Movement;
    Animator _animator;

    [Header("Movement Attributes/Magnitudes")]
    public float movementSpeed = 2.5f;
    public float jumpForce = 7.5f;
    public float movementMultiplier = 10f;
    float groundedDrag = 6f;
    float notGroundedDrag = 2f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    float horizontalMove;
    float verticalMove;

    [Header("Player Attributes")]
    public float playerHeight = 2f;
    Rigidbody rb;

    [Header("Camera Orientation")]
    [SerializeField] Transform orientation;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Detection for Ground")]
    [SerializeField] Transform checkPlayerGrounded;
    public bool playerIsGrounded;
    [SerializeField] LayerMask groundMask;
    float distancetoGround = 0.4f;
    RaycastHit slopeDetect;

    void Awake() => _animator = GetComponent<Animator>();

    private void Start()
    {
        Movement = new Movement(movementSpeed);
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void Update()
    {
        animateObject();
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

        if (Input.GetKeyDown(jumpKey) && playerIsGrounded)
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

    //Adjusts the drag for the user for when theyre ground and mid air.
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
    void animateObject()
    {
        float velocityZ = Vector3.Dot(Movement.calculate(horizontalMove, verticalMove), rb.transform.forward);
        float velocityX = Vector3.Dot(Movement.calculate(horizontalMove, verticalMove), rb.transform.right);
        Debug.Log(velocityX);
        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

        if (playerIsGrounded)
        {
            _animator.SetBool("isJumping", false);
        }
        if (!playerIsGrounded)
        {
            _animator.SetBool("isJumping", true);
        }
    }
}