using NUnit.Framework;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class LauncherTests
{
    #region Public Fields

    public RoomInfo info;

    #endregion Public Fields



    #region Private Fields

    private GameObject canvasObject;
    private GameObject createRoomObjectText;
    private Launcher launcher;
    private Menu menu;
    private MenuManager menuManager;
    private GameObject roomField;

    #endregion Private Fields



    #region Public Methods

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

    [OneTimeSetUp]
    public void TestInitialize()
    {
        PhotonNetwork.Disconnect();

        EditorSceneManager.LoadScene("Assets/Scenes/Menu.unity");
        GameObject.Destroy(GameObject.Find("RoomManager"));
    }

    #endregion Public Methods
}