using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [Header("State Speeds")]
    public float sprintSpeed = 15f;
    public float walkSpeed = 5f;
    public float crouchSpeed = 3f;

    [Header("State Heights")]
    public float crouchedHeightModifier = -0.6f;
    public float standingHeight;

    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;

    private bool isCrouching;

    [SerializeField] Transform cameraHeight;

    public CapsuleCollider playerCollider;

    private Vector3 temp;
    private Vector3 beforeTemp;

    Animator _animator;
    private bool isSprinting;

    void Awake()
    {
        CrouchingCamera c1 = new CrouchingCamera();
        standingHeight = c1.standingHeight;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GameObject.Find("Ethan").GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            isSprinting = true;

            playerMovement.movementSpeed = Sprint(playerMovement.movementSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            isSprinting = false;

            playerMovement.movementSpeed = Walk(playerMovement.movementSpeed);
        }
        animateObject();

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
    void FixedUpdate()
    {
        if (isCrouching)
        {
            temp = new Vector3(0.1f, crouchedHeightModifier, 0.75f);
            cameraHeight.localPosition = Vector3.Lerp(cameraHeight.localPosition, temp, Time.deltaTime * 4f);
            Debug.Log(cameraHeight.localPosition);
        }
        if (!isCrouching)
        {
            beforeTemp = new Vector3(0f, standingHeight, 0f);
            cameraHeight.localPosition = Vector3.Lerp(cameraHeight.localPosition, beforeTemp, Time.deltaTime * 4f);

            Debug.Log(cameraHeight.localPosition);
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
            lTemp.y += 0.5f;
            playerCollider.center = lTemp;
        }

        return playerCollider.height;
    }

    void animateObject()
    {
        if (isSprinting)
        {
            _animator.SetBool("isSprinting", true);
        }
        if (!isSprinting)
        {
            _animator.SetBool("isSprinting", false);
        }
        if (isCrouching)
        {
            _animator.SetBool("isCrouching", true);
        }
        if (!isCrouching)
        {
            _animator.SetBool("isCrouching", false);
        }
    }
}