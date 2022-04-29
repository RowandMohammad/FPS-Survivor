using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Public Fields

    public static MenuManager Instance;

    [SerializeField] public Menu[] menus;

    #endregion Public Fields



    #region Public Methods

    public void Awake()
    {
        Instance = this;
    }

    //Closes the passes in menu page
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    //Loads up the firing range for the game
    public void FiringRange()
    {
        SceneManager.LoadScene("FiringRange");
    }

    //Quits the game application
    public void GameClose()
    {
        Application.Quit();
    }

    //Opens the passed in Menu page by string.
    public void menuOpen(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    //Opens the passed in Menu page by Menu Object.
    public void menuOpen(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    #endregion Public Methods
}