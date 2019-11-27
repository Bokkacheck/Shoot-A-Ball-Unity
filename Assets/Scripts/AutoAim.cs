using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAim : MonoBehaviour
{
    public static List<GameObject> enemys = null;
    [SerializeField]
    private Shooting shooting;
    [SerializeField]
    private PlayerControll playerControll;
    [SerializeField]
    private Text txtAutoAim;
    private float counter = 0;
    private bool myEnabled = false;
    [SerializeField]
    private LevelManager lvlManager;
    private Quaternion rotationBeforeAutoAim;
    private bool started = false;

    public bool MyEnabled { get => myEnabled; set { myEnabled = value;StartCoroutine(TurnOffAutoAim()); } }

    void Update()
    {
        if (!myEnabled) return;
        if (enemys == null)
        {
            return;
        }
        if (!started)
        {
            rotationBeforeAutoAim = transform.rotation;
            started = true;
        }
        int index = 0;
        float minDistance = 1000f;
        for(int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].transform.position.y < 0)
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
        if (counter >= 0.1f)
        {
            counter = 0f;
            shooting.Shoot();
        }
    }
    private IEnumerator TurnOffAutoAim()
    {
        int counter = 10;
        while (true)
        {
            txtAutoAim.text = "" + counter;
            yield return new WaitForSeconds(1f);
            if (counter-- == 0)
            {
                lvlManager.TurnOffAutoAim();
                transform.rotation = rotationBeforeAutoAim;
                myEnabled = false;
                break;
            }
        }
        playerControll.MyEnabled = true;
    }
}
