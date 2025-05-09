using UnityEngine;

/*
    Handle fire beam attack
*/

public class EnemyFlameThrowerState : EnemyBaseState
{
    public override void StateEnter(EnemyController enemy)
    {
        if(! GameManager.gameManagerInstance.isGameOver)
        {
            enemy.flameBeamParticle.Play();
            SoundManager.PlaySound(SoundType.Fire);
        }
        
    }

    public override void StateUpdate(EnemyController enemy)
    {
        if(enemy.flameBeamParticle.isStopped)
        {
            enemy.SwitchState(enemy.attackPattern[Random.Range(0, enemy.attackPattern.Count)]);
        }

        if(GameManager.gameManagerInstance.isGameOver)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
