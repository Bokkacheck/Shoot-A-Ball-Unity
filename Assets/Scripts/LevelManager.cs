using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Button autoAim;
    [SerializeField]
    private Text txtAutoAim;
    [SerializeField]
    private Button rainOfBullets;
    [SerializeField]
    private Text txtRainOfBullets;
    [SerializeField]
    private Button powerShield;
    [SerializeField]
    private Text txtPowerShield;
    void Start()
    {
        if (PlayerPrefs.GetInt("AutoAim", 0) == 0)
        {
            TurnOffAutoAim();
        }
        if (PlayerPrefs.GetInt("RainOfBullets", 0) == 0)
        {
            TurnOffRainOfBullets();
        }
        if(PlayerPrefs.GetInt("PowerShield",0) ==0)
        {
            TurnOffPowerShield();
        }
    }
    public void TurnOffAutoAim()
    {
        autoAim.enabled = false;
        autoAim.GetComponent<Image>().enabled = false;
        txtAutoAim.text = "";
    }
    public void TurnOffRainOfBullets()
    {
        rainOfBullets.enabled = false;
        rainOfBullets.GetComponent<Image>().enabled = false;
        txtRainOfBullets.text = "";
    }
    public void TurnOffPowerShield()
    {
        powerShield.enabled = false;
        powerShield.GetComponent<Image>().enabled = false;
        txtPowerShield.text = "";
    }
}
