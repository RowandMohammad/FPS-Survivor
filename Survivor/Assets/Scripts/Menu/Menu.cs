using UnityEngine;

public class Menu : MonoBehaviour
{
    #region Public Fields

    public string menuName;
    public bool open;

    #endregion Public Fields



    #region Public Methods

    //Closes the menu.
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }

    //Opens the menu.
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    #endregion Public Methods
}