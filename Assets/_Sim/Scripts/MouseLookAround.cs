using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Settings")]
    public Transform target;
    public float sensitivity = 5.0f;
    public float minAngle = -90f; // Limit to half circle (left)
    public float maxAngle = 90f;  // Limit to half circle (right)

    private float currentZRotation = 0f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // 1. Get mouse movement
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;

            // 2. Update rotation based on mouse
            currentZRotation += mouseX;
            
            // 3. Clamp the rotation to a 180-degree half-circle
            currentZRotation = Mathf.Clamp(currentZRotation, minAngle, maxAngle);

            // 4. Apply rotation to the CameraBase
            target.localRotation = Quaternion.Euler(0, 0, currentZRotation);
        }
        
    }
}
