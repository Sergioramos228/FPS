using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private const float PointsToAttack = 1000;
    private const float MinDelayBetweenAttack = 0.5f;

    [SerializeField] private Character _character;

    private float _attackSpeed;
    private float _damage;
    private float _attackTime;
    private WaitForSeconds _delay;

    public event Action PreparedToAttack;

    private void Awake()
    {
        _attackTime = PointsToAttack;
        _delay = new WaitForSeconds(MinDelayBetweenAttack);
    }

    public void Attack()
    {
        if (_attackTime < PointsToAttack)
            return;

        _attackTime = 0;
        StartCoroutine(Reload());
        _character.TakeDamage(_damage);
    }

    public void ApplySettings(EnemySettings settings)
    {
        _damage = settings.Damage;
        _attackSpeed = settings.AttackSpeed;
    }

    private IEnumerator Reload()
    {
        yield return null;

        while (_attackTime < PointsToAttack)
        {
            yield return _delay;
            _attackTime += _attackSpeed * MinDelayBetweenAttack;
        }
        
        PreparedToAttack?.Invoke();
    }
}
