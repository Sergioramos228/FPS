using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _slots = new();

    public bool TryAddItem(Item item)
    {
        if (item == null)
            return false;

        foreach (InventorySlot slot in _slots)
        {
            if (slot.TryAdd(item))
                return true;
        }

        return false;
    }

    public Item GetItem(int slotNumber)
    {
        if (slotNumber < 0 || _slots.Count <= slotNumber)
            return null;

        return _slots[slotNumber].Take();
    }
}
