using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
    [SerializeField] private GameConfigSO _gameConfig;
    [SerializeField] private MapCatalogSO _mapCatalog;
    public override void InstallBindings()
    {
        Container.BindInstance(_gameConfig);
        Container.BindInstance(_mapCatalog);
    }
}