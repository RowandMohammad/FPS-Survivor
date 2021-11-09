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
        menuObject = GameObject.Find("MenuCanvas");
        menuManager = menuObject.GetComponent<MenuManager>();
    }

    [Test]
    public void CheckLoadingMenuInArray()
    {
        
        Assert.AreEqual("Loading Menu (Menu)", menuManager.menus[0].ToString());
        
    }

    [Test]
    public void CheckTitleMenuInArray()
    {
        
        Assert.AreEqual("TitleMenu (Menu)", menuManager.menus[1].ToString());

    }

    [Test]
    public void CheckLoadingMenuIsOpened()
    {
        menuManager.menuOpen("loading");
        Assert.AreEqual(true, menuManager.menus[0].open);

    }

    [Test]
    public void CheckTitleMenuIsOpened()
    {
        menuManager.menuOpen("title");
        Assert.AreEqual(true, menuManager.menus[1].open);

    }

    [Test]
    public void CheckLoadingMenuIsClosedAfterOpening()
    {
        menuManager.menuOpen("loading");
        Assert.AreEqual(true, menuManager.menus[0].open);
        menuManager.CloseMenu(menuManager.menus[0]);
        Assert.AreEqual(false, menuManager.menus[0].open);


    }

    [Test]
    public void CheckTitleMenuIsClosedAfterOpening()
    {
        menuManager.menuOpen("title");
        Assert.AreEqual(true, menuManager.menus[1].open);
        menuManager.CloseMenu(menuManager.menus[1]);
        Assert.AreEqual(false, menuManager.menus[1].open);


    }


}
