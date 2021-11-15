using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    [SerializeField] Transform positionOfCamera = null;
    

    private void Awake()
    {
        
    }
    private void Start()
    {

    }

    void Update()
    {
       
        transform.position = positionOfCamera.position;
    }
}