using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests{


    public class PlayerMovementTests 
    {
 
        [Test]
        public void Moves_On_XAxis_For_Horizontal_Movement(){
            Assert.AreEqual(1, new Movement(1).calculate(1).x, 0.1f);

        }

    }
}
