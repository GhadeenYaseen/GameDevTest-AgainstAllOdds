using System;
using UnityEngine;
using DG.Tweening;

/*
    rocket specifications:
        -thrown by enemy
        -picked up automatically by player
        -thrown back by player when button is clicked

    pick up logic:
        -on trigger enter (player nears bullet), auto pick up (game design decision to ease defeating the boss)
        -player will have a child empty transform, where the rocket will be held, rocket will auto be positioned there
        -if rocket is already picked, dont pick up any more (check bool on player)

    non-explode-able

    ONLY ONE BUTTON TO FIRE FOR BOTH PLAYERS, to increase fun and craziness ;D
*/

[RequireComponent(typeof(CapsuleCollider))]
public class Rocket : MonoBehaviour, IPickable
{
    [SerializeField] private float throwPower = 3f, lifeCycleDuration;

    private CapsuleCollider _rocketCollider;
    private Inventory _playerInventory;

    private Transform _holdPosition;
    private GameObject _currentPlayer;

    private Transform _enemyLaunchPoint;

    private void Awake() 
    {
        _rocketCollider = GetComponent<CapsuleCollider>();
        _enemyLaunchPoint = gameObject.transform.parent.transform;
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1") /*&& _playerInventory.SlotFull() == true*/ && _currentPlayer !=null)
        {
            Throw();
        }
    }

    // if rocket is encountered and inventory is empty, equip
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            VerifyInventory(other.gameObject);
            Debug.Log("picked rocket");

            if(_playerInventory.SlotFull() == false)
            {
                _playerInventory.SetSlotState(true);
                PickUp();
            }
            else
            {
                Debug.Log("u already have rocket, throw it first");
            }
        }
        else if(other.gameObject.CompareTag("Enemy") && other.gameObject.activeSelf)
        {
            try
            {
                Debug.Log("enemy has been hit by rocket");
                ScoreManager.scoreManagerInstance.UpdateScore();
                other.gameObject.GetComponent<Health>().TakeDamage(other.gameObject);
            }
            catch (System.Exception)
            {
                    
                throw;
            }
        }    
    }
    
    /*(Collider other)
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
                Debug.Log("u already have rocket, throw it first");
            }
        }
        else if(other.gameObject.CompareTag("Enemy") && other.gameObject.activeSelf)
        {
            try
            {
                Debug.Log("enemy has been hit by rocket");
                ScoreManager.scoreManagerInstance.UpdateScore();
                other.gameObject.GetComponent<Health>().TakeDamage(other.gameObject);
            }
            catch (System.Exception)
            {
                    
                throw;
            }
        }
    }*/

    public void VerifyInventory(GameObject gameObject)
    {
        try
        {
            _playerInventory = gameObject.GetComponent<Inventory>();
            _currentPlayer= gameObject;
        }
        catch (NullReferenceException)
        {
            Debug.Log("check ur inventory :/ \n like do u even have one? here i give u one");
            _playerInventory = gameObject.AddComponent<Inventory>();
        }
        catch(Exception)
        {
            Debug.Log("gg");
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

        _rocketCollider.enabled = false;

        ScoreManager.scoreManagerInstance.UpdateScore();
    }

    public void Throw()
    {
        gameObject.transform.parent = null;

        Vector3 throwDirection =  _currentPlayer.transform.forward;
        gameObject.transform.DOMove(throwDirection * throwPower, lifeCycleDuration).SetEase(Ease.OutExpo).OnComplete(
            ()=> {
                    gameObject.transform.parent = null;
                    _playerInventory.SetSlotState(false);
                    _rocketCollider.enabled = true;
                }
        );
    }

    // reset
    public void OnRelod()
    {
        gameObject.transform.parent = _enemyLaunchPoint;
        gameObject.transform.localPosition = _enemyLaunchPoint.localPosition;
        
        if(_playerInventory != null)
            _playerInventory.SetSlotState(false);
        
        _rocketCollider.enabled = true;
    }
}
