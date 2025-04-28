using DG.Tweening;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    float patrolCycleLength;

    public override void StateEnter(EnemyController enemy)
    {
        patrolCycleLength = enemy.patrolCycleLength;

        Debug.Log("on start patrol");
        // Get the target position
        Vector3 targetPosition = enemy.destination.position;

        // move towards X linearly
        enemy.gameObject.transform.DOMoveX(targetPosition.x, enemy.moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetId("enemy");

        // Move along y axis following the sine wave
        DOVirtual.Float(0, enemy.moveDuration, enemy.moveDuration, (t) =>
        {
            // Calculate the new y position using the sine function and apply the shift to our og y
            float newY = enemy.waveAmplitude * Mathf.Sin(t * enemy.waveFrequency * Mathf.PI * 2);
            enemy.gameObject.transform.position = new Vector3(enemy.gameObject.transform.position.x, enemy._startPosition.y + newY, enemy._startPosition.z);

        }).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).SetId("enemy");
    }
    
    public override void StateUpdate(EnemyController enemy)
    {
        
        //start timer to patrol for a while then start first attack
        patrolCycleLength -= Time.deltaTime; 
        Debug.Log("time left to walk: " + patrolCycleLength);
        
        if(patrolCycleLength < 0) 
        { 
            DOTween.Pause("enemy"); 
            enemy.SwitchState(enemy.flameThrowerState);
        } 
    }
}
