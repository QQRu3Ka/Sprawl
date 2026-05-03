using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraZoom : MonoBehaviour
{
    [Inject] private GameInputActions _inputActions;

    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minZPosition;
    [SerializeField] private float _maxZPosition;

    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        _inputActions.Enable();
    }

    private void Update()
    {
        var scrollValue = _inputActions.Camera.Zoom.ReadValue<Vector2>();
        if (scrollValue != Vector2.zero)
        {
            var position = _cameraTransform.localPosition;

            position.z += scrollValue.y * _zoomSpeed * Time.deltaTime;
            position.z = Mathf.Clamp(position.z, _minZPosition, _maxZPosition);

            _cameraTransform.localPosition = position;
        }
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
    }
}
