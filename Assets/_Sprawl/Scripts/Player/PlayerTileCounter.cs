using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerTileCounter
{
    [Inject] private Map _map;

    public int GetTileAmountOf(Player player)
    {
        return _map.Tiles.Count(tile => tile.Color == player.Color);
    }
}
