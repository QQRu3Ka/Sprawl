using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] private MapCatalogSO _mapCatalog;
    [Inject] private SelectedMapHolder _selectedMapHolder;
    public override void InstallBindings()
    {
        var mapPrefab = _mapCatalog.Data[_selectedMapHolder.SelectedMap].Map;

        Container.Bind<InputBlocker>().AsSingle();
        Container.Bind<TurnOrder>().AsSingle();
        Container.Bind<PlayerTileCounter>().AsSingle();
        Container.Bind<PlayerClickValidator>().AsSingle();
        Container.Bind<GameInputActions>().AsSingle();

        Container.Bind<Map>().FromComponentInNewPrefab(mapPrefab).AsSingle();
    }
}