using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapList : MonoBehaviour
{
    [Inject] private MapCatalogSO _mapCatalog;
    [Inject] private SelectedMapHolder _selectedMapHolder;

    [SerializeField] private GameObject _buttonPrefab;

    private List<Button> _buttons;

    private void Start()
    {
        _buttons = new List<Button>();
        foreach(var data in _mapCatalog.Data)
        {
            var buttonObj = Instantiate(_buttonPrefab, transform);
            var button = buttonObj.GetComponent<Button>();
            _buttons.Add(button);

            button.GetComponentInChildren<TextMeshProUGUI>().text = data.Value.Name;
            button.onClick.AddListener(() => _selectedMapHolder.SelectedMap = data.Key);
        }
    }

    private void OnDestroy()
    {
        foreach(var button in _buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
