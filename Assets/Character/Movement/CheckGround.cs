using System;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 0.49f;
    [SerializeField] private float _checkDistance = 0.69f;

    private bool _isGrounded;
    private Transform _transform;
    private RaycastHit _hit;

    public event Action<bool> StateChanged;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        TryChangedCurrentState(Physics.SphereCast(_transform.position, _checkRadius, Vector3.down, out _hit, _checkDistance));
    }

    private void TryChangedCurrentState(bool newState)
    {
        if (_isGrounded != newState)
        {
            _isGrounded = newState;
            StateChanged?.Invoke(_isGrounded);
        }
    }
}
