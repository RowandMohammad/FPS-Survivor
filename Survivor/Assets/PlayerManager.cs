using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Private Fields

    private PhotonView PV;
    private Vector3 spawn = new Vector3(10f, 0f, 0f);

    #endregion Private Fields



    #region Private Methods

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonObjects", "PlayerObject"), spawn, Quaternion.identity);
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    #endregion Private Methods
}