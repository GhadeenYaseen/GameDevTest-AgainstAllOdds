using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
        bullet.GetComponent<Bullet>().ogPos = transform.position;
        bullet.GetComponent<Bullet>().target = target;
        bullet.SetActive(false);
    }

    private void Shoot()
    {
        bulletPrefab.SetActive(true);
    }

    private void Relod()
    {
        bulletPrefab.SetActive(false);
    }
}
