using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject backViewCenter;
    private float currentRotation = 0f;
    private bool myEnabled = true;

    public bool MyEnabled { get => myEnabled; set => myEnabled = value; }

    void FixedUpdate()
    {
        if (!myEnabled) return;
        //float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = JoyStickMove.PerformMovement().x * 2f;
        float mouseY = JoyStickMove.PerformMovement().z * 1.5f;
        currentRotation += mouseY;
        if (currentRotation > 65)
        {
            currentRotation = 65;
        }
        else if(currentRotation < -28)
        {
            currentRotation = -28;
        }
        else
        {
            transform.Rotate(-mouseY, 0, 0);
        }
        transform.RotateAround(transform.position, Vector3.up, mouseX);
        backViewCenter.transform.Rotate(0, mouseX, 0);
    }
}
