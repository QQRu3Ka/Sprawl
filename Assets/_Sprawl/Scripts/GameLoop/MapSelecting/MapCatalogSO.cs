using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MapCatalogSO", menuName = "Scriptable Objects/MapCatalogSO")]
public class MapCatalogSO : ScriptableObject
{
    public SerializedDictionary<MapType, GameObject> Prefabs;
}
