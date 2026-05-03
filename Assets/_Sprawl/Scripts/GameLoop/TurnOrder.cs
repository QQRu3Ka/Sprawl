using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnOrder
{
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private PlayerTileCounter _playerTileCounter;
    [Inject] private InputBlocker _inputBlocker;

    private int _index;

    public event Action<Player> OnOnePlayerLeft;
    public event Action<Player> OnNextPlayerSelected;

    private List<Player> _players => _playerRegistry.Players;

    public Player CurrentPlayer => _playerRegistry.GetByIndex(_index);

    public async UniTask SelectNextPlayer()
    {
        if (TryGetWinner(out Player winner))
        {
            OnOnePlayerLeft?.Invoke(winner);
            return;
        }

        _players[_index].MadeFirstTurn = true;

        if (_index + 1 == _players.Count)
        {
            _index = 0;
        }
        else
        {
            _index++;
        }

        await UniTask.WaitUntil(() => !_inputBlocker.IsBlocked);

        if (_playerTileCounter.GetTileAmountOf(_players[_index]) == 0 && _players[_index].MadeFirstTurn)
        {
            await SelectNextPlayer();
            return;
        }

        OnNextPlayerSelected?.Invoke(_players[_index]);
    }

    private bool TryGetWinner(out Player winner)
    {
        winner = null;

        if(_players.Any(player => !player.MadeFirstTurn))
        {
            return false;
        }

        var activePlayers = _players.Where(player => _playerTileCounter.GetTileAmountOf(player) > 0).ToList();

        if(activePlayers.Count == 1)
        {
            winner = activePlayers[0];
            return true;
        }

        return false;
    }
}
