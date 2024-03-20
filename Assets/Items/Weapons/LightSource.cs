using System.Collections;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _duration;

    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(_duration);
    }

    private void OnEnable()
    {
        _light.enabled = false;
    }

    private void OnDisable()
    {
        _light.enabled = false;
    }

    public void Play()
    {
        StartCoroutine(Lightning());
    }

    private IEnumerator Lightning()
    {
        _light.enabled = true;
        yield return _delay;
        _light.enabled = false;
    }
}
