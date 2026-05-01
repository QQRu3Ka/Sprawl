using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuCanvas : MonoBehaviour
{
    [Inject] private PlayerRegistry _playerRegistry;

    [SerializeField] private Button _resetPlayersButton;
    [SerializeField] private TextMeshProUGUI _info;

    private void Start()
    {
        _info.text = $"Игроков: {_playerRegistry.Players.Count}\n\nЗанятые цвета:\n{GetColors()}";
        _resetPlayersButton.onClick.AddListener(() =>
        {
            _playerRegistry.Players.Clear();
            _info.text = $"Игроков: {_playerRegistry.Players.Count}\n\nЗанятые цвета:\n{GetColors()}";
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

    private void OnDestroy()
    {
        _resetPlayersButton.onClick.RemoveAllListeners();
    }
}
