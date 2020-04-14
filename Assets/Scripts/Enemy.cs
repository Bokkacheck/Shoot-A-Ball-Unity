using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private float speed = 0.5f;
    private Rigidbody rb;
    private static int pieces = 4;
    private int maxHealth = 100;
    private int currentHealth = 100; 
    [HideInInspector]
    public static GameObject[,,] smallCubes = null;
    [SerializeField]
    private Material mat;
    private Image healthBar;
    [SerializeField]
    private GameObject canvas;

    private int id;

    private int damage;

    public int CurrentHealth { get => currentHealth; set { currentHealth = value; healthBar.fillAmount = (float)currentHealth/maxHealth;} }

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public int Id { get => id; set => id = value; }

    public static bool soundFlag = true;

    void Start()
    {
        GetComponent<AudioSource>().volume = MusicManager.singleton.gameVolume;
        player = GameObject.Find("Head").transform;
        rb = GetComponent<Rigidbody>();
        MakeHealthBar();
    }
    void FixedUpdate()
    {
        rb.AddForce((player.position - transform.position).normalized * speed, ForceMode.Impulse);
        canvas.transform.position = transform.position + new Vector3(0, -3, 0);
        canvas.transform.rotation = player.rotation;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Bullet")
        {
            CurrentHealth -= Player.singleton.damage;
            if (currentHealth <= 0)
            {
                if (Player.singleton.MakeKill())
                {
                    return;
                }
                Respawn();
            }
        }
        else if(other.collider.tag == "PowerShell")
        {
            CurrentHealth = 0;
            if (Player.singleton.MakeKill())
            {
                return;
            }
            Respawn();
        }
        else if(other.collider.tag == "Player")
        {
            Player.singleton.TakeDmg(damage);
            StartCoroutine(SpawnEnemy.singleton.ReCreateEnemy(id));
        }
    }
    void Respawn()
    {
        SetExplosion();
        StartCoroutine(SpawnEnemy.singleton.ReCreateEnemy(id));
    }
    void SetExplosion()
    {
        if (soundFlag)
        {
            GetComponent<AudioSource>().Play();
        }
        Vector3 force = rb.velocity.normalized + Vector3.up * 8;
        for (int i = 0; i < pieces; i++)
            for (int j = 0; j < pieces; j++)
                for (int k = 0; k < pieces; k++)
                {
                    smallCubes[i, j, k].transform.position = transform.position;
                    smallCubes[i, j, k].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                }
    }
    void MakeHealthBar()
    {
        canvas = Instantiate(canvas, transform.position + new Vector3(0, 4, 0), Quaternion.identity);
        healthBar = canvas.transform.Find("Image").Find("Image2").gameObject.GetComponent<Image>();
        healthBar.fillAmount = 1.0f;
    }
    public void SetData(int id,int health, int damage, float speed)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        this.damage = damage;
        this.speed = speed;
        this.id = id;
    }
    public void Destroy()
    {
        Destroy(healthBar);
        Destroy(canvas);
    }
}
