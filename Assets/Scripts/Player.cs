using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int health = 100;
    private int kills = 0;
    private int deaths = 0;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Text txtKills;
    [SerializeField]
    private Text txtDeaths;
    [SerializeField]
    private Text txtTime;
    [SerializeField]
    private int time = 60;



    public int Kills { get => kills; set { kills = value; txtKills.text ="Kills: " + kills;} }
    public int Deaths { get => deaths; set { deaths = value; txtDeaths.text = "Deaths: " + deaths; } }

    public void Reset()
    {
        health = maxHealth;
        healthBar.fillAmount = (float)health / maxHealth;
        SpawnEnemy spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        List<GameObject> enemys = spawnEnemy.EnemyArray;
        for (int i = 0; i < enemys.Count; i++)
        {
          StartCoroutine(spawnEnemy.ReCreateEnemy(i));
        }
    }
    public void TakeDmg(int dmg)
    {
        health -= dmg;
        healthBar.fillAmount = (float)health / maxHealth;
        if (health <= 0)
        {
            PlayerPrefs.SetInt("Score", kills);
            PlayerPrefs.Save();
            ResetGame();
            Enemy.smallCubes = null;
            SceneManager.LoadScene("StartScene");
        }
    }
    public void ResetGame()
    {
        Reset();
        txtTime.text = "Time left: " + time;
        time = 60;
        kills = 0;
        deaths = 0;
        txtKills.text = "Kills:";
        txtDeaths.text = "Deaths:";
    }

    //public IEnumerator CountDown()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        txtTime.text = "Time left: " + time;
    //        if(--time == 0)
    //        {
    //            PlayerPrefs.SetInt("Score", kills);
    //            PlayerPrefs.Save();
    //            ResetGame();
    //            Enemy.smallCubes = null;
    //            SceneManager.LoadScene("StartScene");
    //        }
    //    }
    //}
}
