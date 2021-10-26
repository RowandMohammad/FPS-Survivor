using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementAnimationTests
{
    private TestSetup _setup = new TestSetup();
    private PlayerMovement playerMovement;
    private SprintAndCrouch sprintAndCrouch;
    private MovementAnimator animator;

    [OneTimeSetUp]
    public void TestInitialize()
    {
        playerMovement = _setup.playerMovement();
        sprintAndCrouch = _setup.sprintAndCrouch();
        animator = _setup.animator();
    }


}