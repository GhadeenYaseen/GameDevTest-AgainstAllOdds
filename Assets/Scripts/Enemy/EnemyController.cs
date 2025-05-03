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

    [Header("Rocket Launcher Attack Config")]

    public List<Transform> rocketLaunchPositions = new List<Transform>();
    //public Transform launchPos1; // boom rocket1
    //public Transform launchPos2; //pickable rocket
    //public Transform launchPos3; // boom rocket2
    public GameObject bullsEye1, bullsEye2;


    EnemyBaseState currentState;

    public EnemyEagleFliesState eagleFliesState = new EnemyEagleFliesState();
    public EnemyFlameThrowerState flameThrowerState = new EnemyFlameThrowerState();
    public EnemyRocketLauncherState rocketLauncherState = new EnemyRocketLauncherState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyHurtState hurtState = new EnemyHurtState();

    [HideInInspector] public GameObject[] _players;
    private List<EnemyBaseState> attackPattern = new List<EnemyBaseState>();

    [HideInInspector] public Vector3 _ogPositionBullseye1, _ogPositionBullseye2;

    private void Awake()
    {
        _startPosition = startPos.position;
        _players = GameObject.FindGameObjectsWithTag("Player");

        _ogPositionBullseye1 = bullsEye1.transform.localPosition;
        _ogPositionBullseye2 = bullsEye2.transform.localPosition;

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
