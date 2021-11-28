using NUnit.Framework;
using UnityEngine;

public class MenuTesting
{
    #region Private Fields

    private Menu menu;
    private MenuManager menuManager;
    private GameObject menuObject;

    #endregion Private Fields



    #region Public Methods

    [Test]
    public void CheckLoadingMenuInArray()
    {
        Assert.AreEqual("Loading Menu (Menu)", menuManager.menus[0].ToString());
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
    public void CheckLoadingMenuIsOpened()
    {
        menuManager.menuOpen("loading");
        Assert.AreEqual(true, menuManager.menus[0].open);
    }

    [Test]
    public void CheckTitleMenuInArray()
    {
        Assert.AreEqual("TitleMenu (Menu)", menuManager.menus[1].ToString());
    }

    [Test]
    public void CheckTitleMenuIsClosedAfterOpening()
    {
        menuManager.menuOpen("title");
        Assert.AreEqual(true, menuManager.menus[1].open);
        menuManager.CloseMenu(menuManager.menus[1]);
        Assert.AreEqual(false, menuManager.menus[1].open);
    }

    [Test]
    public void CheckTitleMenuIsOpened()
    {
        menuManager.menuOpen("title");
        Assert.AreEqual(true, menuManager.menus[1].open);
    }

    [OneTimeSetUp]
    public void TestInitialize()
    {
        menu = new Menu();
        menuObject = GameObject.Find("MenuCanvas");
        menuManager = menuObject.GetComponent<MenuManager>();
    }

    #endregion Public Methods
}