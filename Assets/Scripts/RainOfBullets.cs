using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfBullets : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private GameObject[] bullets;
    private bool myEnabled = false;
    [SerializeField]
    private LevelManager lvlManager;

    public bool MyEnabled { get => myEnabled; set { myEnabled = value; StartRain(); } }
    void Start()
    {
        bullets = new GameObject[500];
        float distance = 0;
        float angle = 0;
        for(int i = 0; i < 500;i++)
        {
            angle += (float)((2.0 * Mathf.PI) / 55.0);
            if (i < 20) distance += 1.2f;
            else if (i < 50) distance += 0.8f;
            else if (i < 100) distance += 0.6f;
            else distance += 0.3f;
                Vector3 position = new Vector3(Mathf.Sin(angle) * distance, Random.Range(200,1400), Mathf.Cos(angle) * distance);
            bullets[i] = Instantiate(bullet, transform.position + position, Quaternion.identity);
        }
    }
    void StartRain()
    {
        for(int i = 0; i < 500; i++)
        {
            bullets[i].GetComponent<Rigidbody>().AddForce(Vector3.up * -300, ForceMode.Impulse);
            Destroy(bullets[i], 8f);
        }
        lvlManager.TurnOffRainOfBullets();
    }

}
