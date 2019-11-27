using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Shooting shooting;
    [SerializeField]
    private AutoAim autoAim;
    [SerializeField]
    private RainOfBullets rainOfBullets;
    [SerializeField]
    private PowerShield powerShield;
    public void Shoot()
    {
        shooting.Shoot();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene1");
    }
    public void EnterShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ExitGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void TurnOnAutoAim()
    {
        if (autoAim.MyEnabled) return;
        autoAim.MyEnabled = true;
    }
    public void TurnOnBulletRain()
    {
        if (rainOfBullets.MyEnabled) return;
        rainOfBullets.MyEnabled = true;
    }
    public void TurnOnPowerShield()
    {
        if (powerShield.MyEnabled) return;
        powerShield.MyEnabled = true;
    }
    public void GoBack()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Get100Coins()
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0) + 100;
        PlayerPrefs.SetInt("AllCoins", coins);
        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins;
        PlayerPrefs.Save();
    }
    public void HideInfo()
    {
         GameObject.Find("INFO").GetComponent<Image>().enabled = false;
         GameObject.Find("INFOtext").GetComponent<Text>().text = "";
    }
    public void BuyAutoAim()
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0);
        if (coins < 100)
        {
            ShopManager.ShowInfo("You don't have enough money for this item.");
            return;
        }
        if (PlayerPrefs.GetInt("AutoAim", 0) == 1)
        {
            ShopManager.ShowInfo("You already have this item.");
            return;
        }
        coins -= 100;
        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins;
        PlayerPrefs.SetInt("AllCoins", coins);
        PlayerPrefs.SetInt("AutoAim", 1);
        PlayerPrefs.Save();
        ShopManager.ShowInfo("Succesfull, you now have AutoAim item.");
    }
    public void BuyRainOfBullets()
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0);
        if (coins < 200)
        {
            ShopManager.ShowInfo("You don't have enough money for this item.");
            return;
        }
        if (PlayerPrefs.GetInt("RainOfBullets", 0) == 1)
        {
            ShopManager.ShowInfo("You already have this item.");
            return;
        }
        coins -= 200;
        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins;
        PlayerPrefs.SetInt("AllCoins", coins);
        PlayerPrefs.SetInt("RainOfBullets", 1);
        PlayerPrefs.Save();
        ShopManager.ShowInfo("Succesfull, you now have RainOfBullets item.");
    }
    public void BuyPowerShield()
    {
        int coins = PlayerPrefs.GetInt("AllCoins", 0);
        if (coins < 300)
        {
            ShopManager.ShowInfo("You don't have enough money for this item.");
            return;
        }
        if (PlayerPrefs.GetInt("PowerShield", 0) == 1)
        {
            ShopManager.ShowInfo("You already have this item.");
            return;
        }
        coins -= 300;
        GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins;
        PlayerPrefs.SetInt("AllCoins", coins);
        PlayerPrefs.SetInt("PowerShield", 1);
        PlayerPrefs.Save();
        ShopManager.ShowInfo("Succesfull, you now have PowerShield item.");
    }
}
