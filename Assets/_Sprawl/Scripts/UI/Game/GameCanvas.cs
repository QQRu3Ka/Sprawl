using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameCanvas : MonoBehaviour
{
    [Inject] private PlayerColorConfigProvider _playerColorConfigProvider;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private TurnOrder _turnOrder;

    [SerializeField] private Image _borderImage;
    [SerializeField] private TextMeshProUGUI _winnerTextMesh;
    [SerializeField] private TextMeshProUGUI _currentPlayerTextMesh;
    [SerializeField] private Button _returnToMenuButton;

    private void Start()
    {
        _turnOrder.OnNextPlayerSelected += OnNextPlayerSelectedEventListener;
        _turnOrder.OnOnePlayerLeft += OnOnePlayerLeftEventListener;

        _returnToMenuButton.onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
        _returnToMenuButton.gameObject.SetActive(false);

        _borderImage.transform.DOScale(1f, 1.5f);
        RecolorBorderWith(_playerRegistry.GetByIndex(0).Color);
        ChangeCurrentPlayerWith(_playerRegistry.GetByIndex(0));
    }

    private void OnNextPlayerSelectedEventListener(Player player)
    {
        RecolorBorderWith(player.Color);
        ChangeCurrentPlayerWith(player);
    }

    private void RecolorBorderWith(PlayerColor color)
    {
        _borderImage.DOColor(_playerColorConfigProvider.Get(color).Color, 1f);
    }

    private void ChangeCurrentPlayerWith(Player player)
    {
        _currentPlayerTextMesh.text = $"Ход игрока {player.Name.ColorWith(_playerColorConfigProvider.Get(player.Color).Color)}";
    }

    private void OnOnePlayerLeftEventListener(Player player)
    {
        MoveBorderOutOfScreen();
        _returnToMenuButton.gameObject.SetActive(true);
        _winnerTextMesh.text = $"Победил игрок {player.Name.ColorWith(_playerColorConfigProvider.Get(player.Color).Color)}";
    }

    private void MoveBorderOutOfScreen()
    {
        _borderImage.transform.DOScale(1.2f, 1.5f);
    }

    private void OnDestroy()
    {
        _turnOrder.OnNextPlayerSelected -= OnNextPlayerSelectedEventListener;
        _turnOrder.OnOnePlayerLeft -= OnOnePlayerLeftEventListener;
        _returnToMenuButton.onClick.RemoveAllListeners();
    }
}
