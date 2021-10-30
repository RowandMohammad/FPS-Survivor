using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    #region Public Fields

    public Animator _animator;
    public bool isJumping;
    public bool isSprinting;
    public bool isSprintJump;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private KeyCode crouchKey = KeyCode.C;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    private Movement Movement;
    private PlayerMovement playerMovement;
    private SprintAndCrouch sprintAndCrouch;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    #endregion Private Fields



    #region Public Methods

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

    #endregion Public Methods



    #region Private Methods

    private void animateCrouch()
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

    private void animateJump()
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

    private void animateMovementLocomotion()
    {
        float velocityZ = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.forward);
        float velocityX = Vector3.Dot(Movement.calculate(playerMovement.horizontalMove, playerMovement.verticalMove), playerMovement.rb.transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    private void animateSprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !sprintAndCrouch.isCrouching && (Input.GetAxisRaw("Vertical") >= 0 || Input.GetAxisRaw("Horizontal") >= 0))
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

    private void animateSprintJump()
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

    private void Update()
    {
        animateMovementLocomotion();
        animateJump();
        animateSprinting();
        animateCrouch();
        animateSprintJump();
    }

    #endregion Private Methods
}