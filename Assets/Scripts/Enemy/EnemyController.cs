using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*in future, replace attack settings w scriptable objects for more زلّامي code*/

    [Header("Patrol Movement Config")] /*pause patrol and resume by using -> DoTween.Pause("enemy"); .Play("enemy"); */
    public Transform destination;
    public Transform startPos;
    public float moveDuration = 5f;
    public float waveAmplitude = 2f;
    public float waveFrequency = 2f;
    public float patrolCycleLength = 6f;
    [HideInInspector] public Vector3 _startPosition;

    [Header("Flame Thrower Attack Config")] /*warm up effect could be -> inc light power from heart of enemy*/
    public ParticleSystem flameBeamParticle;

    [Header("Eagle Flies Attack Config")]
    public Transform offScreenPosition;
    public float takeOffDuration;
    public float returnToOGPosDuration;
    public Ease takeOffEase;
    public float slamPointsSphereRadius;
    public GameObject shadowObject;

    [Header("CameraShake Effect Config")]
    public float magn;
    public float rough; 
    public float fadeIn;
    public float fadeOut;

    EnemyBaseState currentState;

    public EnemyEagleFliesState eagleFliesState = new EnemyEagleFliesState();
    public EnemyFlameThrowerState flameThrowerState = new EnemyFlameThrowerState();
    public EnemyRocketLauncherState rocketLauncherState = new EnemyRocketLauncherState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyHurtState hurtState = new EnemyHurtState();

    [HideInInspector] public GameObject[] _players;
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
        _startPosition = startPos.position;
        _players = GameObject.FindGameObjectsWithTag("Player");

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
