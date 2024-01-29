using UnityEngine;

/// <summary>
/// Controls the main camera movement based on user input.
/// </summary>
public class MainCameraMovement : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Vector2 _cameraXBounds;
    [SerializeField] private Vector2 _cameraYBounds;
    [SerializeField] private float _cameraSpeed;

    private void Start()
    {
        _inputManager.OnMiddleClicked += CameraMovement;
    }

    /// <summary>
    /// Moves the camera based on mouse input while clamping within specified bounds.
    /// </summary>
    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * _cameraSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _cameraSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position - new Vector3(mouseX, mouseY, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, _cameraXBounds.x, _cameraXBounds.y);
        newPosition.y = Mathf.Clamp(newPosition.y, _cameraYBounds.x, _cameraYBounds.y);
        transform.position = newPosition;
    }
}
