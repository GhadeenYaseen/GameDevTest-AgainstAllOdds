using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmo 
{
    void OnRelod();
    void OnFire();
    void OnImpact(Collision collision);
}
