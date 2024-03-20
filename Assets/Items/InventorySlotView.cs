using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private InventorySlot _mySlot;
    [SerializeField] private Image _icon;

    private void OnItemChange(Item item)
    {
        if (item == null)
        {
            _name.text = string.Empty;
            _icon.sprite = null;
        }
        else
        {
            _name.text = item.Name;
            _icon.sprite = item.Icon;
        }
    }

    private void OnEnable()
    {
        _mySlot.ItemChanged += OnItemChange;
    }

    private void OnDisable()
    {
        _mySlot.ItemChanged -= OnItemChange;
    }

}
