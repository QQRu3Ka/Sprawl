using System;
using UnityEngine;
using Zenject;

public class GameManager : IInitializable, IDisposable
{
    [Inject] private TurnController _turnController;

    public void Initialize()
    {
        _turnController.OnOnePlayerLeft += EndGame;
    }

    private void EndGame(PlayerColor color)
    {
        Debug.Log("Игра окончена");
    }

    public void Dispose()
    {
        _turnController.OnOnePlayerLeft -= EndGame;
    }
}
