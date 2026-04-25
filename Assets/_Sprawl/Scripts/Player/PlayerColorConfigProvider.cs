using System.Collections.Generic;
using UnityEngine;

public class PlayerColorConfigProvider
{
    private Dictionary<PlayerColor, PlayerColorConfig> _dictionary;

    public PlayerColorConfigProvider(Dictionary<PlayerColor, PlayerColorConfig> dictionary)
    {
        _dictionary = dictionary;
    }

    public PlayerColorConfig Get(PlayerColor color) => _dictionary[color];
}
