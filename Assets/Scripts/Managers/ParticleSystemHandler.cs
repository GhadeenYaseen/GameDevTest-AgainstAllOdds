using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            try
            {
                other.GetComponent<Health>().TakeDamage(other);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
