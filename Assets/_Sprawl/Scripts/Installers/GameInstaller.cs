using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Map _map;

    public override void InstallBindings()
    {
        Container.Bind<InputBlocker>().AsSingle();
        Container.Bind<TurnController>().AsSingle();
        Container.Bind<PlayerTileService>().AsSingle();
        Container.Bind<CellClickHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindInstance(_map);
    }
}