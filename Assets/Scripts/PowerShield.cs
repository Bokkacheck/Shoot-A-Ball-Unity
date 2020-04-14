using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    private float counter = 0;
    private bool myEnabled = false;
    [SerializeField]
    private LevelManager lvlManager;
    public void TurnOnPowerShield()
    {
        if (myEnabled) return;
        myEnabled = true;
        lvlManager.TurnOffPowerShield();
        lvlManager.powerUpSound.Play();
    }
    void FixedUpdate()
    {
        if (myEnabled)
        {
            counter+=2;
            transform.localScale += new Vector3(counter, counter, counter);
            if (counter > 350)
            {
                myEnabled = false;
                counter = 0;
                transform.localScale = Vector3.zero;
            }
        }
    }
}

