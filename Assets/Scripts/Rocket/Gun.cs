using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject target;

    List<GameObject> bullets = new List<GameObject>();
    
    void Start()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
        bullet.GetComponent<Bullet>().ogPos = transform.position;
        bullet.GetComponent<Bullet>().target = target;

        bullets.Add(bullet);

        bullet.SetActive(true);
    }

    private void OnEnable() 
    {
        Shoot();
    }

    private void Shoot()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(true);
        }
        
    }
}
