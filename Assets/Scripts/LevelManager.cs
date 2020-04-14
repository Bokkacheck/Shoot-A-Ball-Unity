using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Button autoAim;
    [SerializeField]
    public Text txtAutoAim;
    [SerializeField]
    private Button rainOfBullets;
    [SerializeField]
    private Text txtRainOfBullets;
    [SerializeField]
    private Button powerShield;
    [SerializeField]
    private Text txtPowerShield;

    [SerializeField]
    private GameObject leftControls;
    [SerializeField]
    private GameObject rightControls;

    public AudioSource powerUpSound;
    [SerializeField]
    private AudioSource clickSound;
    [SerializeField]
    private Canvas pauseCanvas;

    [SerializeField]
    private Image imgMusic;
    [SerializeField]
    private Sprite musicOn;
    [SerializeField]
    private Sprite musicOff;

    [SerializeField]
    private Image imgSound;
    [SerializeField]
    private Sprite soundOn;
    [SerializeField]
    private Sprite soundOff;
    [SerializeField]
    private Text txtLevel;
    [SerializeField]
    private GameObject lvlCanvas;
    [SerializeField]
    private GameObject endGameView;

    //Level managment
    [SerializeField]
    private SpawnEnemy spawnEnemy;
    [HideInInspector]
    public int currentLevel;

    public static LevelManager singleton;


    void Start()
    {
        singleton = this;
        imgMusic.sprite = MusicManager.singleton.music.enabled ? musicOn : musicOff;
        powerUpSound.volume = MusicManager.singleton.gameVolume;

        if (PlayerPrefs.GetInt("Controls", 0) == 0)
        {
            leftControls.SetActive(true);
        }
        else
        {
            leftControls.SetActive(false);
            rightControls.SetActive(true);
            autoAim = GameObject.Find("AutoAim").GetComponent<Button>();
            txtAutoAim = autoAim.transform.Find("Text").GetComponent<Text>();
            rainOfBullets = GameObject.Find("BulletRain").GetComponent<Button>();
            txtRainOfBullets = rainOfBullets.transform.Find("Text").GetComponent<Text>();
            powerShield = GameObject.Find("PowerShell").GetComponent<Button>();
            txtPowerShield = powerShield.transform.Find("Text").GetComponent<Text>();
        }
        if (PlayerPrefs.GetInt("AutoAim", 0) == 0)
        {
            TurnOffAutoAim();
        }
        if (PlayerPrefs.GetInt("RainOfBullets", 0) == 0)
        {
            TurnOffRainOfBullets();
        }
        if (PlayerPrefs.GetInt("PowerShield", 0) == 0)
        {
            TurnOffPowerShield();
        }
        currentLevel = 1;
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
    public void PauseGame()
    {
        pauseCanvas.enabled = true;
        Shooting.singleton.drawCrosshair = false;
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        pauseCanvas.enabled = false;
        Shooting.singleton.drawCrosshair = true;
        Time.timeScale = 1;
    }
    public void ResetGame()
    {
        Time.timeScale = 1;
        Enemy.smallCubes = null;
        SceneManager.LoadScene("GameScene1");
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        Enemy.smallCubes = null;
        SceneManager.LoadScene("StartScene");
    }
    public void HandleMusic()
    {
        MusicManager.singleton.HandleMusic();
        if (MusicManager.singleton.music.enabled)
        {
            imgMusic.sprite = musicOn;
        }
        else
        {
            imgMusic.sprite = musicOff;
        }
    }
    public void HandleSound()
    {
        Enemy.soundFlag = !Enemy.soundFlag;
        powerUpSound.enabled = !powerUpSound.enabled;
        Shooting.singleton.shootSound.enabled = !Shooting.singleton.shootSound.enabled;
        if (Enemy.soundFlag)
        {
            imgSound.sprite = soundOn;
        }
        else
        {
            imgSound.sprite = soundOff;
        }
    }
    public void ClickSound()
    {
        clickSound.Play();
    }
    public void GameOver(int kills)
    {
        Shooting.singleton.drawCrosshair = false;
        endGameView.transform.Find("Score").Find("Text").GetComponent<Text>().text = "SCORE: "+kills;
        endGameView.transform.Find("Coins").Find("Text").GetComponent<Text>().text = "Coins EARNED: " + kills;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        endGameView.transform.Find("HighScore").Find("Text").GetComponent<Text>().text = "high score: " + (highScore>kills? highScore:kills);
        endGameView.SetActive(true);
        PlayerPrefs.SetInt("Score", kills);
        PlayerPrefs.Save();
        Enemy.smallCubes = null;
        StartCoroutine(BackToMenuCorutine());
    }
    public IEnumerator BackToMenuCorutine()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("StartScene");
    }

    //Level managment

    public void TurnOnAutoAim()
    {
        if (PlayerPrefs.GetInt("AutoAim", 0) == 1)
        {
            autoAim.enabled = true;
            autoAim.GetComponent<Image>().enabled = true;
            txtAutoAim.text = "AutoAim";
        }
    }
    public void TurnOnRainOfBullets()
    {
        if (PlayerPrefs.GetInt("RainOfBullets", 0) == 1)
        {
            rainOfBullets.enabled = true;
            rainOfBullets.GetComponent<Image>().enabled = true;
            txtRainOfBullets.text = "RainOfBullets";
        }
    }
    public void TurnOnPowerShield()
    {
        if (PlayerPrefs.GetInt("PowerShield", 0) == 1)
        {
            powerShield.enabled = true;
            powerShield.GetComponent<Image>().enabled = true;
            txtPowerShield.text = "PowerShield";
        }
    }
    public void NextLevel()
    {
        currentLevel++;
        txtLevel.text = "Level: " + currentLevel;
        TurnOnAutoAim();
        TurnOnRainOfBullets();
        TurnOnPowerShield();
        lvlCanvas.SetActive(true);
        StartCoroutine(HideLevelUp());
        spawnEnemy.NextLevel(currentLevel);
    }
    public IEnumerator HideLevelUp()
    {
        yield return new WaitForSeconds(2.7f);
        lvlCanvas.SetActive(false);
    } 

}
