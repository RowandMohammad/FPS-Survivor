using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{


    public class PlayerMovementTests
    {
        [SerializeField] Transform orientation;

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

    }
}
