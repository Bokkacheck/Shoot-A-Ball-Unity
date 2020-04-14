using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager singleton;
    private int bestScore = 0;
    private int allCoins = 0;

    public Text txtAllCoins = null;
    public Text txtBestScore = null;

    public Text txtDmgPrice;
    public Text txtDmgCurr;
    public Text txtDmgUpgr;

    public Text txtHealthPrice;
    public Text txtHealtgCurr;
    public Text txtHealthUpgr;

    public Text txtLivesPrice;
    public Text txtLivesCurr;
    public Text txtLivesUpgr;

    public Text txtAutoAimPrice;
    public Text txtROBPrice;
    public Text txtPSHPrice;

    public Text txtS1;
    public Text txtS2;
    public Text txtS3;
    public Text txtS4;

    public int BestScore { get => bestScore;
        set{
            bestScore = value;
            txtBestScore.text ="Best score: "+ bestScore;
        }
    }
    public int AllCoins{ get => allCoins;
        set
        {
            allCoins = value;
            txtAllCoins.text = ""+allCoins;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        singleton = this;
        ScoreAndCoins();
        Upgrades();
        Abilities();
        Styles();
    }
    private void ScoreAndCoins()
    {
        PlayerPrefs.SetInt("AllCoins", 10000);
        PlayerPrefs.Save();
        BestScore = PlayerPrefs.GetInt("HighScore", 0);
        AllCoins = PlayerPrefs.GetInt("AllCoins", 0);
        int newScore = PlayerPrefs.GetInt("Score", 0);
        if (newScore > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", BestScore = newScore);
        }
        PlayerPrefs.SetInt("AllCoins", AllCoins = allCoins + newScore);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
    }
    public void Upgrades()
    {
        int damageLevel = PlayerPrefs.GetInt("DamageLevel", 1);
        txtDmgPrice.text = Mathf.Pow(2f, (damageLevel - 1)) * 50 + "";
        txtDmgCurr.text = "Current: "+ Mathf.Pow(2f, (damageLevel - 1)) + "/16";
        txtDmgUpgr.text = "Upgrade: " + Mathf.Pow(2f, (damageLevel));
        if(damageLevel == 5)
        {
            txtDmgPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtDmgPrice.text = "MAXED";
            txtDmgUpgr.text = "Upgrade: ---";
        }
        int healthLevel = PlayerPrefs.GetInt("HealthLevel", 1);
        txtHealthPrice.text = Mathf.Pow(2f, (healthLevel - 1)) * 100 + "";
        txtHealtgCurr.text = "Current: " + Mathf.Pow(2f, (healthLevel - 1)) * 100 + "/1600";
        txtHealthUpgr.text = "Upgrade: " + Mathf.Pow(2f, (healthLevel)) * 100;
        if (healthLevel == 5)
        {
            txtHealthPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtHealthPrice.text = "MAXED";
            txtHealthUpgr.text = "Upgrade: ---";
        }
        int lives = PlayerPrefs.GetInt("Lives", 1);
        txtLivesPrice.text = Mathf.Pow(2f, (lives - 1)) * 250 + "";
        txtLivesCurr.text = "Current: " + lives + "/5";
        txtLivesUpgr.text = "Upgrade: " + (lives+1);
        if (lives == 5)
        {
            txtLivesPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtLivesPrice.text = "MAXED";
            txtLivesUpgr.text = "Upgrade: ---";
        }
    }
    public void Styles()
    {
        int number =  PlayerPrefs.GetInt("Style",0);
        GameObject.Find("Helmet").GetComponent<MeshRenderer>().material = ButtonHandler.singleton.materials[number];
        GameObject.Find("BODY").GetComponent<MeshRenderer>().material = ButtonHandler.singleton.materials[number];
        GameObject.Find("HelmetStyle").GetComponent<MeshRenderer>().material = ButtonHandler.singleton.materials[number];
        GameObject.Find("BODYStyle").GetComponent<MeshRenderer>().material = ButtonHandler.singleton.materials[number];
        SetSyles(txtS1, 1, 50);
        SetSyles(txtS2, 2, 50);
        SetSyles(txtS3, 3, 100);
        SetSyles(txtS4, 4, 100);
    }
    private void SetSyles(Text txt, int number, int price)
    {
        if (PlayerPrefs.GetInt("Style"+number, 0) == 0)
        {
            txt.text = price + "";
        }
        else
        {
            if (PlayerPrefs.GetInt("Style", 0) == number - 1)
            {
                txt.transform.parent.GetComponent<Button>().interactable = false;
                txt.text = "SELECTED";
            }
            else
            {
                txt.transform.parent.GetComponent<Button>().interactable = true;
                txt.text = "SET";
            }
        }
    }
    public void Abilities()
    {
        if (PlayerPrefs.GetInt("AutoAim", 0) == 0)
        {
            txtAutoAimPrice.text = 200 + "";
        }
        else
        {
            txtAutoAimPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtAutoAimPrice.text = "OWNED";
        }
        if (PlayerPrefs.GetInt("RainOfBullets", 0) == 0)
        {
            txtROBPrice.text = 400 + "";
        }
        else
        {
            txtROBPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtROBPrice.text = "OWNED";
        }
        if (PlayerPrefs.GetInt("PowerShield", 0) == 0)
        {
            txtPSHPrice.text = 600 + "";
        }
        else
        {
            txtPSHPrice.transform.parent.GetComponent<Button>().interactable = false;
            txtPSHPrice.text = "OWNED";
        }
    }
}
