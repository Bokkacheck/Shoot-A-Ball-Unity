using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private Transform playerModel;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private Rigidbody rb;
    private Vector3 direction;
    public int enemyID;
    [SerializeField]
    private int pieces = 5;
    [SerializeField]
    private int enemyDamage;
    [SerializeField]
    private SpawnEnemy spawnEnemy;
    [SerializeField]
    private int maxHealth;
    private int currentHealth; 
    public static GameObject[,,] smallCubes = null;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private Player playerScript;
    Image healthBar;
    [SerializeField]
    GameObject canvas;

    public int CurrentHealth { get => currentHealth; set { currentHealth = value;healthBar.fillAmount = (float)currentHealth/maxHealth;} }

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerModel = player.Find("Sphere");
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        currentHealth = maxHealth;
        MakeHealthBar();
        if (smallCubes == null)
        {
            smallCubes = new GameObject[pieces, pieces, pieces];
            for (int i = 0; i < pieces; i++)
                for (int j = 0; j < pieces; j++)
                    for (int k = 0; k < pieces; k++)
                    {
                        if (i % 2 == 0)
                        {
                            smallCubes[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        }
                        else
                        {
                            smallCubes[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        }
                        smallCubes[i, j, k].transform.position = new Vector3(0, -300, 0);
                        smallCubes[i, j, k].transform.localScale = new Vector3(2f, 2, 2f);
                        smallCubes[i, j, k].AddComponent<Rigidbody>();
                        smallCubes[i, j, k].GetComponent<MeshRenderer>().material = mat;
                    }
        }

    }
    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction = direction.normalized;
        rb.AddForce(direction * speed, ForceMode.Impulse);

        canvas.transform.position = transform.position + new Vector3(0, -3, 0);
        canvas.transform.rotation = playerModel.rotation;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Bullet")
        {
            CurrentHealth -= 1;
            if (currentHealth <= 0)
            {
                playerScript.Kills += 1;
                Respawn();
            }
        }
        else if(other.collider.tag == "PowerShell")
        {
            CurrentHealth = 0;
            playerScript.Kills += 1;
            Respawn();
        }
        else if(other.collider.tag == "Player")
        {
            playerScript.TakeDmg(enemyDamage);
        }
    }
    void Respawn()
    {
        SetExplosion();
        StartCoroutine(spawnEnemy.ReCreateEnemy(enemyID));
    }
    void SetExplosion()
    {
        for (int i = 0; i < pieces; i++)
            for (int j = 0; j < pieces; j++)
                for (int k = 0; k < pieces; k++)
                {
                    smallCubes[i, j, k].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    smallCubes[i,j,k].transform.position = transform.position + new Vector3(i,j,k);
                    smallCubes[i,j,k].GetComponent<Rigidbody>().AddForce(rb.velocity.normalized  * 5, ForceMode.Impulse);
                }
    }
    void MakeHealthBar()
    {
        canvas = Instantiate(canvas, transform.position + new Vector3(0, 4, 0), Quaternion.identity);
        healthBar = canvas.transform.Find("Image").Find("Image2").gameObject.GetComponent<Image>();
        healthBar.fillAmount = 1.0f;
    }

}
