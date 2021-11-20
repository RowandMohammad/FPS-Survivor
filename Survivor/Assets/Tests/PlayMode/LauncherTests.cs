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

    [SetUp]
    public void TestInitialize()
    {
        EditorSceneManager.LoadScene("Assets/Scenes/Menu.unity");
    }


}
