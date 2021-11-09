using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static Launcher Instance;
    [SerializeField] GameObject listedRoomObject;
    [SerializeField] Transform listOfRoomInfo;
    [SerializeField] public TMP_InputField inputtedRoomName;
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] TMP_Text nameOfRoom;
   
    public List<RoomInfo> roomLister;

   
    void Awake()
    {
        Instance = this;
    }

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

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
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

    // Update is called once per frame
    void Update()
    {

    }
}
