using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
    Class houses attack configs and state handling for enemy behavior
*/

public class EnemyController : MonoBehaviour
{
    /*in future, replace attack settings w scriptable objects for more زلّامي code*/

#region Attack Configs
    [Header("Patrol Movement Config")] 
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
    public GameObject bullsEye1, bullsEye2;
    public List<GameObject> _players;

#endregion

    EnemyBaseState currentState;

    public EnemyEagleFliesState eagleFliesState = new EnemyEagleFliesState();
    public EnemyFlameThrowerState flameThrowerState = new EnemyFlameThrowerState();
    public EnemyRocketLauncherState rocketLauncherState = new EnemyRocketLauncherState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();

    [HideInInspector] public List<EnemyBaseState> attackPattern = new List<EnemyBaseState>(); // choose state randomely for random patterns
    
    [HideInInspector] public Vector3 _ogPositionBullseye1, _ogPositionBullseye2;

    private void Awake()
    {
        _startPosition = startPos.position;

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
        attackPattern.Add(rocketLauncherState);
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

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Rocket"))
        {
            Debug.Log("grah has been hit by rocket");
            
            ScoreManager.scoreManagerInstance.UpdateScore();
            GetComponent<Health>().TakeDamage(this.gameObject);
        }
    }
}
