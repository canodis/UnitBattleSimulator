using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Vector2 cameraXBounds;
    [SerializeField] private Vector2 cameraYBounds;

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;

            Vector3 newPosition = transform.position - new Vector3(mouseX, mouseY, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, cameraXBounds.x, cameraXBounds.y);
            newPosition.y = Mathf.Clamp(newPosition.y, cameraYBounds.x, cameraYBounds.y);
            transform.position = newPosition;
        }
    }
}
