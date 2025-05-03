using System;
using UnityEngine;
using DG.Tweening;

/*
    rocket specifications:
        -thrown by enemy
        -picked up automatically by player
        -thrown back by player when button is clicked

    pick up logic:
        -on trigger enter (player nears bullet), auto pick up
        -player will have a child empty transform, where the rocket will be held, rocket will auto be positioned there
        -if rocket is already picked, dont pick up any more (check bool on player)
        -if rocket is not picked for 3 seconds, return to pool/deactivate/dissapear

    non-explode-able
*/

[RequireComponent(typeof(CapsuleCollider))]
public class Rocket : MonoBehaviour, IPickable
{
    [SerializeField] private float throwPower = 3f, lifeCycleDuration;

    private CapsuleCollider _collider;
    private Inventory _playerInventory;

    private Transform _holdPosition;
    private GameObject _currentPlayer;

    private Transform _enemyLaunchPoint;

    private void Awake() 
    {
        _collider = GetComponent<CapsuleCollider>();
        _enemyLaunchPoint = gameObject.transform.parent.transform;
        Debug.Log("bullet awake parent:" + _enemyLaunchPoint.gameObject.name);
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1") && _playerInventory.SlotFull() == true && _currentPlayer !=null)
        {
            Throw();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            VerifyInventory(other.gameObject);

            if(_playerInventory.SlotFull() == false)
            {
                _playerInventory.SetSlotState(true);
                PickUp();
            }
            else
            {
                Debug.Log("u already have rocket man, throw it first");
            }
        }
    }

    public void VerifyInventory(GameObject gameObject)
    {
        try
        {
            _playerInventory = gameObject.GetComponent<Inventory>();
            _playerInventory.ID();
            _currentPlayer= gameObject;
        }
        catch (NullReferenceException)
        {
            Debug.Log("my guy check ur inventory :/ \n like do u even have one bruh? here i gib u one ;3");
            _playerInventory = gameObject.AddComponent<Inventory>();
        }
        catch(Exception)
        {
            Debug.Log("tsk dude gg ig");
            throw;
        }
    }

    public void PickUp()
    {
        //get transform of holding point from player, assign that to rocket
        _holdPosition = _playerInventory.HoldPosition();
        Debug.Log("hold pos:" + _holdPosition);

        gameObject.transform.position = _holdPosition.position;
        gameObject.transform.parent = _holdPosition;

        _collider.enabled = false;
    }

    public void Throw()
    {
        gameObject.transform.parent = null;

        Vector3 throwDirection =  _currentPlayer.transform.forward;
        gameObject.transform.DOMove(throwDirection * throwPower, lifeCycleDuration).SetEase(Ease.OutExpo).OnComplete(

            ()=> {
                    Debug.Log("reuse, ecece");
                    gameObject.transform.parent = null;
                    _playerInventory.SetSlotState(false);
                    _collider.enabled = true;
                }
        );
    }

    public void OnRelod()
    {
        gameObject.transform.parent = _enemyLaunchPoint;
        gameObject.transform.localPosition = _enemyLaunchPoint.localPosition;
        
        if(_playerInventory != null)
            _playerInventory.SetSlotState(false);
        
        _collider.enabled = true;
    }
}
