using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SerializedDictionary<PlayerColor, Material> _colorMaterialDictionary;
    [SerializeField] private SerializedDictionary<PlayerColor, Color> _colorDictionary;
    [SerializeField] private Map _map;

    public override void InstallBindings()
    {
        Container.BindInstance(_colorMaterialDictionary);
        Container.BindInstance(_colorDictionary);
        Container.Bind<InputBlocker>().AsSingle();
        Container.Bind<TurnController>().AsSingle();
        Container.Bind<PlayerTileService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindInstance(_map);
    }
}