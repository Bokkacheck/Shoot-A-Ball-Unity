using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Texture2D crosshair;
    [SerializeField]
    private float crosshairSize = 120f;
    public AudioSource shootSound;
    private RaycastHit hitBefore;
    [SerializeField]
    private Transform gunShootPoint;

    public static Shooting singleton;
    public bool drawCrosshair = true;

    void Start()
    {
        singleton = this;
        shootSound.volume = MusicManager.singleton.gameVolume;
    }
  
    public void Shoot()
    {
        shootSound.Play();
        GameObject bullet = Instantiate(bulletPrefab, gunShootPoint.position, gunShootPoint.rotation);
        bullet.transform.Rotate(90, 0, 0);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * 400, ForceMode.Impulse);
        Destroy(bullet, 2f);
    }
    void OnGUI()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.transform.position, transform.forward, out hit, 1000))
        {
            if (hit.collider.tag == "Bullet")
            {
                hit = hitBefore;
            }
        }
        else
        {
            hit.point = transform.forward * 1000;
        }
        hitBefore = hit;
        gunShootPoint.LookAt(hit.point);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
        screenPosition.y = Screen.height - screenPosition.y;
        if (drawCrosshair)
        {
            GUI.DrawTexture(new Rect(screenPosition.x - crosshairSize / 2, screenPosition.y - crosshairSize / 2f, crosshairSize, crosshairSize), crosshair);
        }
    }
}
