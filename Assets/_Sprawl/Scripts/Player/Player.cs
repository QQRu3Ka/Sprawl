using System.Linq;
using UnityEngine;
using Zenject;

public class Player
{
    private PlayerColor _color;
    private bool _madeFirstTurn;

    public PlayerColor Color
    {
        get => _color;
        set => _color = value;
    }

    public bool MadeFirstTurn
    {
        get => _madeFirstTurn;
        set => _madeFirstTurn = value;
    }
}
