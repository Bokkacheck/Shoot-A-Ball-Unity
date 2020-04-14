using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int health = 100;
    private int kills = 0;

    [HideInInspector]
    public int damage = 0;
    private int numberOfLives;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Text txtKills;
    [SerializeField]
    private Text txtHealth;
    [SerializeField]
    private GameObject lives;
    [SerializeField]
    private Image imgLive;
    [SerializeField]
    MeshRenderer body;
    [SerializeField]
    MeshRenderer helmet;

    private int nextLevelKills;

    public static Player singleton;

    void Start()
    {
        singleton = this;
        helmet.material = body.material = ButtonHandler.selectedMaterial;
        int healthLevel = PlayerPrefs.GetInt("HealthLevel", 1);
        maxHealth = (int)Mathf.Pow(2f, (healthLevel - 1)) * 100;
        damage = (int)Mathf.Pow(2f,PlayerPrefs.GetInt("DamageLevel", 1)-1);
        numberOfLives = PlayerPrefs.GetInt("Lives", 1);
        health = maxHealth;
        for(int i = 0; i < numberOfLives; i++)
        {
            Image img = Instantiate(imgLive);
            img.gameObject.transform.parent = lives.gameObject.transform;
        }
        nextLevelKills = 30;
        txtHealth.text = health + "/" + maxHealth;
        txtKills.text = "Kills: " + kills + "/" + nextLevelKills;
    }

    public void TakeDmg(int dmg)
    {
        if (health == 0)
        {
            return;
        }
        health -= dmg;
        if (health <= 0)
        {
            numberOfLives--;
            Destroy(lives.transform.Find("LiveImage(Clone)").gameObject);
            if (numberOfLives == 0)
            {
                health = 0;
                LevelManager.singleton.GameOver(kills);
            }
            else
            {
                health = maxHealth;
            }
        }
        healthBar.fillAmount = (float)health / maxHealth;
        txtHealth.text = health + "/" + maxHealth;
    }
    public bool MakeKill()
    {
        kills++;
        txtKills.text = "Kills: " + kills + "/" + nextLevelKills;
        if (kills == nextLevelKills)
        {
            nextLevelKills += 30;
            health = maxHealth;
            healthBar.fillAmount = (float)health / maxHealth;
            txtHealth.text = health + "/" + maxHealth;
            LevelManager.singleton.NextLevel();
            txtKills.text = "Kills: " + kills + "/" + nextLevelKills;
            return true;
        }
        return false;
    }
}
