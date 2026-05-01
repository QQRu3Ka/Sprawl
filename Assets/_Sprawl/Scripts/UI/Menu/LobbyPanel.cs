using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LobbyPanel : MonoBehaviour
{
    [Inject] private PlayerRegistry _playerRegistry;

    [SerializeField] private Button _startGameButton;

    private void Start()
    {
        _playerRegistry.OnPlayerAdd += OnPlayerAddEventListener;
        _startGameButton.onClick.AddListener(() =>
        {
            _playerRegistry.Shuffle();
            SceneManager.LoadSceneAsync(1);
        });
        _startGameButton.gameObject.SetActive(false);
    }

    private void OnPlayerAddEventListener(Player player)
    {
        if (_playerRegistry.Players.Count >= 2) _startGameButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _playerRegistry.OnPlayerAdd -= OnPlayerAddEventListener;
        _startGameButton.onClick.RemoveAllListeners();
    }
}
