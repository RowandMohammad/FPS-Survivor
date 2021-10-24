using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;


namespace Tests
{


    public class PlayerMovementTests
    {
        private GameObject player()
        {
            GameObject myGameObject = GameObject.Find("Ethan");
            return myGameObject;

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
        public void Jump_in_Air()
        {
            PlayerMovement playerMovement = player().AddComponent<PlayerMovement>();
            Rigidbody rb = player().GetComponent<Rigidbody>();
            playerMovement.Jump(rb);
            Assert.IsFalse(playerMovement.playerIsGrounded);
        }

        [Test]
        public void Sprint_Button_Changes_Movement_Speed()
        {
            GameObject playerObject = player();
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            SprintAndCrouch sprintAndCrouch = playerObject.GetComponent<SprintAndCrouch>();
            Assert.AreEqual(5f, playerMovement.movementSpeed);
            playerMovement.movementSpeed = sprintAndCrouch.Sprint(playerMovement.movementSpeed);
            Assert.AreEqual(10f, playerMovement.movementSpeed);
        }

        [Test]
        public void Walk_Speed_Returns_After_Sprint_Key_Released()
        {
            GameObject playerObject = player();
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            SprintAndCrouch sprintAndCrouch = playerObject.GetComponent<SprintAndCrouch>();

            playerMovement.movementSpeed = sprintAndCrouch.Sprint(playerMovement.movementSpeed);
            Assert.AreEqual(sprintAndCrouch.sprintSpeed, playerMovement.movementSpeed);

            playerMovement.movementSpeed = sprintAndCrouch.Walk(playerMovement.movementSpeed);
            Assert.AreEqual(sprintAndCrouch.walkSpeed, playerMovement.movementSpeed);
        }


        [Test]
        public void Player_Collider_Shrinks_With_Crouch()
        {
            GameObject playerObject = player();
            CapsuleCollider playerCollider = GameObject.Find("Ethan").GetComponent<CapsuleCollider>();
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            SprintAndCrouch sprintAndCrouch = playerObject.GetComponent<SprintAndCrouch>();
            float preCrouchHeight = sprintAndCrouch.standingHeight;
            float heightWhenCrouched = sprintAndCrouch.Crouch(sprintAndCrouch.crouchedHeightModifier);
            Assert.AreEqual(1f, heightWhenCrouched);
            Assert.AreEqual(2f, sprintAndCrouch.Crouch(preCrouchHeight));



        }
    }
}