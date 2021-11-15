
using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
	Vector3 spawn= new Vector3(0f, 0f, 0f);
	


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

		PhotonNetwork.Instantiate(Path.Combine("PhotonObjects", "PlayerObject"), spawn, Quaternion.identity);
	}


}