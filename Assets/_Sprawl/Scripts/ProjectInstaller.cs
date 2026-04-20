using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerRegistry>().AsSingle();
        Container.Bind<Player>().AsTransient();
    }
}