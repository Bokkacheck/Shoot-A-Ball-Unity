using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private List<GameObject> enemyArray;
    [SerializeField]
    private GameObject smallEnemy;
    [SerializeField]
    private GameObject bigEnemy;
    [SerializeField]
    private Transform player;
    private float radius = 230f;
    private int numberOfRespawn = 0;

    public List<GameObject> EnemyArray { get => enemyArray; set => enemyArray = value; }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0, -10000,0);
        enemyArray = new List<GameObject>() { new GameObject(),new GameObject(),new GameObject(),new GameObject(),new GameObject()};
        for(int i = 0; i < enemyArray.Count; i++)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector3 newPosition = new Vector3(radius * Mathf.Cos(angle), 10, radius * Mathf.Sin(angle));
            if (i % 2 == 0)
            {
                enemyArray[i] = Instantiate(smallEnemy, newPosition, Quaternion.identity);
            }
            else
            {
                enemyArray[i] = Instantiate(bigEnemy, newPosition, Quaternion.identity);
            }
            enemyArray[i].GetComponent<Enemy>().enemyID = i;
        }
        AutoAim.enemys = enemyArray;
    }
    public IEnumerator ReCreateEnemy(int ID)
    {
        enemyArray[ID].transform.position = new Vector3(0, -1000, 0);
        enemyArray[ID].GetComponent<Enemy>().CurrentHealth = enemyArray[ID].GetComponent<Enemy>().MaxHealth;
        yield return new WaitForSeconds(4f);
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 newPosition = new Vector3(radius * Mathf.Cos(angle), 10, radius * Mathf.Sin(angle));
        enemyArray[ID].transform.position = newPosition;
        enemyArray[ID].GetComponent<Rigidbody>().velocity = Vector3.zero;
        numberOfRespawn++;
        if (numberOfRespawn % 4 == 0)
        {
            angle = Random.Range(0, 2 * Mathf.PI);
            newPosition = new Vector3(radius * Mathf.Cos(angle), 30, radius * Mathf.Sin(angle));
            enemyArray.Add(Instantiate(smallEnemy, newPosition, Quaternion.identity));
            enemyArray[enemyArray.Count - 1].GetComponent<Enemy>().enemyID = enemyArray.Count - 1;
            enemyArray[enemyArray.Count - 1].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

}
