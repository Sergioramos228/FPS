using System;
using System.Collections;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] private float _health;

    public float Current { get; private set; }
    public float Max => _health;
    public event Action HealthChanged;

    private void Start()
    {
        Current = Max;
        StartCoroutine(LazyStart());
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        if (Current <= 0)
            return;

        Current -= damage;

        if (Current <= 0)
        {
            Current = 0;
            Die();
        }

        HealthChanged?.Invoke();
    }

    protected abstract void Die();

    private IEnumerator LazyStart()
    {
        yield return new WaitForEndOfFrame();
        HealthChanged?.Invoke();
    }
}
