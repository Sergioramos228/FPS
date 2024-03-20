using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemyMaster : MonoBehaviour
{
    private List<Enemy> _enemies;

    [Inject] private PlayerScore _score;

    private void Awake()
    {
        _enemies = transform.GetComponentsInChildren<Enemy>().ToList();
        _score.SetFinalScore(_enemies.Count);
    }

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _score.AddPoint();
        enemy.Died -= OnEnemyDied;
        _enemies.Remove(enemy);
    }
}