using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetObject; // The object to orbit around
    public float distance = 10f; // The distance from the object
    public float height = 5f; // The height above the object
    public float rotationSpeed = 10f; // The speed at which the camera rotates
    public float speedMultiplier = 2f; // The amount to multiply the speed when shift is held
    private float currentAngle = 0f; // The current angle around the object

    private void Start()
    {
        transform.position = targetObject.position + Quaternion.Euler(0f, currentAngle, 0f) * new Vector3(0f, height, -distance);
        transform.LookAt(targetObject);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? rotationSpeed * speedMultiplier : rotationSpeed;
        currentAngle += horizontalInput * speed * Time.deltaTime;
        transform.position = targetObject.position + Quaternion.Euler(0f, currentAngle, 0f) * new Vector3(0f, height, -distance);
        transform.LookAt(targetObject);
    }
}
