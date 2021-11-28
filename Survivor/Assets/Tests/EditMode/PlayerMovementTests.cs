using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tests
{
    public class PlayerMovementTests
    {
        #region Private Fields

        private TestSetup _setup = new TestSetup();
        private CapsuleCollider playerCollider;
        private PlayerMovement playerMovement;
        private Rigidbody rb;
        private SprintAndCrouch sprintAndCrouch;

        #endregion Private Fields



        #region Public Methods

        [Test]
        public void Jump_in_Air()
        {
            playerMovement.Jump(rb);
            Assert.IsFalse(playerMovement.playerIsGrounded);
        }

        [Test]
        public void Moves_On_XAxis_For_Horizontal_Movement()
        {
            Assert.AreEqual(1, new Movement(1).calculate(1, 0).x, 0.1f);
        }

        [Test]
        public void Moves_On_ZAxis_For_Vertical_Movement()
        {
            Assert.AreEqual(1, new Movement(1).calculate(0, 1).z, 0.1f);
        }

        [Test]
        public void Player_Collider_Shrinks_With_Crouch()
        {
            float preCrouchHeight = sprintAndCrouch.standingHeight;
            float heightWhenCrouched = sprintAndCrouch.Crouch(sprintAndCrouch.crouchedHeightModifier);
            Assert.AreEqual(1f, heightWhenCrouched);
            Assert.AreEqual(2f, sprintAndCrouch.Crouch(preCrouchHeight));
        }

        [Test]
        public void Sprint_Button_Changes_Movement_Speed()
        {
            Assert.AreEqual(5f, playerMovement.movementSpeed);
            playerMovement.movementSpeed = sprintAndCrouch.Sprint(playerMovement.movementSpeed);
            Assert.AreEqual(15f, playerMovement.movementSpeed);
        }

        [OneTimeSetUp]
        public void TestInitialize()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MovementTestScene.unity");
            playerMovement = _setup.playerMovement();
            rb = _setup.rb();
            sprintAndCrouch = _setup.sprintAndCrouch();
            playerCollider = _setup.playerCollider();
        }

        [Test]
        public void Walk_Speed_Returns_After_Sprint_Key_Released()
        {
            playerMovement.movementSpeed = sprintAndCrouch.Sprint(playerMovement.movementSpeed);
            Assert.AreEqual(sprintAndCrouch.sprintSpeed, playerMovement.movementSpeed);

            playerMovement.movementSpeed = sprintAndCrouch.Walk(playerMovement.movementSpeed);
            Assert.AreEqual(sprintAndCrouch.walkSpeed, playerMovement.movementSpeed);
        }

        #endregion Public Methods
    }
}