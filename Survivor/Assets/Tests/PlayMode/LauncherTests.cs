using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using UnityEditor.SceneManagement;

public class LauncherTests
{
    private Launcher launcher;
    GameObject canvasObject;

    [OneTimeSetUp]
    public void TestInitialize()
    {
        PhotonNetwork.Disconnect();
        EditorSceneManager.LoadScene("Assets/Scenes/Menu.unity");
        GameObject.Destroy(GameObject.Find("RoomManager"));
    }


    [UnityTest]
    public IEnumerator canvasObjectIsCreated()
    {
        

        Assert.AreEqual(true, GameObject.Find("MenuCanvas").activeInHierarchy);
        yield return new WaitForEndOfFrame();


    }

}
