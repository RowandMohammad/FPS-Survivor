
using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
	Vector3 SpawnPoint = new Vector3(434.792114f, 589.043701f, -17.8297749f);


	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if (PV.IsMine)
		{
			CreateController();
		}
	}

	void CreateController()
	{

		PhotonNetwork.Instantiate(Path.Combine("PhotonObjects", "PlayerObject"), SpawnPoint, Quaternion.identity);
	}


}