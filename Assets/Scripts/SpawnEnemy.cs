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

    private int smallHealth;
    private int bigHealth;
    private int smallDamage;
    private int bigDamage;
    private float speed;
    private int pieces = 4;

    [HideInInspector]
    public List<GameObject> EnemyArray { get => enemyArray; set => enemyArray = value; }
    public static SpawnEnemy singleton;
    [SerializeField]
    private GameObject pieceBox;
    [SerializeField]
    private GameObject pieceSphere;

    // Start is called before the first frame update
    void Start()
    {
        if (Enemy.smallCubes == null)
        {
            Enemy.smallCubes = new GameObject[pieces, pieces, pieces];
            for (int i = 0; i < pieces; i++)
                for (int j = 0; j < pieces; j++)
                    for (int k = 0; k < pieces; k++)
                    {
                        if (i % 2 == 0)
                        {
                            Enemy.smallCubes[i, j, k] = Instantiate(pieceBox);
                        }
                        else
                        {
                            Enemy.smallCubes[i, j, k] = Instantiate(pieceSphere);
                        }
                    }
        }

        singleton = this;
        smallHealth = 1;
        bigHealth = 2;
        smallDamage = 5;
        bigDamage = 10;
        speed = 1f;
        NextLevel(1);
    }
    public void NextLevel(int level)
    {
        StopAllCoroutines();
        if (level != 1)
        {
            smallHealth += 1;
            bigHealth += 2;
            smallDamage += 5;
            bigDamage += 10;
            speed += 0.1f;
            for (int i = 0; i < enemyArray.Count; i++)
            {
                enemyArray[i].GetComponent<Enemy>().Destroy();
                Destroy(enemyArray[i]);
            }
            numberOfRespawn = 0;
        }
        enemyArray = new List<GameObject>();
        AutoAim.enemys = enemyArray;
        StartCoroutine(StartLevel());
    }
    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(3);
        Vector3 position = new Vector3(0, -10000, 0);
        for (int i = 0; i < 4; i++)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector3 newPosition = new Vector3(radius * Mathf.Cos(angle), 10, radius * Mathf.Sin(angle));
            if (i % 2 == 0)
            {
                enemyArray.Add(Instantiate(smallEnemy, newPosition, Quaternion.identity));
                enemyArray[i].GetComponent<Enemy>().SetData(i, smallHealth, smallDamage, speed);

            }
            else
            {
                enemyArray.Add(Instantiate(bigEnemy, newPosition, Quaternion.identity));
                enemyArray[i].GetComponent<Enemy>().SetData(i, bigHealth, bigDamage, speed);
            }
        }

    }
    public IEnumerator ReCreateEnemy(int ID)
    {
        if (ID<enemyArray.Count && enemyArray[ID] != null)
        {
            enemyArray[ID].transform.position = new Vector3(0, -1000, 0);
            enemyArray[ID].GetComponent<Enemy>().CurrentHealth = enemyArray[ID].GetComponent<Enemy>().MaxHealth;
            yield return new WaitForSeconds(4f);
            if (ID < enemyArray.Count && enemyArray[ID] != null)
            {
                float angle = Random.Range(0, 2 * Mathf.PI);
                Vector3 newPosition = new Vector3(radius * Mathf.Cos(angle), 10, radius * Mathf.Sin(angle));
                enemyArray[ID].transform.position = newPosition;
                enemyArray[ID].GetComponent<Rigidbody>().velocity = Vector3.zero;
                if(numberOfRespawn != 49)
                {
                    numberOfRespawn++;
                    if (numberOfRespawn % 6 == 0)
                    {
                        angle = Random.Range(0, 2 * Mathf.PI);
                        newPosition = new Vector3(radius * Mathf.Cos(angle), 10, radius * Mathf.Sin(angle));
                        if (numberOfRespawn == 24)
                        {
                            enemyArray.Add(Instantiate(bigEnemy, newPosition, Quaternion.identity));
                            enemyArray[enemyArray.Count - 1].GetComponent<Enemy>().SetData(enemyArray.Count - 1, bigHealth, bigDamage, speed);
                        }
                        else
                        {
                            enemyArray.Add(Instantiate(smallEnemy, newPosition, Quaternion.identity));
                            enemyArray[enemyArray.Count - 1].GetComponent<Enemy>().SetData(enemyArray.Count - 1, smallHealth, smallDamage, speed);
                        }
                        enemyArray[enemyArray.Count - 1].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
            }
        }
    }

}
