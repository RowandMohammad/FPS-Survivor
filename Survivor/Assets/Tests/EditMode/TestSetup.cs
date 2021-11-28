using UnityEngine;

public class TestSetup
{
    #region Public Methods

    public MovementAnimator animator()
    {
        MovementAnimator animator = CreatePlayerForTest().GetComponent<MovementAnimator>();
        return animator;
    }

    public GameObject CreatePlayerForTest()
    {
        GameObject myGameObject = GameObject.Find("Ethan");
        return myGameObject;
    }

    public CapsuleCollider playerCollider()
    {
        CapsuleCollider playerCollider = CreatePlayerForTest().GetComponent<CapsuleCollider>();
        return playerCollider;
    }

    public PlayerMovement playerMovement()
    {
        PlayerMovement playerMovement = CreatePlayerForTest().GetComponent<PlayerMovement>();
        return playerMovement;
    }

    public Rigidbody rb()
    {
        Rigidbody rb = CreatePlayerForTest().GetComponent<Rigidbody>(); ;
        return rb;
    }

    public SprintAndCrouch sprintAndCrouch()
    {
        SprintAndCrouch sprintAndCrouch = CreatePlayerForTest().GetComponent<SprintAndCrouch>(); ;
        return sprintAndCrouch;
    }

    #endregion Public Methods
}