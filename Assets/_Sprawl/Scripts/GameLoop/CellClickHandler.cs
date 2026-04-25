using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CellClickHandler
{
    [Inject] private TurnController _turnController;

    public async UniTask HandleClick(Cell cell)
    {
        if(!cell.IsPainted && cell.CanBePaintedWith(_turnController.CurrentPlayer.Color))
        {
            cell.PaintWith(_turnController.CurrentPlayer.Color);
            await _turnController.SelectNextPlayer();
        }
    }
}
