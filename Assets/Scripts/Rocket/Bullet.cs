using UnityEngine;

/*
    handle bullet shooting and reseting and impact for both explode-able and pickable rockets
*/

public class Bullet : MonoBehaviour, IAmmo
{
    [SerializeField] private bool isRocket = false;
    [SerializeField] private Vector3 direction = new Vector3(0,0,-1);
    private Vector3 _velocity;
    
    private bool _aquiredByPlayer=false;
    
    public GameObject bullet, target;
    public float speed, rocketSpeed;

    [HideInInspector] public Vector3 ogPosision;

    void Update()
    {
        //is pickable? move forward with no target / is explodeable? move towards player
        if(!isRocket)
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.transform.position, speed);
        else
            _velocity = direction * rocketSpeed;
    }

    private void FixedUpdate() 
    {
        if(isRocket)
        {
            Vector3 pos = transform.position;

            pos += _velocity * Time.fixedDeltaTime;

            transform.position = pos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        OnImpact(collision);
    }

    // what to do on any imact for both types
    public void OnImpact(Collision collision)
    {
        if(!isRocket)
        {
            if(collision.gameObject.CompareTag("Player") && collision.gameObject.activeSelf)
            {
                bullet.SetActive(false);

                try
                {
                    collision.gameObject.GetComponent<Health>().TakeDamage(collision.gameObject);
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }
            else if(collision.gameObject.CompareTag("Enemy") && collision.gameObject.activeSelf)
            {
                bullet.SetActive(false);
                
                try
                {
                    collision.gameObject.GetComponent<Health>().TakeDamage(collision.gameObject);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        else
        {
            _aquiredByPlayer = true;
        }
    }

    // reset
    public void OnRelod()
    {
        if(ogPosision != null)
            transform.position = ogPosision;

        _aquiredByPlayer = false;
    }

    // disabling for pickable is handled in inventory class
    private void OnDisable() 
    {
        if(!isRocket)
            OnRelod();
    }
}
