using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    int coins;
    [SerializeField]
    private Text txtCoins;
    private static GameObject infoButton;
    private static Text txtInfo;
    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("AllCoins", 0);
        txtCoins.text = "Coins: " + coins;
        infoButton = GameObject.Find("INFO");
        txtInfo = GameObject.Find("INFOtext").GetComponent<Text>();
    }
    public static void ShowInfo(string message)
    {
        infoButton.GetComponent<Image>().enabled = true;
        txtInfo.text = message;
    }
}
