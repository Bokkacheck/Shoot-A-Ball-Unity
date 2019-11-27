using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int bestScore = 0;
    private static int allCoins = 0;

    public static Text txtAllCoins = null;
    public static Text txtBestScore = null;

    public static int BestScore { get => bestScore;
        set{
            bestScore = value;
            txtBestScore.text ="Best score: "+ bestScore;
        }
    }
    public static int AllCoins{ get => allCoins;
        set
        {
            allCoins = value;
            txtAllCoins.text = "Coins: " + allCoins;
        }
    }

    void Start()
    {
        txtBestScore = GameObject.Find("BestScore").GetComponent<Text>();
        txtAllCoins = GameObject.Find("Coins").GetComponent<Text>();
        BestScore = PlayerPrefs.GetInt("HighScore", 0);
        AllCoins = PlayerPrefs.GetInt("AllCoins", 0);
        if (txtBestScore != null & txtAllCoins != null)
        {
            int newScore = PlayerPrefs.GetInt("Score",0);
            if (newScore > bestScore)
            {
                PlayerPrefs.SetInt("HighScore", BestScore = newScore);
            }
            PlayerPrefs.SetInt("AllCoins", AllCoins = allCoins+newScore);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();
        }
    }
}
