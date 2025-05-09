using DG.Tweening;
using EZCameraShake;
using UnityEngine;

/*
    Handle bullseye target on players, take off of screen, slamming
*/

public class EnemyEagleFliesState : EnemyBaseState
{
    Vector3 point;

    public override void StateEnter(EnemyController enemy)
    {
        Sequence EagleFlySeq = DOTween.Sequence();

        // take off
        EagleFlySeq.Append(enemy.transform.DOMove(enemy.offScreenPosition.position, enemy.takeOffDuration)).SetEase(enemy.takeOffEase);
        
        
        // select rand player as target
        EagleFlySeq.AppendCallback(()=>
        {
            point = enemy._players[Random.Range(0,GameManager.gameManagerInstance.playersCount)].transform.position;
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
            CameraShaker.Instance.ShakeOnce(enemy.magn, enemy.rough, enemy.fadeIn, enemy.fadeOut);
            SoundManager.PlaySound(SoundType.Slam);
        });

        EagleFlySeq.AppendCallback(()=>
        {
            enemy.shadowObject.SetActive(false);
        });

        EagleFlySeq.AppendInterval(1f);

        // return to patrol area
        EagleFlySeq.Append(enemy.transform.DOMove(enemy.startPos.position, enemy.returnToOGPosDuration).SetEase(enemy.takeOffEase));
        
        // back to patroling
        EagleFlySeq.AppendCallback(()=>
        {
            enemy.SwitchState(enemy.attackPattern[Random.Range(0, enemy.attackPattern.Count)]);
        });
    }

    public override void StateUpdate(EnemyController enemy)
    {
          
    }

}
