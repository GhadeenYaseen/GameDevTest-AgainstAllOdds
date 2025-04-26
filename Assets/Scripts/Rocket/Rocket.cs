using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    rocket specifications:
        -thrown by enemy
        -picked up automatically by player
        -thrown back by player when right mouse button is clicked

    pick up logic:
        -on trigger enter (player nears bullet), auto pick up
        -player will have a child empty transform, where the rocket will be held, rocket will auto be positioned there
        -if rocket is already picked, dont pick up any more (check bool on player)
        -if rocket is not picked for 3 seconds, return to pool/deactivate/dissapear

    non-explode-able
*/

[RequireComponent(typeof(CapsuleCollider)), RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour, IPickable
{
    [SerializeField] private float throwPower = 3f;

    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    private Inventory _playerInventory;

    private Transform _holdPosition;
    private GameObject _currentPlayer;

    private void Awake() 
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1") && _playerInventory.SlotFull() == true &&_currentPlayer !=null)
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
        catch(System.Exception)
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
        _rigidbody.useGravity =false;
        _rigidbody.freezeRotation = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Throw()
    {
        gameObject.transform.parent = null;

        //_collider.enabled = true;
        _rigidbody.useGravity =true;
        _rigidbody.freezeRotation = false;
        _rigidbody.constraints = RigidbodyConstraints.None;

        Vector3 throwDirection =  _currentPlayer.transform.forward;
        _rigidbody.AddForce(throwDirection * throwPower);

        //_playerInventory.SetSlotState(false);
    }
}
