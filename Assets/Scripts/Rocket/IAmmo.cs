using UnityEngine;

/*
    interface for projectiles
*/

public interface IAmmo 
{
    void OnRelod();
    
    void OnImpact(Collision collision);
}
