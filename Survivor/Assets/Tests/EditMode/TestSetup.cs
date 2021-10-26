using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSetup
{
    public GameObject CreatePlayerForTest()
    {
        GameObject myGameObject = GameObject.Find("Ethan");
        return myGameObject;
    }

    public PlayerMovement playerMovement()
    {
        PlayerMovement playerMovement = CreatePlayerForTest().GetComponent<PlayerMovement>();
        return playerMovement;
    }

    public SprintAndCrouch sprintAndCrouch()
    {
        SprintAndCrouch sprintAndCrouch = CreatePlayerForTest().GetComponent<SprintAndCrouch>(); ;
        return sprintAndCrouch;
    }

    public Rigidbody rb()
    {
        Rigidbody rb = CreatePlayerForTest().GetComponent<Rigidbody>(); ;
        return rb;
    }

    public CapsuleCollider playerCollider()
    {
        CapsuleCollider playerCollider = CreatePlayerForTest().GetComponent<CapsuleCollider>();
        return playerCollider;
    }


    public MovementAnimator animator()
    {
        MovementAnimator animator = CreatePlayerForTest().GetComponent<MovementAnimator>();
        return animator;
    }
}