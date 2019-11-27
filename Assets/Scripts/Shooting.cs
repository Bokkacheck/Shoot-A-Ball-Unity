using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Texture2D crosshair;
    private RaycastHit hitBefore;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //      Shoot();
        //}
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.Rotate(90, 0, 0);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * 500, ForceMode.Impulse);
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
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x - 120.0f / 2, screenPosition.y - 120 / 2.0f, 120, 120), crosshair);
    }
}
