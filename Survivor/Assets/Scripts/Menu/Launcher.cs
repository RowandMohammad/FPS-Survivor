using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    [SerializeField] public TMP_InputField inputtedRoomName;
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] TMP_Text nameOfRoom;
   
    public List<RoomInfo> roomLister;

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

    public void CreateARoom()
    {
        if (string.IsNullOrEmpty(inputtedRoomName.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(inputtedRoomName.text);
        MenuManager.Instance.menuOpen("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.menuOpen("room");
        nameOfRoom.text = PhotonNetwork.CurrentRoom.Name;



    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "Room Creation has Failed: " + message;
        Debug.LogError("Room Creation has Failed: " + message);
        MenuManager.Instance.menuOpen("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.menuOpen("loading");
    }



    public override void OnLeftRoom()
    {
        MenuManager.Instance.menuOpen("title");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
