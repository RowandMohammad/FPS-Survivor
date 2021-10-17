using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingCamera

    
{
    private GameObject camera;
    public readonly float standingHeight;

    public CrouchingCamera()
    {
        camera = GameObject.Find("Camera Holder");
        standingHeight = camera.transform.position.y;
    }
 
}
