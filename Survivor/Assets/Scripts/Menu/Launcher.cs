using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Public Fields

    public static Launcher Instance;
    [SerializeField] public TMP_InputField inputtedRoomName;
    public string nameOfRoomCreate;
    public List<RoomInfo> roomLister;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private TMP_Text errorMessage;
    [SerializeField] private GameObject listedPlayerObject;
    [SerializeField] private GameObject listedRoomObject;
    [SerializeField] private Transform listOfPlayerInfo;
    [SerializeField] private Transform listOfRoomInfo;
    [SerializeField] private TMP_Text nameOfRoom;
    [SerializeField] private GameObject startGameButton;

    #endregion Private Fields



    #region Public Methods
    //This method creates a room.
    public void CreateARoom()
    {
        if (string.IsNullOrEmpty(roomName()))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomName());
        MenuManager.Instance.menuOpen("loading");
    }

    //This method joins the selected room.
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.menuOpen("loading");
    }

    //This method leaves the current room.
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.menuOpen("loading");
    }


    //This method is called upon the player connecting to the server.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Successfuly connected to online server");
    }

    //This method is called upon failing to join a selected room.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "Room Creation has Failed: " + message;
        Debug.LogError("Room Creation has Failed: " + message);
        MenuManager.Instance.menuOpen("error");
    }

    //This method is called upon entering a lobby on the server.
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.menuOpen("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }


    //This method is called upon entering a room on the server.
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.menuOpen("room");
        nameOfRoom.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Transform child in listOfPlayerInfo)
        {
            Destroy(child.gameObject);
        }
        Player[] joinedPlayers = PhotonNetwork.PlayerList;

        for (int i = 0; i < joinedPlayers.Count(); i++)
        {
            Instantiate(listedPlayerObject, listOfPlayerInfo).GetComponent<PlayerObjectItem>().SetUp(joinedPlayers[i]); ;
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //This method is called upon leaving a room on the server.
    public override void OnLeftRoom()
    {
        MenuManager.Instance.menuOpen("title");
    }

    //This is called upon the host player leaving and host is assigned to another player.
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //This is called when a remote player enters the room on the server.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(listedPlayerObject, listOfPlayerInfo).GetComponent<PlayerObjectItem>().SetUp(newPlayer);
    }

    //Called upon any update that occurs within the current room.
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform obj in listOfRoomInfo)
        {
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(listedRoomObject, listOfRoomInfo).GetComponent<RoomObjectItem>().SetUp(roomList[i]);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        Debug.Log("Connecting to online server");
    }

    //This method starts the game inside the room.
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    #endregion Public Methods



    #region Private Methods

    private void Awake()
    {
        Instance = this;
    }

    //This method assign the name of the created room.
    private string roomName()
    {
        if (nameOfRoomCreate == "test123")
        {
            return nameOfRoomCreate;
        }
        else
        {
            return inputtedRoomName.text;
        }
    }

    #endregion Private Methods
}