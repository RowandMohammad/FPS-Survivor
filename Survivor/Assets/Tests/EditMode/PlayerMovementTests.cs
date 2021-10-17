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
            GameObject myGameObject = GameObject.Find("First Person Player");
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
            Assert.AreEqual(false, playerMovement.playerIsGrounded);
        }

        [Test]
        public void Sprint_Button_Changes_Movement_Speed()
        {
            GameObject playerObject = player();
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            SprintAndCrouch sprintAndCrouch = playerObject.GetComponent<SprintAndCrouch>();
            Assert.AreEqual(6f, playerMovement.movementSpeed);
            playerMovement.movementSpeed = sprintAndCrouch.Sprint(playerMovement.movementSpeed);
            Assert.AreEqual(10f, playerMovement.movementSpeed);
        }


    }
}
