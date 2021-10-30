using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    #region Public Fields

    [Header("State Heights")]
    public float crouchedHeightModifier = -0.3f;

    public float crouchSpeed = 3f;
    public bool isCrouching;
    public CapsuleCollider playerCollider;

    [Header("State Speeds")]
    public float sprintSpeed = 15f;

    public float standingHeight;
    public float walkSpeed = 5f;

    #endregion Public Fields



    #region Private Fields

    private Vector3 beforeTemp;
    [SerializeField] private Transform cameraHeight;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;
    private PlayerMovement playerMovement;

    [Header("Keybinds")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    private Vector3 temp;

    #endregion Private Fields



    #region Public Methods

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

    #endregion Public Methods



    #region Private Methods

    private void Awake()
    {
        CrouchingCamera c1 = new CrouchingCamera();
        standingHeight = c1.standingHeight;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GameObject.Find("Ethan").GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        if (isCrouching)
        {
            temp = new Vector3(0.1f, crouchedHeightModifier, 1f);
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

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching && (Input.GetAxisRaw("Vertical") >= 0 || Input.GetAxisRaw("Horizontal") >= 0))
        {
            playerMovement.movementSpeed = Sprint(playerMovement.movementSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.movementSpeed = Walk(playerMovement.movementSpeed);
        }

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

    #endregion Private Methods
}