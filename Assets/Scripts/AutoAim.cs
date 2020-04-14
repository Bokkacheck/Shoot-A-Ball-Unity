using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAim : MonoBehaviour
{
    public static List<GameObject> enemys = null;
    [SerializeField]
    private PlayerControll playerControll;
    private float counter = 0;
    private bool myEnabled = false;
    [SerializeField]
    private Button btnShoot;

    public bool MyEnabled { get => myEnabled; set {  } }

    public void TurnOnAutoAim()
    {
        if (myEnabled) return;
        myEnabled = true;
        playerControll.MyEnabled = false;
        LevelManager.singleton.powerUpSound.Play();
        btnShoot.enabled = false;
        StartCoroutine(TurnOffAutoAim());
    }

    void FixedUpdate()
    {
        if (!myEnabled) return;
        if (enemys == null || enemys.Count==0)
        {
            return;
        }
        int index = 0;
        float minDistance = 1000f;
        for(int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].transform.position.y < 0 || enemys[i].transform.position.y>40)
            {
                continue;
            }
            float distance =Mathf.Abs((enemys[i].transform.position - transform.position).magnitude);
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }
        Vector3 whereToShoot = enemys[index].transform.position + enemys[index].GetComponent<Rigidbody>().velocity * (minDistance / 400f) - transform.position;
        if (whereToShoot.y < -10)
        {
            whereToShoot.y = 10;
        }
        Quaternion aimRotate = Quaternion.LookRotation(whereToShoot);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRotate, 8 * Time.deltaTime);
        counter += Time.deltaTime;
        if (counter >= 10*Time.deltaTime && minDistance<1000)
        {
            counter = 0f;
            Shooting.singleton.Shoot();
        }
    }
    private IEnumerator TurnOffAutoAim()
    {
        int counter = 10;
        int lvl = LevelManager.singleton.currentLevel;
        while (true)
        {
            LevelManager.singleton.txtAutoAim.text = "" + counter;
            yield return new WaitForSeconds(1f);
            if (counter-- == 0)
            {
                if (lvl == LevelManager.singleton.currentLevel)
                {
                    LevelManager.singleton.TurnOffAutoAim();
                }
                else
                {
                    LevelManager.singleton.TurnOnAutoAim();
                }
                myEnabled = false;
                btnShoot.enabled = true;
                break;
            }
        }
        playerControll.MyEnabled = true;
    }
}
