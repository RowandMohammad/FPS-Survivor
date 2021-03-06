using Photon.Pun;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    #region Public Fields

    [Header("State Heights")]
    public float crouchedHeightModifier = -0.2f;

    public float crouchSpeed = 7.5f;
    public bool isCrouching;
    public CapsuleCollider playerCollider;

    [Header("State Speeds")]
    public float sprintSpeed = 20f;

    public float standingHeight;
    public float walkSpeed = 10f;

    #endregion Public Fields



    #region Private Fields

    private MovementAnimator animator;
    private Vector3 beforeTemp;
    [SerializeField] private Transform cameraHeight;
    [SerializeField] private Transform weaponHeight;

    [SerializeField] private KeyCode crouchKey = KeyCode.F15;
    private PlayerMovement playerMovement;
    private PhotonView PV;

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
        PV = GetComponent<PhotonView>();
        CrouchingCamera c1 = new CrouchingCamera();
        standingHeight = c1.standingHeight;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GameObject.Find("Ethan").GetComponent<CapsuleCollider>();
        animator = GetComponent<MovementAnimator>();
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        if (isCrouching)
        {
            temp = new Vector3(0.1f, crouchedHeightModifier, 0.2f);
            cameraHeight.localPosition = Vector3.Lerp(cameraHeight.localPosition, temp, Time.deltaTime * 4f);
            weaponHeight.localPosition = Vector3.Lerp(weaponHeight.localPosition, temp, Time.deltaTime * 4f);
            Debug.Log(cameraHeight.localPosition);
        }
        if (!isCrouching)
        {
            beforeTemp = new Vector3(0f, standingHeight, 0f);
            cameraHeight.localPosition = Vector3.Lerp(cameraHeight.localPosition, beforeTemp, Time.deltaTime * 4f);
            weaponHeight.localPosition = Vector3.Lerp(weaponHeight.localPosition, beforeTemp, Time.deltaTime * 4f);

            Debug.Log(cameraHeight.localPosition);
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.movementSpeed = Sprint(playerMovement.movementSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.movementSpeed = Walk(playerMovement.movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.F15))
        {
            Crouch(crouchedHeightModifier);
            playerMovement.movementSpeed = crouchSpeed;
        }
        if (Input.GetKeyUp(KeyCode.F15))
        {
            Crouch(standingHeight);
            playerMovement.movementSpeed = walkSpeed;
        }
    }

    #endregion Private Methods
}