using DG.Tweening;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    float patrolCycleLength;
    Vector3 targetPosition;

    public override void StateEnter(EnemyController enemy)
    {
        patrolCycleLength = enemy.patrolCycleLength;

        Debug.Log("on start patrol");
        DOTween.Restart("enemy"); 
        
        // Get the target position
        targetPosition = enemy.destination.position;
        

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
        
        if(patrolCycleLength < 0) 
        { 
            DOTween.Pause("enemy");
            //targetPosition.x *=-1;
            enemy.SwitchState(enemy.eagleFliesState);
        } 
    }
    
}
