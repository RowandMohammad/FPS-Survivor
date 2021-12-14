using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Public Fields

    public static MenuManager Instance;

    [SerializeField] public Menu[] menus;

    #endregion Public Fields



    #region Public Methods

    //Called upon script being loaded
    public void Awake()
    {
        Instance = this;
    }

    //Closes the currently open menu by Menu object type.
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    //Opens the new menu by String and closes the currently open menu.
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

    //Opens the new menu by Menu object type and closes the currently open menu.
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