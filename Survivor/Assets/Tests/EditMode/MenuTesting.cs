using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MenuTesting
{
    private Menu menu;
    private MenuManager menuManager;
    GameObject menuObject;

    [OneTimeSetUp]
    public void TestInitialize()
    {
        menu = new Menu();
        menuObject = GameObject.Find("Canvas");
        menuManager = menuObject.GetComponent<MenuManager>();
    }

    [Test]
    public void CheckLoadingMenuOpen()
    {
        menuManager.menuOpen("loading");
        Assert.AreEqual("Loading Menu (Menu)", menuManager.menus[0].ToString());
    }

}
