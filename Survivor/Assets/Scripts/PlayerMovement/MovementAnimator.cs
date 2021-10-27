using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Movement Movement;
    private SprintAndCrouch sprintAndCrouch;
    public Animator _animator;
    public bool isSprintJump;
    public bool isSprinting;
    public bool isJumping;

    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    public void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        sprintAndCrouch = GetComponent<SprintAndCrouch>();
        _animator = GetComponent<Animator>();
    }

    public void Start()
    {
        Movement = new Movement(playerMovement.movementSpeed);
    }

    void Update()
    {
        animateMovementLocomotion();
        animateJump();
        animateSprinting();
        animateCrouch();
        animateSprintJump();
    }

    void animateMovementLocomotion()
    {
        float velocityZ = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.forward);
        float velocityX = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    void animateJump()
    {
        if (Input.GetKeyDown(jumpKey) && playerMovement.playerIsGrounded && !isSprinting)
        {
            
            _animator.SetBool("isJumping", true);
            isJumping = true;

        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("isJumping") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            
            _animator.SetBool("isJumping", false);
            isJumping = false;

        }

    }

    void animateSprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !sprintAndCrouch.isCrouching)
        {
            
            _animator.SetBool("isSprinting", true);
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !sprintAndCrouch.isCrouching)
        {
            
            _animator.SetBool("isSprinting", false);
            isSprinting = false;
        }
    }

    void animateCrouch()
    {
        if (sprintAndCrouch.isCrouching)
        {
            _animator.SetBool("isCrouching", true);
        }
        if (!sprintAndCrouch.isCrouching)
        {
            _animator.SetBool("isCrouching", false);
        }
    }

    void animateSprintJump()
    {
        if (isSprinting && Input.GetKeyDown(jumpKey) && playerMovement.playerIsGrounded)
        {
            _animator.SetBool("isSprintJumping", true);
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Sprint") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _animator.SetBool("isSprintJumping", false);
        }
    }
}