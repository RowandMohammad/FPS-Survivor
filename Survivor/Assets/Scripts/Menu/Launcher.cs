using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public void Start()
    {

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to online server");

        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Successfuly connected to online server");
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.menuOpen("title");
        Debug.Log("Joined Lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
