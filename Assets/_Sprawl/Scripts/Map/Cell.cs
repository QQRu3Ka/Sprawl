using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [Inject] private PlayerColorConfigProvider _playerColorConfigProvider;
    [Inject] private CellClickHandler _cellClickHandler;

    [SerializeField] private MeshRenderer _meshRenderer;

    public event Action<PlayerColor> OnPainted;
    public event Predicate<PlayerColor> OnPressed;

    private bool _isPainted;

    public bool IsPainted => _isPainted;

    public bool CanBePaintedWith(PlayerColor color) => OnPressed?.Invoke(color) ?? false;

    public async void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            await _cellClickHandler.HandleClick(this);
        }
    }

    public void PaintWith(PlayerColor color)
    {
        _meshRenderer.material = _playerColorConfigProvider.Get(color).CellMaterial;
        _isPainted = true;
        OnPainted?.Invoke(color);
    }

    public void RepaintSilentlyWith(PlayerColor color)
    {
        _meshRenderer.material = _playerColorConfigProvider.Get(color).CellMaterial;
    }

    public void Clean()
    {
        _meshRenderer.material = _playerColorConfigProvider.Get(PlayerColor.NONE).CellMaterial;
        _isPainted = false;
    }
}
