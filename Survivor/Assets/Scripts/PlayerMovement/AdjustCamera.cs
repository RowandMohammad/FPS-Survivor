using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    [SerializeField] Transform positionOfCamera = null;

    void Update()
    {
        transform.position = positionOfCamera.position;
    }
}