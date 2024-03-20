using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private BaseHealth _health;
    [SerializeField] private Canvas _billboard;

    private Transform _camera;
    private Transform _canvas;

    private Coroutine _rotateCanvas;
    private bool _isRotatable;

    private void Awake()
    {
        if (_billboard != null && _billboard.renderMode == RenderMode.WorldSpace)
        {
            _camera = Camera.main.transform;
            _canvas = _billboard.transform;
            _isRotatable = true;
        }
    }

    private void OnEnable()
    {
        UpdateView();
        _health.HealthChanged += UpdateView;

        if (_isRotatable)
            _rotateCanvas = StartCoroutine(Rotate());
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateView;

        if (_rotateCanvas != null)
        {
            StopCoroutine(_rotateCanvas);
            _rotateCanvas = null;
        }
    }

    private void UpdateView()
    {
        _healthSlider.value = _health.Current / _health.Max;
        _label.text = $"{_health.Current} / {_health.Max}";
    }

    private IEnumerator Rotate()
    {
        yield return null;
        WaitForSeconds delay = new WaitForSeconds(0.01f);

        while (true)
        {
            _canvas.rotation = _camera.rotation;
            yield return delay;
        }
    }
}