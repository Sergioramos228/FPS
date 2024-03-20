using UnityEngine;
using Zenject;

public class CharacterRotation : MonoBehaviour
{
    private const float MinMagnitude = 0.01f;

    [SerializeField] private float _sensitive;
    [Inject] private NewControls _control;

    private Vector2 _input;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _input = _control.Character.Rotate.ReadValue<Vector2>().normalized;

        if (_input.magnitude > MinMagnitude)
            _transform.Rotate(new Vector3(0, _input.x, 0) * _sensitive * Time.deltaTime);
    }
}
