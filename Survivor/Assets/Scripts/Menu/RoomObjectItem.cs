using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomObjectItem : MonoBehaviour
{
    #region Public Fields

    public RoomInfo info;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private TMP_Text text;

    #endregion Private Fields



    #region Public Methods

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name;
    }

    #endregion Public Methods
}