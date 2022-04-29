using UnityEngine;

public class Turn_Move : MonoBehaviour
{
    #region Public Fields

    public int MoveX;
    public int MoveY;
    public int MoveZ;
    public int TurnX;
    public int TurnY;
    public int TurnZ;
    public bool World;

    #endregion Public Fields



    #region Private Methods

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (World == true)
        {
            transform.Rotate(TurnX * Time.deltaTime, TurnY * Time.deltaTime, TurnZ * Time.deltaTime, Space.World);
            transform.Translate(MoveX * Time.deltaTime, MoveY * Time.deltaTime, MoveZ * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(TurnX * Time.deltaTime, TurnY * Time.deltaTime, TurnZ * Time.deltaTime, Space.Self);
            transform.Translate(MoveX * Time.deltaTime, MoveY * Time.deltaTime, MoveZ * Time.deltaTime, Space.Self);
        }
    }

    #endregion Private Methods
}