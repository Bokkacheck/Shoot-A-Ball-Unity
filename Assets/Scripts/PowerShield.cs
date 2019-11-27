using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    private float counter = 0;
    private bool myEnabled = false;
    [SerializeField]
    private LevelManager lvlManager;
    public bool MyEnabled { get => myEnabled; set { myEnabled = value; } }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myEnabled)
        {
            transform.localScale += new Vector3(1, 1, 1) * counter++;
        }
        if (counter > 250)
        {
            myEnabled = false;
            lvlManager.TurnOffPowerShield();
            Destroy(gameObject);
        }
    }
}

