using NUnit.Framework;
using UnityEditor.SceneManagement;

public class MovementAnimationTests
{
    #region Private Fields

    private TestSetup _setup = new TestSetup();
    private MovementAnimator animator;
    private PlayerMovement playerMovement;
    private SprintAndCrouch sprintAndCrouch;

    #endregion Private Fields

    //confirm that the animation happens
    //check the duration of the animation



    #region Public Methods

    [Test]
    public void IdleAnimationCorrectDurationTest()
    {
        animator.Awake();
        animator.Start();

        Assert.AreEqual(8.333334f, animator._animator.GetCurrentAnimatorStateInfo(0).length);
    }

    [Test]
    public void IdleAnimationPlayingTest()
    {
        animator.Awake();
        animator.Start();

        Assert.AreEqual(true, animator._animator.GetCurrentAnimatorStateInfo(0).IsName("Movement Locomotion"));
    }

    [OneTimeSetUp]
    public void TestInitialize()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MovementTestScene.unity");

        playerMovement = _setup.playerMovement();
        sprintAndCrouch = _setup.sprintAndCrouch();
        animator = _setup.animator();
    }

    #endregion Public Methods
}