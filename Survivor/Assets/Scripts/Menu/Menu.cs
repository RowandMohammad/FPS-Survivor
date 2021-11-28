using UnityEngine;

public class Menu : MonoBehaviour
{
    #region Public Fields

    public string menuName;
    public bool open;

    #endregion Public Fields



    #region Public Methods

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    #endregion Public Methods
}