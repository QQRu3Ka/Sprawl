using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MapCatalogSO", menuName = "Scriptable Objects/MapCatalogSO")]
public class MapCatalogSO : ScriptableObject
{
    public SerializedDictionary<MapType, MapTypeData> Data;

    [Serializable]
    public class MapTypeData
    {
        public GameObject Map;
        public string Name;
    }
}
