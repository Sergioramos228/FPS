using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float PreDecayDelay = 0.1f;
    private const float DecayTime = 4;

    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private AnimationRouter _animator;
    [SerializeField] private EnemyAttacker _attacker;
    [SerializeField] private Killable _hitPoints;
    [SerializeField] private EnemySettings _settings;
    [SerializeField] private SkinnedMeshRenderer _view;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _view.material = _settings.Material;
        transform.localScale = Vector3.one * _settings.Size;
        _attacker.ApplySettings(_settings);
    }

    private void OnEnable()
    {
        _movement.StateChanged += _animator.Run;
        _attacker.PreparedToAttack += _movement.SetReadyToCaught;
        _movement.PlayerCaught += OnPlayerCaught;
        _hitPoints.HealthChanged += _animator.Damage;
        _hitPoints.Died += OnDied;
    }

    private void OnDisable()
    {
        DeSubscribe();
    }

    private void OnPlayerCaught()
    {
        _attacker.Attack();
        _animator.Attack();
    }

    private void OnDied()
    {
        Died?.Invoke(this);
        DeSubscribe();
        _movement.enabled = false;
        _attacker.enabled = false;
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(PreDecayDelay);
        _animator.Death();
        yield return new WaitForSeconds(DecayTime);
        gameObject.SetActive(false);
    }

    private void DeSubscribe()
    {
        _movement.StateChanged -= _animator.Run;
        _attacker.PreparedToAttack -= _movement.SetReadyToCaught;
        _movement.PlayerCaught -= OnPlayerCaught;
        _hitPoints.HealthChanged -= _animator.Damage;
        _hitPoints.Died -= OnDied;
    }
}
