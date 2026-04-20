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
    [Inject] private SerializedDictionary<PlayerColor, Color> _colorDictionary;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private TurnController _turnController;
    [Inject] private GameManager _gameManager;

    [SerializeField] private Image _borderImage;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Button _returnToMenuButton;

    private void Start()
    {
        _turnController.OnNextPlayerSelected += RecolorBorderWith;
        _turnController.OnOnePlayerLeft += OnFinished;

        _returnToMenuButton.onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
        _returnToMenuButton.gameObject.SetActive(false);

        _gameManager.IsInProgress = true;
        _borderImage.transform.DOScale(1f, 2f);
        RecolorBorderWith(_playerRegistry.GetByIndex(0).Color);
    }

    private void RecolorBorderWith(PlayerColor color)
    {
        _borderImage.DOColor(_colorDictionary[color], 1f);
    }

    private void OnFinished(PlayerColor color)
    {
        MoveBorder();
        _returnToMenuButton.gameObject.SetActive(true);
        _textMesh.text = $"Победил игрок <#{_colorDictionary[color].ToHexString()}>{color}";
    }

    private void MoveBorder()
    {
        _borderImage.transform.DOScale(1.1f, 1.5f);
    }
}
