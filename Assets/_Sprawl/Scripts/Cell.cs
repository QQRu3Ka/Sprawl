using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [Inject] private SerializedDictionary<PlayerColor, Material> _colorMaterialDictionary;
    [Inject] private TurnController _turnController;
    [Inject] private GameManager _gameManager;

    [SerializeField] private MeshRenderer _meshRenderer;

    public event Action<PlayerColor> OnPainted;
    public event Predicate<PlayerColor> OnPressed;

    private bool _isPainted;

    public bool IsPainted => _isPainted;

    public async void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !_isPainted && (OnPressed?.Invoke(_turnController.CurrentPlayer.Color) ?? false) && _gameManager.IsInProgress)
        {
            SetColor(_turnController.CurrentPlayer.Color);
            await _turnController.SelectNextPlayer();
        }
    }

    public void SetColor(PlayerColor color)
    {
        _meshRenderer.material = _colorMaterialDictionary[color];
        _isPainted = true;
        OnPainted?.Invoke(color);
    }

    public void SetColorOnSpread(PlayerColor color)
    {
        _meshRenderer.material = _colorMaterialDictionary[color];
    }

    public void Clean()
    {
        _meshRenderer.material = _colorMaterialDictionary[PlayerColor.NONE];
        _isPainted = false;
    }
}
