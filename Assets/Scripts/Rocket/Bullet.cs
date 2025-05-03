using UnityEngine;

public class Bullet : MonoBehaviour, IAmmo
{
    [SerializeField] private bool isRocket = false;
    
    [SerializeField] private Vector3 direction = new Vector3(0,0,-1);

    private bool _aquiredByPlayer=false;
    private Vector3 _velocity;
    
    public GameObject bullet, target;
    public float speed, rocketSpeed;

    [HideInInspector] public Vector3 ogPos;

    void Update()
    {
        //if rocket and is thrown dont move towards
        if(!_aquiredByPlayer && !isRocket)
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.transform.position, speed);
    
        if(isRocket)
        {
            _velocity = direction * rocketSpeed;
        }
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
    
    //play smoke particle? sound?
    public void OnFire()
    {

    }

    public void OnImpact(Collision collision)
    {
        if(!isRocket)
        {
            if(collision.gameObject.CompareTag("Player") )
            {
                bullet.SetActive(false);
                // play explosion particle
                // deal damage
            }
            else
            {
                bullet.SetActive(false);
                // play explosion particle
            }
        }
        else
        {
            _aquiredByPlayer = true;
        }
    }

    public void OnRelod()
    {
        if(ogPos != null)
            transform.position = ogPos;

        _aquiredByPlayer = false;
    }

    //if is rocket and collision player inventory slot is full, dont disable
    private void OnDisable() 
    {
        if(!isRocket)
            OnRelod();
    }
}
