using UnityEngine;

public class EnemyFlameThrowerState : EnemyBaseState
{
    public override void StateEnter(EnemyController enemy)
    {
        Debug.Log("begin fire beam cuh");
        enemy.flameBeamParticle.Play();
    }

    public override void StateUpdate(EnemyController enemy)
    {
        if(enemy.flameBeamParticle.isStopped)
        {
            Debug.Log("fire done");
            enemy.SwitchState(enemy.patrolState);
        }
    }
}
