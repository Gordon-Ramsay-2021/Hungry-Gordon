using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//loacks cursor to screen
        Cursor.visible = false; //makes cursor invisible
    }

    public void UpdateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //takes in the user mouse movement on the X axis and times by the mouse sensitivity
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //takes in the user mouse movement on the Y axis and times by the mouse sensitivity

        xRotation -= mouseY; // invert the output to make it more natural
        xRotation = Mathf.Clamp(xRotation, -85f, 85f); //clamp it to stop you from spinning camera 360

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotates the camera up and down on the local X axis
        playerBody.Rotate(Vector3.up * mouseX);// rotates the player body on the cameras Y rotation
    }
}
