using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]private float xSens = 100f;
    [SerializeField]private float ySens = 100f;

    Camera camera;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private void Start() {
        camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }
    private void Update() {
        playerInput();

        camera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        transform.rotation= Quaternion.Euler(0,yRotation,0);
    }

    void playerInput(){
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation+= mouseX * xSens * multiplier;
        xRotation-= mouseY * ySens * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


    }











}

