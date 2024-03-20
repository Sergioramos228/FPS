using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetTracker : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private float _trackingRadius;

    private Transform _transform;
    private Queue<Func<bool>> _nextCalculator;
    private Func<bool> _currentCalculator;
    private bool _isEntered;

    public event Action<bool> TargetAttainable;

    private void Awake()
    {
        _isEntered = true;
        _transform = transform;
        _currentCalculator = CalculateEntering;
        _nextCalculator = new Queue<Func<bool>>();
        _nextCalculator.Enqueue(CalculateExiting);
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private void FixedUpdate()
    {
        if (_currentCalculator())
        {
            TargetAttainable?.Invoke(_isEntered);
            _isEntered = !_isEntered;
            _nextCalculator.Enqueue(_currentCalculator);
            _currentCalculator = _nextCalculator.Dequeue();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _trackingRadius);
    }

    public void InverseChecking()
    {
        if (_nextCalculator.Peek()())
            TargetAttainable?.Invoke(!_isEntered);
    }

    private bool CalculateEntering() => Vector3.Distance(_transform.position, _player.Position) <= _trackingRadius;

    private bool CalculateExiting() => Vector3.Distance(_transform.position, _player.Position) >= _trackingRadius;

    private bool OffCalculate() => false;

    private void OnPlayerDied()
    {
        _currentCalculator = OffCalculate;
        _nextCalculator.Clear();
        _nextCalculator.Enqueue(OffCalculate);
        TargetAttainable?.Invoke(false);
        enabled = false;
    }

}
