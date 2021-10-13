using UnityEngine;


public class Movement
{
    public float movementSpeed; 

    public Movement(float MovementSpeed){
        movementSpeed=MovementSpeed;
    }

    public Vector3 calculate(float horizontalMove){
        var x = horizontalMove * movementSpeed;
        return new Vector3(x,0,0);
    
    
  }

}
