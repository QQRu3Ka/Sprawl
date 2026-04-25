using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Tile : MonoBehaviour
{
    [Inject] private InputBlocker _inputBlocker;

    [SerializeField] private Vector2Int _position;

    [SerializeField] private bool _hasNorthNeighbor;
    [SerializeField] private bool _hasWestNeighbor;
    [SerializeField] private bool _hasEastNeighbor;
    [SerializeField] private bool _hasSouthNeighbor;

    private List<Cell> _cells;
    private List<AnimatedCell> _animatedCells;
    private int _paintedCells;
    private PlayerColor _color;
    private Animator _animator;
    private bool _isSpreading;

    public Vector2Int Position => _position;
    public bool HasNorthNeighbor => _hasNorthNeighbor;
    public bool HasWestNeighbor => _hasWestNeighbor;
    public bool HasEastNeighbor => _hasEastNeighbor;
    public bool HasSouthNeighbor => _hasSouthNeighbor;

    public PlayerColor Color => _color;

    public event Action<Tile> OnFullPainted;

    private void Awake()
    {
        _cells = GetComponentsInChildren<Cell>().ToList();
        _animatedCells = GetComponentsInChildren<AnimatedCell>(true).ToList();
        _animator = GetComponent<Animator>();
        foreach (var cell in _cells)
        {
            cell.OnPressed += OnPressedEventListener;
            cell.OnPainted += OnPaintedEventListener;
        }
    }
    private bool OnPressedEventListener(PlayerColor color)
    {
        if ((_color == PlayerColor.NONE || _color == color) && !_inputBlocker.IsBlocked)
        {
            return true;
        }
        return false;
    }

    private async void OnPaintedEventListener(PlayerColor color)
    {
        _paintedCells++;
        _color = color;
        await IsFullPainted();
    }

    private async UniTask IsFullPainted()
    {
        if (_paintedCells >= _cells.Count)
        {
            _isSpreading = true;
            _cells.ForEach(cell => cell.Clean());
            _paintedCells = 0;

            _inputBlocker.Block();
            await PlaySpreadAnimation();
            _inputBlocker.Unblock();

            OnFullPainted?.Invoke(this);
            _color = PlayerColor.NONE;
            _isSpreading = false;
        }
    }

    private async UniTask PlaySpreadAnimation()
    {
        _animatedCells.ForEach(cell => cell.RepaintWith(_color));
        _animator.SetTrigger("Spread");
        await UniTask.Delay(1000);
    }

    public void RepaintAfterSpreadWith(PlayerColor color)
    {
        //Проверка, чтобы у плитки не было доп. закрашиваний
        if (_isSpreading) return;
        _cells.Where(cell => cell.IsPainted).ToList().ForEach(cell => cell.RepaintSilentlyWith(color));
        var freeCells = _cells.Where(cell => !cell.IsPainted).ToList();
        freeCells[Random.Range(0, freeCells.Count)].PaintWith(color);
    }

    private void OnDestroy()
    {
        foreach (var cell in _cells)
        {
            cell.OnPressed -= OnPressedEventListener;
            cell.OnPainted -= OnPaintedEventListener;
        }
    }
}
