using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfBullets : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private ParticleSystem smoke;
    private GameObject[] bullets;
    private bool myEnabled = false;
    private Vector3 [] positions;
    private GameObject parent;
    public void TurnOnBulletRain()
    {
        if (myEnabled) return;
        myEnabled = true;
        StartRain();
    }
    void Start()
    {
        parent = new GameObject();
        bullets = new GameObject[500];
        positions = new Vector3[500];
        float distance = 0;
        float angle = 0;
        float increment = (float)((2.0 * Mathf.PI) / 55.0);
        for (int i = 0; i < 300;i++)
        {
            angle += increment;
            if (i < 30) distance = 30;
            else if (i < 50) distance += 1f;
            else if (i < 100) distance += 0.8f;
            else if (i < 300) distance += 0.6f;
            else distance += 0.4f;
            Vector3 position = new Vector3(Mathf.Sin(angle) * distance, Random.Range(400,1000), Mathf.Cos(angle) * distance);
            positions[i] = position;
            bullets[i] = Instantiate(bullet, transform.position + position, Quaternion.identity);
            bullets[i].transform.parent = parent.transform;
        }
        parent.SetActive(false);
    }
    void StartRain()
    {
        LevelManager.singleton.powerUpSound.Play();
        parent.SetActive(true);
        for(int i = 0; i < 300; i++)
        {
            bullets[i].GetComponent<Rigidbody>().AddForce(Vector3.up * -200, ForceMode.Impulse);
        }
        smoke.Play();
        StartCoroutine(StopRain());
        LevelManager.singleton.TurnOffRainOfBullets();
    }
    IEnumerator StopRain()
    {
        yield return new WaitForSeconds(10);
        myEnabled = false;
        for(int i = 0; i < 300; i++)
        {
            bullets[i].transform.position = positions[i];
            bullets[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        smoke.Stop();
        parent.SetActive(false);
    }
}
