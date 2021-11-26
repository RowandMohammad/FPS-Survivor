using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using UnityEditor.SceneManagement;
using TMPro;

public class LauncherTests
{
    private Launcher launcher;
    GameObject canvasObject;
    GameObject createRoomObjectText;
    private Menu menu;
    private MenuManager menuManager;


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

    [UnityTest]
    public IEnumerator roomIsCreated()
    {
        menuManager = canvasObject.GetComponent<MenuManager>();
        yield return new WaitForEndOfFrame();
        menuManager.menuOpen("createroom");
        yield return new WaitForEndOfFrame();
        canvasObject.GetComponentInChildren<TMP_InputField>().text = "testRoom";
        yield return new WaitForEndOfFrame();


        Assert.DoesNotThrow(() => launcher.CreateARoom(), "Error creating a room");
        yield return null;

    }
}
