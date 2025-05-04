using UnityEngine;

/*
    Arrows and numpad player using the old input system with unity's character controller
*/

public class PlayerTwoController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpPower = 6f, gravity;

    private float _directionY;

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        if(characterController.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                _directionY = jumpPower;
            }
        }

        _directionY-= gravity * Time.deltaTime;
        direction.y = _directionY;
        
        if(direction.magnitude >= 0.1f)
        {
            characterController.Move(direction * speed * Time.deltaTime);
        }
    }

     void OnTriggerEnter(Collider other) 
     {
        if(other.gameObject.CompareTag("Enemy"))
        {
            try
            {
                gameObject.GetComponent<Health>().TakeDamage(gameObject);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
