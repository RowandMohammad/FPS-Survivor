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

    public void CreateARoom()
    {
        if (string.IsNullOrEmpty(roomName()))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomName());
        MenuManager.Instance.menuOpen("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.menuOpen("loading");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.menuOpen("loading");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Successfuly connected to online server");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "Room Creation has Failed: " + message;
        Debug.LogError("Room Creation has Failed: " + message);
        MenuManager.Instance.menuOpen("error");
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.menuOpen("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

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

    public override void OnLeftRoom()
    {
        MenuManager.Instance.menuOpen("title");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(listedPlayerObject, listOfPlayerInfo).GetComponent<PlayerObjectItem>().SetUp(newPlayer);
    }

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