using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Character _player;

    private void OnEnable()
    {
        _start.onClick.AddListener(OnGameStart);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(OnGameStart);
    }

    private void OnGameStart()
    {
        _player.EnterGame();
    }
}
