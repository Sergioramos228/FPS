using System;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item _current;

    public event Action<Item> ItemChanged;

    public Item Take()
    {
        if (_current == null)
            return null;

        Item item = _current;
        _current = null;
        ItemChanged?.Invoke(_current);
        return item;
    }

    public bool TryAdd(Item item)
    {
        if (_current == null)
        {
            _current = item;
            ItemChanged?.Invoke(_current);
            return true;
        }
        else
        {
            return false;
        }
    }
}
