using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*in future, replace attack settings w scriptable objects for more زلّامي code*/

    [Header("Patrol Movement Configuration")] /*pause patrol and resume by using -> DoTween.Pause("enemy"); .Play("enemy"); */
    public Transform destination;
    public float moveDuration = 5f;
    public float waveAmplitude = 2f;
    public float waveFrequency = 2f;
    public float patrolCycleLength = 6f;
    [HideInInspector] public Vector3 _startPosition;

    EnemyBaseState currentState;

    public EnemyEagleFliesState eagleFliesState = new EnemyEagleFliesState();
    public EnemyFlameThrowerState flameThrowerState = new EnemyFlameThrowerState();
    public EnemyRocketLauncherState rocketLauncherState = new EnemyRocketLauncherState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyHurtState hurtState = new EnemyHurtState();

    private List<EnemyBaseState> attackPattern = new List<EnemyBaseState>();
    private int _currentStateIndex = 0;

    /*
        attack patterns as follow:
        - a list of enemy states, with patrol in between each state, as follow:
            list[patrol, flame, patrol, rocket, patrol, eagle]
        -on each state complete, we increase the current index, then switch state to the value of the new index
    
    */

    private void Awake()
    {
        _startPosition = transform.position;
        InitAttackPattern();
    }

    private void Start() 
    {
        currentState = patrolState;
        currentState.StateEnter(this);
    }

    private void InitAttackPattern()
    {
        attackPattern.Add(patrolState);
        attackPattern.Add(flameThrowerState);

        attackPattern.Add(patrolState);
        attackPattern.Add(rocketLauncherState);

        attackPattern.Add(patrolState);
        attackPattern.Add(eagleFliesState);
    }

    public void SwitchState(EnemyBaseState baseState)
    {
        currentState = baseState;
        baseState.StateEnter(this);
    }

    void Update()
    {
        currentState.StateUpdate(this);
    }
}
