using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, Tile> _tiles;

    public List<Tile> Tiles
    {
        get => _tiles.Values.ToList();
    }

    private void Awake()
    {
        _tiles = GetComponentsInChildren<Tile>().ToDictionary(tile => tile.Position);
        _tiles.Values.ToList().ForEach(tile => tile.OnFullPainted += OnTileFullPainted);
    }

    private void OnTileFullPainted(Tile tile)
    {
        if (_tiles.Values.Where(tile => tile.Color != PlayerColor.NONE).Select(tile => tile.Color).ToHashSet().Count > 1)
        {
            foreach (var neighborTile in GetNearbyTilesOf(tile))
            {
                neighborTile.RepaintAfterSpreadWith(tile.Color);
            }
        }
    }

    private List<Tile> GetNearbyTilesOf(Tile tile)
    {
        List<Tile> tiles = new();
        var neighbors = new (bool has, Vector2Int offset)[]
        {
            (tile.HasNorthNeighbor, Vector2Int.right),
            (tile.HasWestNeighbor,  Vector2Int.down),
            (tile.HasEastNeighbor,  Vector2Int.up),
            (tile.HasSouthNeighbor, Vector2Int.left)
        };
        var coords = tile.Position;
        foreach (var (hasNeighbor, offset) in neighbors)
        {
            if (hasNeighbor)
            {
                tiles.Add(_tiles[coords + offset]);
            }
        }
        return tiles;
    }
}
