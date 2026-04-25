using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnController
{
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private PlayerTileService _playerTileService;
    [Inject] private InputBlocker _inputBlocker;

    private int _index;

    public event Action<PlayerColor> OnOnePlayerLeft;
    public event Action<PlayerColor> OnNextPlayerSelected;

    private List<Player> _players => _playerRegistry.Players;

    public Player CurrentPlayer => _playerRegistry.GetByIndex(_index);

    public async UniTask SelectNextPlayer()
    {
        if (!_players.Exists(player => !player.MadeFirstTurn) && 
            _players.Count(player => _playerTileService.GetTileAmount(player) > 0) == 1)
        {
            OnOnePlayerLeft?.Invoke(_players.Where(player => _playerTileService.GetTileAmount(player) > 0).First().Color);
            return;
        }

        if (!_players[_index].MadeFirstTurn) _players[_index].MadeFirstTurn = true;

        if (_index + 1 == _players.Count)
        {
            _index = 0;
        }
        else
        {
            _index++;
        }

        await UniTask.WaitUntil(() => !_inputBlocker.IsBlocked);

        if (_playerTileService.GetTileAmount(_players[_index]) == 0 && _players[_index].MadeFirstTurn)
        {
            Debug.Log($"Игрок {_players[_index].Color} больше не в игре");
            await SelectNextPlayer();
            return;
        }

        OnNextPlayerSelected?.Invoke(_players[_index].Color);
    }
}
