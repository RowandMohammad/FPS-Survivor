using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private Transform camera;
    private float mouseX;
    private float mouseY;
    private float multiplier = 0.01f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform orientationOfCharacter;
    private float xRotation;
    [SerializeField] private float xSens = 100f;
    private float yRotation;
    [SerializeField] private float ySens = 100f;

    #endregion Private Fields



    #region Private Methods

    private void playerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * xSens * multiplier;
        xRotation -= mouseY * ySens * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 75f);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        playerInput();

        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientationOfCharacter.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    #endregion Private Methods
}