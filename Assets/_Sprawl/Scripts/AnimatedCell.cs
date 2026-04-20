using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class AnimatedCell : MonoBehaviour
{
    [Inject] private SerializedDictionary<PlayerColor, Material> _colorMaterialDictionary;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetColor(PlayerColor _color)
    {
        _meshRenderer.material = _colorMaterialDictionary[_color];
    }
}
