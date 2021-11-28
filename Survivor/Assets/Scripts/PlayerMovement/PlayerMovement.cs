using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Fields

    public float horizontalMove;
    public float jumpForce = 7.5f;
    public Vector3 MoveDirection;
    public Movement Movement;
    public float movementMultiplier = 4f;

    [Header("Movement Attributes/Magnitudes")]
    public float movementSpeed = 5f;

    [Header("Player Attributes")]
    public float playerHeight = 2f;

    public bool playerIsGrounded;
    public Rigidbody rb;
    public float verticalMove;

    #endregion Public Fields



    #region Private Fields

    [Header("Detection for Ground")]
    [SerializeField] private Transform checkPlayerGrounded;

    private float distancetoGround = 0.1f;
    private float groundedDrag = 6f;
    [SerializeField] private LayerMask groundMask;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private Vector3 moveDirection;
    private float notGroundedDrag = 3f;

    [Header("Camera Orientation")]
    [SerializeField] private Transform orientation;

    private PhotonView PV;
    private RaycastHit slopeDetect;
    private Vector3 slopeMoveDirection;

    #endregion Private Fields



    #region Public Methods

    //Adds upwards force to user to jump.
    public void Jump(Rigidbody rb)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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

    #endregion Public Methods



    #region Private Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    //Adjusts the drag for the user for when theyre grounded and mid air.
    private void dragControl()
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

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        PlayerMover();
    }

    //Checks  possible user states and allows user to move around correctly for that state.
    private void PlayerMover()
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

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(rb);
        }
        Movement = new Movement(movementSpeed);

        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        Movement = new Movement(movementSpeed);
        playerIsGrounded = Physics.CheckSphere(checkPlayerGrounded.position, distancetoGround, groundMask);
        print(playerIsGrounded);
        playerInput();
        dragControl();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeDetect.normal);
    }

    #endregion Private Methods
}