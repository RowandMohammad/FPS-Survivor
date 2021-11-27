using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using UnityEditor.SceneManagement;
using TMPro;
using Photon.Realtime;

public class LauncherTests
{
    private Launcher launcher;
    GameObject canvasObject;
    GameObject createRoomObjectText;
    private Menu menu;
    private MenuManager menuManager;
    GameObject roomField;
    public RoomInfo info;


    [OneTimeSetUp]
    public void TestInitialize()
    {
        PhotonNetwork.Disconnect();
        
        EditorSceneManager.LoadScene("Assets/Scenes/Menu.unity");
        GameObject.Destroy(GameObject.Find("RoomManager"));
       
    }




    [UnityTest, Order(1)]
    public IEnumerator canvasObjectIsCreated()
    {

        yield return new WaitForEndOfFrame();
        Assert.AreEqual(true, GameObject.Find("MenuCanvas").activeInHierarchy);
        yield return new WaitForSeconds(2f);


    }
    [UnityTest, Order(2)]
    public IEnumerator launcherIsConnected()
    {
        canvasObject = GameObject.Find("MenuCanvas");
        launcher = canvasObject.AddComponent<Launcher>();

        yield return new WaitForEndOfFrame();

        Debug.Log(PhotonNetwork.IsConnected);

        Assert.AreEqual(true, PhotonNetwork.IsConnected);
    }

    [UnityTest, Order(3)]
    public IEnumerator roomIsCreated()
    {
        
        launcher.nameOfRoomCreate = "test123";
        yield return new WaitForSeconds(2f);
        Assert.DoesNotThrow(() => launcher.CreateARoom(), "Error creating a room");
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();


    }



}
