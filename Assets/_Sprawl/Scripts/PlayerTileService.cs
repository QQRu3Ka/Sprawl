using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerTileService
{
    [Inject] private Map _map;
    [Inject] private PlayerRegistry _playerRegistry;

    public int GetTileAmount(Player player)
    {
        return _map.Tiles.Count(tile => tile.Color == player.Color);
    }
}
