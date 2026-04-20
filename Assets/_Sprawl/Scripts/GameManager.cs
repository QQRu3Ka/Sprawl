using System;
using UnityEngine;
using Zenject;

public class GameManager : IInitializable
{
    [Inject] private TurnController _turnController;

    private bool _isInProgress;

    public bool IsInProgress
    {
        get => _isInProgress;
        set => _isInProgress = value;
    }

    public void Initialize()
    {
        _turnController.OnOnePlayerLeft += EndGame;
    }

    private void EndGame(PlayerColor color)
    {
        Debug.Log("Игра окончена");
        _isInProgress = false;
    }
}
