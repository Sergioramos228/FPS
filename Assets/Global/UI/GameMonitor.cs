using DG.Tweening;
using UnityEngine;

public class GameMonitor : MonoBehaviour
{
    private const float HideValue = 0;
    private const float ShowValue = 1;
    private const float Duration = 3;

    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private bool _isCursorIn;

    public void Show()
    {
        _canvas.alpha = HideValue;
        _canvas.gameObject.SetActive(true);
        _canvas.DOFade(ShowValue, Duration);

        if (_isCursorIn)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void Hide()
    {
        _canvas.alpha = ShowValue;
        _canvas.DOFade(HideValue, Duration).OnComplete(OnHide);
    }

    private void OnHide()
    {
        _canvas.gameObject.SetActive(false);
    }
}
