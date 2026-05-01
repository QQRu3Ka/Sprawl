using TMPro;
using UnityEngine;
using Zenject;

public class PlayerDescription : MonoBehaviour
{
    [Inject] private PlayerRegistry _playerRegistry;

    [SerializeField] private TMP_InputField _nameInputField;

    private Player _player;

    private void Start()
    {
        _nameInputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string text)
    {
        _playerRegistry.UpdateNameOf(_player, text);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void OnDestroy()
    {
        _nameInputField.onValueChanged.RemoveAllListeners();
    }
}
