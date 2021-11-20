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
    [UnityTest]
    public IEnumerator launcherIsConnected()
    {
        canvasObject = GameObject.Find("MenuCanvas");
        launcher = canvasObject.AddComponent<Launcher>();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();


        Debug.Log(PhotonNetwork.IsConnected);

        Assert.AreEqual(true, PhotonNetwork.IsConnected);
    }
}
