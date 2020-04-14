using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Text txtInfo;
    [SerializeField]
    private Image imgInfo;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private Image imgControlsLeft;
    [SerializeField]
    private Image imgControlsRight;
    [SerializeField]
    private Slider sensX;
    [SerializeField]
    private Slider sensY;
    [SerializeField]
    public Material[] materials;
    public static ButtonHandler singleton;
    public static Material selectedMaterial;

    void Awake()
    {
        singleton = this;
        selectedMaterial = materials[PlayerPrefs.GetInt("Style", 0)];
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("Controls", 0) == 0)
        {
            ControlLeft();
        }
        else
        {
            ControlRight();
        }
        sensX.value = PlayerPrefs.GetFloat("SensX", 0.5f);
        sensY.value = PlayerPrefs.GetFloat("SensY", 0.5f);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene1");
    }
    public void ExitGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void Get100Coins()
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0) + 100;
        PlayerPrefs.SetInt("AllCoins", coins);
        PlayerPrefs.Save();
        StartSceneManager.singleton.txtAllCoins.text = " " + coins;
    }
    public void BuyAutoAim()
    {
        BuySomething(200, "AutoAim");
    }
    public void BuyPowerShield()
    {
        BuySomething(600, "PowerShield");
    }
    public void BuyRainOfBullets()
    {
        BuySomething(400, "RainOfBullets");
    }

    public void UpgradeDamage()
    {
        int price =int.Parse(StartSceneManager.singleton.txtDmgPrice.text);
        UpgradeSomething(price, "DamageLevel", "you upgraded Damage");
    }
    public void UpgradeHealth()
    {
        int price = int.Parse(StartSceneManager.singleton.txtHealthPrice.text);
        UpgradeSomething(price, "HealthLevel", "you upgraded Health");
    }
    public void UpgradeLives()
    {
        int price = int.Parse(StartSceneManager.singleton.txtLivesPrice.text);
        UpgradeSomething(price, "Lives", "you get more Lives");
    }
    private void UpgradeSomething(int price, string toUpgrade, string message)
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0);
        int level = PlayerPrefs.GetInt(toUpgrade, 1);
        if (coins < price)
        {
            ShowInfo("You don't have enough coins.");
            return;
        }
        coins -= price;
        StartSceneManager.singleton.txtAllCoins.text = "" + coins;
        PlayerPrefs.SetInt("AllCoins", coins);
        level++;
        PlayerPrefs.SetInt(toUpgrade, level);
        PlayerPrefs.Save();
        StartSceneManager.singleton.Upgrades();
        ShowInfo("Congratulations, "+message);
    }
    private bool BuySomething(int price, string toBuy)
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0);
        if (coins < price)
        {
            ShowInfo("You don't have enough coins.");
            return false;
        }
        if (PlayerPrefs.GetInt(toBuy, 0) == 1)
        {
            ShowInfo("You already have this.");
            return false;
        }
        coins -= price;
        StartSceneManager.singleton.txtAllCoins.text ="" + coins;
        PlayerPrefs.SetInt("AllCoins", coins);
        PlayerPrefs.SetInt(toBuy, 1);
        PlayerPrefs.Save();
        StartSceneManager.singleton.Abilities();
        ShowInfo("Congratulations, you buy "+toBuy+" !");
        return true;
    }
    private void ShowInfo(string info)
    {
        StopAllCoroutines();
        imgInfo.gameObject.SetActive(true);
        txtInfo.text = info;
        StartCoroutine(HideInfoDelay());
    }
    IEnumerator HideInfoDelay()
    {
        yield return new WaitForSeconds(3);
        HideInfo();
    }
    public void HideInfo()
    {
        StopAllCoroutines();
        txtInfo.text = "";
        imgInfo.gameObject.SetActive(false);
    }
    public void ClickSound()
    {
        audio.Play();
    }

    public void SetMusicVolume()
    {
        MusicManager.singleton.music.volume = MusicManager.singleton.sliderMusic.value;
        PlayerPrefs.SetFloat("MusicVolume", MusicManager.singleton.sliderMusic.value);
        PlayerPrefs.Save();
        if (!MusicManager.singleton.music.enabled)
        {
            MusicManager.singleton.music.enabled = true;
        }
        if (!MusicManager.singleton.music.isPlaying)
        {
            MusicManager.singleton.music.Play();
        }
    }
    public void SetGameVolume()
    {
        PlayerPrefs.SetFloat("GameVolume", MusicManager.singleton.sliderGame.value);
        PlayerPrefs.Save();
        MusicManager.singleton.gameVolume = MusicManager.singleton.sliderGame.value;
    }
    public void ControlLeft()
    {
        PlayerPrefs.SetInt("Controls", 0);
        PlayerPrefs.Save();
        imgControlsLeft.enabled = true;
        imgControlsLeft.transform.Find("Frame").GetComponent<Image>().enabled = true;
        imgControlsRight.enabled = false;
        imgControlsRight.transform.Find("Frame").GetComponent<Image>().enabled = false;
    }
    public void ControlRight()
    {
        PlayerPrefs.SetInt("Controls", 1);
        PlayerPrefs.Save();
        imgControlsLeft.enabled = false;
        imgControlsLeft.transform.Find("Frame").GetComponent<Image>().enabled = false;
        imgControlsRight.enabled = true;
        imgControlsRight.transform.Find("Frame").GetComponent<Image>().enabled = true;
    }
    public void SetSensX()
    {
        PlayerPrefs.SetFloat("SensX", sensX.value);
        PlayerPrefs.Save();
        sensX.GetComponentInChildren<Text>().text = "Sensitivity X  :  " + sensX.value.ToString("0.00"); ;
    }
    public void SetSensY()
    {
        PlayerPrefs.SetFloat("SensY", sensY.value);
        PlayerPrefs.Save();
        sensY.GetComponentInChildren<Text>().text = "Sensitivity Y  :  " + sensY.value.ToString("0.00"); ;
    }

    public void BuyStyle(string args)
    {
        int number = int.Parse(args.Split(',')[0]);
        int price = int.Parse(args.Split(',')[1]);
        if (PlayerPrefs.GetInt("Style" + number, 0) == 0)
        {
            BuySomething(price, "Style" + number);
            StartSceneManager.singleton.Styles();
        }
        else
        {
            SetStyle(number);
        }
    }
    private void SetStyle(int number)
    {
        PlayerPrefs.SetInt("Style", number-1);
        PlayerPrefs.Save();
        selectedMaterial = materials[number-1];
        StartSceneManager.singleton.Styles();
    }

}
