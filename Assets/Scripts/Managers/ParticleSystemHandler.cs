using UnityEngine;

/*
    handle fire beam particle collision, timer to prevent every single particle from damaging the player and killing them instantly
*/

public class ParticleSystemHandler : MonoBehaviour
{
    public float damageCooldown = 0.8f; // seconds
    private float lastDamageTime;

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            try
            {
                if (Time.time - lastDamageTime >= damageCooldown)
                {
                    lastDamageTime = Time.time;
                    other.GetComponent<Health>().TakeDamage(other);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
