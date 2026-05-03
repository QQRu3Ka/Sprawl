using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraRotation : MonoBehaviour
{
    [Inject] private GameInputActions _inputActions;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private Transform _cameraAnchor;

    private void Start()
    {
        _cameraAnchor = GetComponent<Transform>();
        _inputActions.Enable();
    }

    private void Update()
    {
        if (_inputActions.Camera.RightClick.IsPressed())
        {
            var moveValue = _inputActions.Camera.Move.ReadValue<Vector2>();
            if (moveValue != Vector2.zero)
            {
                var angles = _cameraAnchor.eulerAngles;

                angles.x -= moveValue.y * _rotationSpeed * Time.deltaTime;
                angles.x = Mathf.Clamp(angles.x, _minX, _maxX);

                angles.y += moveValue.x * _rotationSpeed * Time.deltaTime;

                _cameraAnchor.eulerAngles = angles;
            }
        }
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
    }
}
