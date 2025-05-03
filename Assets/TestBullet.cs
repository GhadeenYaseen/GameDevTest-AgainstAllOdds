using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public GameObject bullet, target;
    public float speed;

    public Vector3 ogPos;

    void Start()
    {
        //ogPos = transform;
        if(ogPos != null)
        Debug.Log(ogPos);

    }

    private void OnEnable() 
    {
        //target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.transform.position, speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() 
    {
        if(ogPos != null)
        transform.position = ogPos;
    }
}
