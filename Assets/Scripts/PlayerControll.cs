using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private float sensX;
    private float sensY;
    private float currentRotation = 0f;
    private bool myEnabled = true;
    public bool MyEnabled { get => myEnabled; set{ myEnabled = value; currentRotation = 10-transform.rotation.x; } }
    
    void Start()
    {
        sensX = PlayerPrefs.GetFloat("SensX", 0.5f) * 4;
        sensY = PlayerPrefs.GetFloat("SensY", 0.5f) * 3;
    }

    void FixedUpdate()
    {
        if (!myEnabled) return;
        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = JoyStickMove.inputVector.x * sensX;
        float mouseY = JoyStickMove.inputVector.z * sensY;
        currentRotation += mouseY;
        if (currentRotation > 40)
        {
            currentRotation = 40;
        }
        else if(currentRotation < -10)
        {
            currentRotation = -10;
        }
        else
        {
            transform.Rotate(-mouseY, 0, 0);
        }
        transform.RotateAround(transform.position, Vector3.up, mouseX);
    }
}
