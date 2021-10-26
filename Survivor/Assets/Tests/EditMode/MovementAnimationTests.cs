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

    //confirm that the animation happens
    //check the duration of the animation
    //check the frame rate of the animation

    [OneTimeSetUp]
    public void TestInitialize()
    {
        playerMovement = _setup.playerMovement();
        sprintAndCrouch = _setup.sprintAndCrouch();
        animator = _setup.animator();
    }

    [Test]
    public void IdleAnimationPlayingTest()
    {
        animator.Awake();
        animator.Start();

        Assert.AreEqual(true, animator._animator.GetCurrentAnimatorStateInfo(0).IsName("Movement Locomotion"));


    }

    [Test]
    public void IdleAnimationCorrectDurationTest()
    {
        animator.Awake();
        animator.Start();

        Assert.AreEqual(8.333334f, animator._animator.GetCurrentAnimatorStateInfo(0).length);


    }


}