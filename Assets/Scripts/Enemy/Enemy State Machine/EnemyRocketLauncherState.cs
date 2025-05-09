using DG.Tweening;
using UnityEngine;

/*
    handle bullseyes and guns, first enable bullseys, leave them for a while, lock target, activate guns, reset everything
*/

public class EnemyRocketLauncherState : EnemyBaseState
{
    public override void StateEnter(EnemyController enemy)
    {
        Sequence launcherSeq = DOTween.Sequence();

        // reset, then display bullseyes within players
        launcherSeq.AppendCallback(()=>
        {
            enemy.bullsEye1.transform.parent = enemy._players[0].transform;
            enemy.bullsEye2.transform.parent = enemy._players[1].transform;

            enemy.bullsEye1.transform.localPosition = enemy._ogPositionBullseye1;
            enemy.bullsEye2.transform.localPosition = enemy._ogPositionBullseye2;
            
            if(enemy._players[0].activeSelf)
            {
                enemy.bullsEye1.transform.parent = enemy._players[0].transform;
                enemy.bullsEye1.transform.localPosition = enemy._ogPositionBullseye1;
                enemy.bullsEye1.SetActive(true);
                
            }
            
            if(enemy._players[1].activeSelf)
            {
                enemy.bullsEye2.transform.parent = enemy._players[1].transform;
                enemy.bullsEye2.transform.localPosition = enemy._ogPositionBullseye2;
                enemy.bullsEye2.SetActive(true);
            }

            //enemy.bullsEye1.SetActive(true);
            //enemy.bullsEye2.SetActive(true);
        });
        
        // follow for a while
        launcherSeq.AppendInterval(2f);

        // lock target
        launcherSeq.AppendCallback(()=>
        {
            if(enemy._players[0].activeSelf) enemy.bullsEye1.transform.parent = null;
            if(enemy._players[1].activeSelf) enemy.bullsEye2.transform.parent = null;
        });

        launcherSeq.AppendInterval(1f);

        //shoot here
        launcherSeq.AppendCallback(()=>
        {
            for(int i =0 ; i< GameManager.gameManagerInstance.playersCount ; i++) 
            {
                enemy.rocketLaunchPositions[i].gameObject.SetActive(true);
            }
            
            enemy.rocketLaunchPositions[2].gameObject.SetActive(true);
            /*foreach (Transform gun in enemy.rocketLaunchPositions)
            {
                gun.gameObject.SetActive(true);
            }*/
            
            if(enemy._players[0].activeSelf) enemy.bullsEye1.SetActive(false);
            if(enemy._players[1].activeSelf) enemy.bullsEye2.SetActive(false);
        });

        launcherSeq.AppendInterval(6f);

        // reset bullseyes and guns
        launcherSeq.AppendCallback(()=>
        {
            foreach (Transform gun in enemy.rocketLaunchPositions)
            {
                gun.gameObject.SetActive(false);
            }
        });

        launcherSeq.AppendCallback(()=>
        {
            enemy.SwitchState(enemy.attackPattern[Random.Range(0, enemy.attackPattern.Count)]);
        });
    }

    public override void StateUpdate(EnemyController enemy)
    {
        
    }
}
