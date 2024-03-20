using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    [Inject] private NewControls _control;

    [SerializeField] private SearchItems _scan;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _weaponPoint;

    public event Action ItemActivated;
    private Transform _transform;
    private Dictionary<Guid, int> _slotMap = new Dictionary<Guid, int>();
    private Item _current;

    private void Awake()
    {
        _slotMap.Add(_control.Inventory.TakeItem1.id, 0);
        _slotMap.Add(_control.Inventory.TakeItem2.id, 1);
        _slotMap.Add(_control.Inventory.TakeItem3.id, 2);
        _slotMap.Add(_control.Inventory.TakeItem4.id, 3);
        _transform = transform;
    }

    private void OnEnable()
    {
        _control.Character.ActivateItem.performed += ActivateItem;
        _control.Character.PickupItem.performed += ctx => TakeScannerItem();
        _control.Character.DropItem.performed += ctx => DropItem();
        _control.Inventory.TakeItem1.performed += TakeInventoryItem;
        _control.Inventory.TakeItem2.performed += TakeInventoryItem;
        _control.Inventory.TakeItem3.performed += TakeInventoryItem;
        _control.Inventory.TakeItem4.performed += TakeInventoryItem;
        _control.Inventory.AddItem.performed += ctx => HideItem();

        if (_current == null)
            _scan.StartSearching();
    }

    private void OnDisable()
    {
        _control.Character.ActivateItem.performed -= ActivateItem;
        _control.Character.PickupItem.performed -= ctx => TakeScannerItem();
        _control.Character.DropItem.performed -= ctx => DropItem();
        _control.Inventory.TakeItem1.performed -= TakeInventoryItem;
        _control.Inventory.TakeItem2.performed -= TakeInventoryItem;
        _control.Inventory.TakeItem3.performed -= TakeInventoryItem;
        _control.Inventory.TakeItem4.performed -= TakeInventoryItem;
        _control.Inventory.AddItem.performed -= ctx => HideItem();

        if (_current == null)
            _scan.StopSearching();
    }

    private void ActivateItem(InputAction.CallbackContext context)
    {
        if (_current != null)
        {
            _current.Interact(_transform.forward, _transform.position);
            ItemActivated?.Invoke();
        }
    }

    private void TakeItem(Item item)
    {
        if (item == null)
            return;

        _current = item;
        _current.Take(_weaponPoint);
        _scan.StopSearching();
    }

    private void TakeScannerItem()
    {
        if (_current != null)
            return;

        TakeItem(_scan.CurrentItem);
    }

    private void TakeInventoryItem(InputAction.CallbackContext context)
    {
        Item itemInSlot = _inventory.GetItem(_slotMap[context.action.id]);

        if (itemInSlot == null)
            return;

        Item oldItem = _current;
        TakeItem(itemInSlot);
        oldItem?.Take(_container);
        _inventory.TryAddItem(oldItem);
    }

    private void DropItem()
    {
        if (_current == null)
            return;

        _scan.StartSearching();
        _current.Drop();
        _current = null;
    }

    private void HideItem()
    {
        if (_current == null)
            return;

        if (_inventory.TryAddItem(_current))
        {
            _current.Take(_container);
            _current = null;
            _scan.StartSearching();
        }
    }
}
