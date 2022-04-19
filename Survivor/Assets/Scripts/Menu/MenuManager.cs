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

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

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

    public void GameClose()
    {
        Application.Quit();
    }

    public void FiringRange()
    {
        SceneManager.LoadScene("FiringRange");
    }

    #endregion Public Methods
}