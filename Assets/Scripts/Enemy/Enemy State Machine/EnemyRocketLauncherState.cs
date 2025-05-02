using DG.Tweening;
using UnityEngine;

public class EnemyRocketLauncherState : EnemyBaseState
{
    /*
        onstate enter:
        -enable two bulls eye images
        -append interval 2 seconds
        -remove from parent
        -set active true three rockets
    */

    public override void StateEnter(EnemyController enemy)
    {
        Sequence launcherSeq = DOTween.Sequence();

        launcherSeq.AppendCallback(()=>
        {
            enemy.bullsEye1.SetActive(true);
            enemy.bullsEye2.SetActive(true);
        });
        
        launcherSeq.AppendInterval(2f);

        launcherSeq.AppendCallback(()=>
        {
            enemy.bullsEye1.transform.parent = null;
            enemy.bullsEye2.transform.parent = null;
        });

        launcherSeq.AppendInterval(1f);

        //shoot here

        // reset
        launcherSeq.AppendCallback(()=>
        {
            enemy.bullsEye1.transform.parent = enemy._players[0].transform;
            enemy.bullsEye2.transform.parent = enemy._players[1].transform;

            enemy.bullsEye1.transform.localPosition = enemy._ogPositionBullseye1;
            Debug.Log("bulls eye1 og pos: " + enemy.bullsEye1.transform.position);
            enemy.bullsEye2.transform.localPosition = enemy._ogPositionBullseye2;

            enemy.bullsEye1.SetActive(false);
            enemy.bullsEye2.SetActive(false);

            
        });

        launcherSeq.AppendCallback(()=>
        {
            enemy.SwitchState(enemy.patrolState);
        });

    }

    public override void StateUpdate(EnemyController enemy)
    {
        
    }
}
