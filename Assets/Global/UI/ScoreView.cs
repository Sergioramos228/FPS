using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [Inject] private PlayerScore _score;

    private void Awake()
    {
        _score.ScoreChanged += OnScoreChanged;
        OnScoreChanged(0);
    }

    private void OnScoreChanged(int score)
    {
        _label.text = $"{score} / {_score.PointsToFinish}";
    }
}
