using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour, IAmmo
{
    public GameObject bullet, target;
    public float speed;

    public Vector3 ogPos;

    public void OnFire()
    {

    }

    void Update()
    {
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.transform.position, speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnImpact();
    }

    public void OnImpact()
    {
        // play explosion particle
        // deal damage

        bullet.SetActive(false);
    }

    public void OnRelod()
    {

    }

    private void OnDisable() 
    {
        if(ogPos != null)
        transform.position = ogPos;
    }
}
