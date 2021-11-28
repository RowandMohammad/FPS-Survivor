using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerObjectItem : MonoBehaviourPunCallbacks
{
    #region Private Fields

    private Player player;
    [SerializeField] private TMP_Text text;

    #endregion Private Fields



    #region Public Methods

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }

    #endregion Public Methods
}