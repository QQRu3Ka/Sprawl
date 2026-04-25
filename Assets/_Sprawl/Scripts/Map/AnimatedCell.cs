using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class AnimatedCell : MonoBehaviour
{
    [Inject] private PlayerColorConfigProvider _playerColorConfigProvider;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void RepaintWith(PlayerColor color)
    {
        _meshRenderer.material = _playerColorConfigProvider.Get(color).CellMaterial;
    }
}
