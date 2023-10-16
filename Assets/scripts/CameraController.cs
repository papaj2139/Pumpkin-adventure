using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;   
    private float rotationX = 0.0f;    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;                     
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);  

        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);

        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
