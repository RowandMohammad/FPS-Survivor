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
        launcher = new Launcher();
    }

    [Test]
    public void LauncherConnected()
    {
        launcher.Start();
        Assert.AreEqual(true, PhotonNetwork.IsConnected);
    }
}
