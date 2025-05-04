using UnityEngine;

/*
    manage player picking throwable rocket, currently allows one slot to increase difficulty and playtime.
    might consider adding a max slots number and allow rocket stacking.
*/

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform holdPosition; //slot position
    private bool _isSlotFull = false;

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
