using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*[Header("Patrol Movement Settings")]
    [SerializeField] private float patrolCycleLength;
    [SerializeField] private float patrolRange;
    [SerializeField] private Ease patrolEase;

    private void Start() 
    {
        Partrol();
    }

    public void Partrol()
    {
        //yoyo, and updown
        gameObject.transform.DOMoveX(10 * patrolRange, patrolCycleLength).SetEase(patrolEase).SetLoops(-1, LoopType.Yoyo);
    }*/

     [SerializeField]
    [Tooltip("Spot to move towards")]
    private Transform destination;

    [SerializeField]
    [Tooltip("Duration of the movement towards the target")]
    public float moveDuration = 5f;

    [SerializeField]
    [Tooltip("Amplitude of the sine wave")]
    public float waveAmplitude = 2f;

    [SerializeField]
    [Tooltip("Frequency of the sine wave")]
    public float waveFrequency = 2f;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {
        // Get the target position
        Vector3 targetPosition = destination.position;

        // move towards X linearly
        transform.DOMoveX(targetPosition.x, moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        // Move along y axis following the sine wave
        DOVirtual.Float(0, moveDuration, moveDuration, (t) =>
        {
            // Calculate the new y position using the sine function and apply the shift to our og y
            float newY = waveAmplitude * Mathf.Sin(t * waveFrequency * Mathf.PI * 2);
            transform.position = new Vector3(transform.position.x, startPosition.y + newY, startPosition.z);
        }).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}

public enum EnemyState
{
    Patrol, //sin wave type movement
    Shoot, //either fire laser, or rockets, enemy must stand still to shoot then go back patroling
    Hurt, //enemy stand still for a few seconds, cant shoot in this state, then back patroling
    EagleFlies, // stop patroling, fly out of frame, project shadow on one of the players' position, then fly straight down to it, then hover back to patrol line
    Dead // play death animation, signal to game manager to stop game and display win state stuff
}
