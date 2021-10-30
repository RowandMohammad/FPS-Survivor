using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private Transform positionOfCamera = null;

    #endregion Private Fields



    #region Private Methods

    private void Update()
    {
        transform.position = positionOfCamera.position;
    }

    #endregion Private Methods
}