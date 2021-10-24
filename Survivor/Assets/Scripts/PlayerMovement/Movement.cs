using UnityEngine;


public class Movement
{
    public float movementSpeed;

    Vector3 moveDirection;
    

    public Movement(float MovementSpeed)
    {
        movementSpeed = MovementSpeed;
    }

    public Vector3 calculate(float horizontalMove, float verticalMove)
    {
        var x = horizontalMove;
        var z = verticalMove ;
        moveDirection = new Vector3(x, 0, z);
        moveDirection = moveDirection.normalized * movementSpeed;
        return moveDirection;


    }




}
