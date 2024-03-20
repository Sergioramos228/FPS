using DG.Tweening;
using TMPro;
using UnityEngine;

public class FoundItemView : MonoBehaviour
{
    private const float ShowValue = 1f;
    private const float HideValue = 0f;
    private const float Duration = 0.5f;

    [SerializeField] private TMP_Text _label;
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private SearchItems _searcher;

    private void OnEnable()
    {
        _searcher.FoundItem += OnItemsSearched;
    }

    private void OnDisable()
    {
        _searcher.FoundItem -= OnItemsSearched;
    }

    private void OnItemsSearched(Item item)
    {
        if (item == null)
        {
            _label.text = string.Empty;
            _canvas.DOFade(HideValue, Duration);
        }
        else
        {
            _label.text = $"You found {item.Name}";
            _canvas.DOFade(ShowValue, Duration);
        }
    }
}
