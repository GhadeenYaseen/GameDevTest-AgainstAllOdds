using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void StateEnter(EnemyController enemy);
    public abstract void StateUpdate(EnemyController enemy);
}
