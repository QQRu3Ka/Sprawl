using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SerializedDictionary<PlayerColor, PlayerColorConfig> _playerColorDictionary;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerRegistry>().AsSingle();
        Container.Bind<Player>().AsTransient();
        Container.Bind<PlayerColorConfigProvider>().AsSingle().WithArguments(_playerColorDictionary);
    }
}