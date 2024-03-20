using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private const float TimeDelay = 0.5f;

    [SerializeField] private Character _player;
    [SerializeField] private EnemyTargetTracker _targetTracker;
    [SerializeField] private float _attackDistance = 1.8f;

    private Vector3 _bornPosition;
    private Coroutine _coroutine;
    private NavMeshAgent _agent;
    private Transform _transform;
    private bool _isReadyToCaught;
    private bool _isInMove;

    public event Action PlayerCaught;
    public event Action<bool> StateChanged;

    public void Awake()
    {
        _transform = transform;
        _bornPosition = _transform.position;
        _isReadyToCaught = true;

        if (TryGetComponent<NavMeshAgent>(out _agent) == false)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    private void OnEnable()
    {
        _targetTracker.TargetAttainable += OnTargetAttainable;
    }

    private void OnDisable()
    {
        _targetTracker.TargetAttainable -= OnTargetAttainable;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void FixedUpdate()
    {
        if (_isInMove == _agent.velocity.sqrMagnitude.Equals(0))
        {
            _isInMove = !_isInMove;
            StateChanged?.Invoke(_isInMove);
        }
    }

    private void OnTargetAttainable(bool isEntered)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (isEntered)
            _coroutine = StartCoroutine(CaughtPlayer());
        else
            _agent.SetDestination(_bornPosition);

    }

    private IEnumerator CaughtPlayer()
    {
        yield return null;

        WaitForSeconds delay = new WaitForSeconds(TimeDelay);

        while (Vector3.Distance(_player.Position, _transform.position) >= _attackDistance || _isReadyToCaught == false)
        {
            _agent.SetDestination(_player.Position);
            yield return delay;
        }

        PlayerCaught?.Invoke();
        _agent.SetDestination(_transform.position);
        _coroutine = null;
        _isReadyToCaught = false;
    }

    public void SetReadyToCaught()
    {
        _isReadyToCaught = true;
        _targetTracker.InverseChecking();
    }
}
