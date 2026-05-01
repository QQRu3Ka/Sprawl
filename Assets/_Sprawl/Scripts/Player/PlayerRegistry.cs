using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerRegistry : IInitializable
{
    [Inject] private GameConfigSO _gameConfig;
    [Inject] private DiContainer _container;

    private List<Player> _players = new();
    private List<PlayerColor> _availableColors;

    public List<Player> Players => _players;

    public event Action<Player> OnPlayerAdd;

    public void Initialize()
    {
        _availableColors = Enum.GetValues(typeof(PlayerColor)).OfType<PlayerColor>().Except(new PlayerColor[] { PlayerColor.NONE }).ToList();
    }

    public void AddPlayer()
    {
        if (_players.Count >= _gameConfig.MaxPlayers) return;

        var takenColors = _players.Select(x => x.Color).ToList();
        var freeColors = _availableColors.Except(takenColors).ToList();

        //Если игроков нет -> maxId = 0; иначе -> maxId = _players.Max(x => x.Id).
        var maxId =_players.Count == 0 ? 0 : _players.Max(x => x.Id);

        var newPlayer = _container.Resolve<Player>();
        newPlayer.Id = maxId + 1;
        newPlayer.Color = freeColors[0];
        newPlayer.Name = $"Player {maxId + 1}";

        _players.Add(newPlayer);
        OnPlayerAdd?.Invoke(newPlayer);
    }

    public void Shuffle()
    {
        var rng = new System.Random();
        var n = _players.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (_players[k], _players[n]) = (_players[n], _players[k]);
        }
    }

    public Player GetByIndex(int index)
    {
        return _players[index];
    }

    public void UpdateNameOf(Player player, string name)
    {
        player.Name = name;
    }
}
