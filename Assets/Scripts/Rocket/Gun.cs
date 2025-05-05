using System.Collections.Generic;
using UnityEngine;

/*
    handle launching bullets.

    no object pooling was used as it didn't seem necessary, but it follows a similar structure:
        -instantiate once at start
        -reuse (enable disable) objects when needed
*/

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject target;

    List<GameObject> bullets = new List<GameObject>(); //to get children
    
    void Start()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
        bullet.GetComponent<Bullet>().ogPosision = transform.position;
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
            bullet.GetComponent<Bullet>().ogPosision = transform.position;
            bullet.SetActive(true);
        }
    }

    private void OnDisable() 
    {
        bullets[0].GetComponent<Bullet>().ogPosision = transform.position;
    }
}
