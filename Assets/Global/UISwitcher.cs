using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private GameMonitor _play;
    [SerializeField] private GameMonitor _start;
    [SerializeField] private GameMonitor _die;
    [SerializeField] private GameMonitor _finish;
    [SerializeField] private Character _player;
    [SerializeField] private Button _startButton;
    [Inject] private PlayerScore _score;

    private GameMonitor _current;

    private void Awake()
    {
        _start.Show();
        _current = _start;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _score.GoalReached += OnScoreReached;
        _startButton.onClick.AddListener(OnStart);
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _score.GoalReached -= OnScoreReached;
        _startButton.onClick.RemoveListener(OnStart);
    }

    private void OnPlayerDied() => ChangeCurrent(_die);

    private void OnScoreReached() => ChangeCurrent(_finish);

    private void OnStart() => ChangeCurrent(_play);

    private void ChangeCurrent(GameMonitor current)
    {
        _current.Hide();
        _current = current;
        _current.Show();
    }
}
