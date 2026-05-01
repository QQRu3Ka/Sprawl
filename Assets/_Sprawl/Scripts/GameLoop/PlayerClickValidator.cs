using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerClickValidator
{
    [Inject] private TurnOrder _turnOrder;

    public async UniTask ValidateClickOn(Cell cell)
    {
        if(!cell.IsPainted && cell.CanBePaintedWith(_turnOrder.CurrentPlayer.Color))
        {
            cell.PaintWith(_turnOrder.CurrentPlayer.Color);
            await _turnOrder.SelectNextPlayer();
        }
    }
}
