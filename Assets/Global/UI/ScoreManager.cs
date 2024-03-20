using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    private const string WinKey = "WinScore";
    private const string LoseKey = "LoseScore";

    [SerializeField] private TMP_Text _winView;
    [SerializeField] private TMP_Text _loseView;
    [SerializeField] private Character _player;
    [Inject] private PlayerScore _playerScore;

    private void OnEnable()
    {
        _playerScore.GoalReached += RegistryWin;
        _player.Died += RegistryLose;
    }

    private void OnDisable()
    {
        _playerScore.GoalReached -= RegistryWin;
        _player.Died -= RegistryLose;
    }

    private void Start()
    {
        _winView.text = $"Win: {PlayerPrefs.GetInt(WinKey, 0)}";
        _loseView.text = $"Lose: {PlayerPrefs.GetInt(LoseKey, 0)}";
    }

    private void RegistryWin()
    {
        PlayerPrefs.SetInt(WinKey, PlayerPrefs.GetInt(WinKey, 0) + 1);
        PlayerPrefs.Save();
    }

    private void RegistryLose()
    {
        PlayerPrefs.SetInt(LoseKey, PlayerPrefs.GetInt(LoseKey, 0) + 1);
        PlayerPrefs.Save();
    }
}
