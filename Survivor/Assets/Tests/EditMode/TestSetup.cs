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
        PlayerMovement playerMovement = CreatePlayerForTest().AddComponent<PlayerMovement>();
        return playerMovement;
    }

    public Rigidbody rb()
    {
        Rigidbody rb = CreatePlayerForTest().GetComponent<Rigidbody>(); ;
        return rb;
    }

}

