using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IAmmo
{
    /*
        called on update, one target field, be shot towards the target
    */

    private void Update() 
    {
        
    }

    public void OnFire()
    {
        
    }

    /*
        on collision, enable explolsion particle, deal damage if tag is player, set active false graphics, turn off collider, turn off ui target
    */

    public void OnImpact()
    {
        
    }

    /*
        after explosion, set active false, reposition in spawn point
    */
    public void OnRelod()
    {
        
    }
}
