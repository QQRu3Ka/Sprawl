using AYellowpaper.SerializedCollections;
using DG.Tweening;
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
    [Inject] private TurnController _turnController;

    [SerializeField] private Image _borderImage;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Button _returnToMenuButton;

    private void Start()
    {
        _turnController.OnNextPlayerSelected += RecolorBorderWith;
        _turnController.OnOnePlayerLeft += OnFinished;

        _returnToMenuButton.onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
        _returnToMenuButton.gameObject.SetActive(false);

        _borderImage.transform.DOScale(1f, 2f);
        RecolorBorderWith(_playerRegistry.GetByIndex(0).Color);
    }

    private void RecolorBorderWith(PlayerColor color)
    {
        _borderImage.DOColor(_playerColorConfigProvider.Get(color).Color, 1f);
    }

    private void OnFinished(PlayerColor color)
    {
        MoveBorder();
        _returnToMenuButton.gameObject.SetActive(true);
        _textMesh.text = $"Победил игрок <#{_playerColorConfigProvider.Get(color).Color.ToHexString()}>{color}";
    }

    private void MoveBorder()
    {
        _borderImage.transform.DOScale(1.1f, 1.5f);
    }

    private void OnDestroy()
    {
        _turnController.OnNextPlayerSelected -= RecolorBorderWith;
        _turnController.OnOnePlayerLeft -= OnFinished;
    }
}
