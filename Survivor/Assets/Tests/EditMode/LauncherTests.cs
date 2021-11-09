using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;

public class LauncherTests
{
    private Launcher launcher;

    [OneTimeSetUp]
    public void TestInitialize()
    {
        var canvasobject = GameObject.Find("MenuCanvas");
        launcher = canvasobject.AddComponent<Launcher>();
        

    }

    [Test]
    public void LauncherConnected()
    {
        launcher.Start();
        Debug.Log(PhotonNetwork.IsConnected);
        Assert.AreEqual(true, PhotonNetwork.IsConnected);
        

    }
}
