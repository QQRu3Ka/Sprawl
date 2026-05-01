using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayersPanel : MonoBehaviour
{
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private DiContainer _container;

    [SerializeField] private GameObject _playersList;
    [SerializeField] private Button _addPlayerButton;
    [SerializeField] private GameObject _playerDescription;

    private void Start()
    {
        foreach (var player in _playerRegistry.Players)
        {
            InstantiatePlayerDescriptionWith(player);
        }
        _playerRegistry.OnPlayerAdd += OnPlayerAddEventListener;
        _addPlayerButton.onClick.AddListener(() =>
        {
            _playerRegistry.AddPlayer();
        });
    }

    private void OnPlayerAddEventListener(Player player)
    {
        InstantiatePlayerDescriptionWith(player);
    }

    private void InstantiatePlayerDescriptionWith(Player player)
    {
        var obj = Instantiate(_playerDescription, _playersList.transform);

        var desc = obj.GetComponent<PlayerDescription>();
        _container.Inject(desc);
        desc.SetPlayer(player);
    }

    private void OnDestroy()
    {
        _addPlayerButton.onClick.RemoveAllListeners();
        _playerRegistry.OnPlayerAdd -= OnPlayerAddEventListener;
    }
}
