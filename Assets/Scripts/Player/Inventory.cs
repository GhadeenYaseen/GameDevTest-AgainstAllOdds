using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform holdPosition;
    private bool _isSlotFull = false;
    

    public void ID()
    {
        Debug.Log("tis inventory belongs to his majesty: " + gameObject.name);
    }

    public bool SlotFull()
    {
        return _isSlotFull;
    }

    public void SetSlotState(bool state)
    {
        _isSlotFull = state;
    }

    public Transform HoldPosition()
    {
        return holdPosition;
    }
}
