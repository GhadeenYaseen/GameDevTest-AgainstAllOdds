using System.Collections.Generic;
using DG.Tweening;
using EZCameraShake;
using UnityEngine;

public class EnemyEagleFliesState : EnemyBaseState
{
    /*
        use sequence
        -save/hold current pos
        -domovey to target transform field
        -choose random point from sphere
        -boss has shadow obj child, change pos to selected slam point, then activate
        -domove to slam point
        -camera shake
        -deactivate shadow
        -domove to original pos
        -back to patroling
    */

    Vector3 point;

    public override void StateEnter(EnemyController enemy)
    {
        Sequence EagleFlySeq = DOTween.Sequence();

        // take off
        EagleFlySeq.Append(enemy.transform.DOMove(enemy.offScreenPosition.position, enemy.takeOffDuration)).SetEase(enemy.takeOffEase);
        
        
        // select rand point in range to slam to
        EagleFlySeq.AppendCallback(()=>
        {
            point = enemy._players[Random.Range(0,2)].transform.position; //choose one of the players as target
            enemy.shadowObject.transform.position = point;
            enemy.shadowObject.SetActive(true);
        });

        EagleFlySeq.AppendInterval(1f);

        // slam
        EagleFlySeq.AppendCallback(()=>
        {
            enemy.transform.DOMove(point, enemy.takeOffDuration).SetEase(enemy.takeOffEase);
        });
        
        // slam effect
        EagleFlySeq.AppendCallback(()=>
        {
            //enemy.shadowObject.SetActive(false);
            CameraShaker.Instance.ShakeOnce(enemy.magn, enemy.rough, enemy.fadeIn, enemy.fadeOut);
        });

        EagleFlySeq.AppendInterval(1f);

        // return to patrol area
        EagleFlySeq.Append(enemy.transform.DOMove(enemy.startPos.position, enemy.returnToOGPosDuration).SetEase(enemy.takeOffEase));
        
        // back to patroling
        EagleFlySeq.AppendCallback(()=>
        {
            Debug.Log("eagle done");
            enemy.SwitchState(enemy.patrolState);
        });
    }

    public override void StateUpdate(EnemyController enemy)
    {
          
    }

}
