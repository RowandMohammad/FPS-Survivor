using UnityEngine;

public class Movement
{
    #region Public Fields

    public float movementSpeed;

    #endregion Public Fields



    #region Private Fields

    private Vector3 moveDirection;

    #endregion Private Fields

    #region Public Constructors

    public Movement(float MovementSpeed)
    {
        movementSpeed = MovementSpeed;
    }

    #endregion Public Constructors



    #region Public Methods

    public Vector3 calculate(float horizontalMove, float verticalMove)
    {
        var x = horizontalMove;
        var z = verticalMove;
        moveDirection = new Vector3(x, 0, z);
        moveDirection = moveDirection.normalized * movementSpeed;
        return moveDirection;
    }

    #endregion Public Methods
}