using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuCanvas : MonoBehaviour
{
    [Inject] private PlayerRegistry _playerRegistry;

    [SerializeField] private Button _addPlayersButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _resetPlayersButton;
    [SerializeField] private TextMeshProUGUI _info;

    private void Start()
    {
        _info.text = $"Игроков: {_playerRegistry.Players.Count}\n\nЗанятые цвета:\n{GetColors()}";
        _startGameButton.interactable = false;
        _addPlayersButton.onClick.AddListener(() => 
        { 
            _playerRegistry.AddPlayer();
            _info.text = $"Игроков: {_playerRegistry.Players.Count}\n\nЗанятые цвета:\n{GetColors()}";
            if (_playerRegistry.Players.Count >= 2) _startGameButton.interactable = true;
        });
        _startGameButton.onClick.AddListener(() =>
        {
            _addPlayersButton.interactable = false;
            _startGameButton.interactable = false;
            _playerRegistry.Shuffle();
            SceneManager.LoadSceneAsync(1);
        });
        _resetPlayersButton.onClick.AddListener(() =>
        {
            _playerRegistry.Players.Clear();
            _info.text = $"Игроков: {_playerRegistry.Players.Count}\n\nЗанятые цвета:\n{GetColors()}";
            _startGameButton.interactable = false;
        });
    }

    private string GetColors()
    {
        var stringBuilder = new StringBuilder();
        foreach(var player in _playerRegistry.Players)
        {
            stringBuilder.Append($"{player.Color}\n");
        }
        return stringBuilder.ToString();
    }
}
