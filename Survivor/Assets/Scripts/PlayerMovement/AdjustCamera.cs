using Photon.Pun;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private Transform positionOfCamera = null;
    private PhotonView PV;

    #endregion Private Fields



    #region Private Methods

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        transform.position = positionOfCamera.position;
    }

    #endregion Private Methods
}