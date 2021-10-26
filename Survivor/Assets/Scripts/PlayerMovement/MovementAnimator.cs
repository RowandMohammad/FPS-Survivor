using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Movement Movement;
    private SprintAndCrouch sprintAndCrouch;
    public Animator _animator;

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
 
    }

    public void animateMovementLocomotion()
    {
        float velocityZ = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.forward);
        float velocityX = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }


}