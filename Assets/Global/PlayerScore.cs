using System;

public class PlayerScore
{
    private int _finalScore;
    private int _score;
    
    public int PointsToFinish => _finalScore;
    public event Action<int> ScoreChanged;
    public event Action GoalReached;

    public void SetFinalScore(int newValue)
    {
        if (_finalScore == 0)
            _finalScore = newValue;
    }

    public void AddPoint()
    {
        _score++;
        ScoreChanged?.Invoke(_score);

        if (_score == _finalScore)
            GoalReached?.Invoke();
    }
}
