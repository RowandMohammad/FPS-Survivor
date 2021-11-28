using UnityEngine;

public class CrouchingCamera

{
    #region Public Fields

    public readonly float standingHeight;

    #endregion Public Fields



    #region Private Fields

    private GameObject camera;

    #endregion Private Fields

    #region Public Constructors

    public CrouchingCamera()
    {
        camera = GameObject.Find("Camera Holder");
        standingHeight = camera.transform.position.y;
    }

    #endregion Public Constructors
}