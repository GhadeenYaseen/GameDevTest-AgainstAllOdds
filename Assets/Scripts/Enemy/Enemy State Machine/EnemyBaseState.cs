/*
    Base class for all enemy states
*/

public abstract class EnemyBaseState
{
    public abstract void StateEnter(EnemyController enemy);
    public abstract void StateUpdate(EnemyController enemy);
}
