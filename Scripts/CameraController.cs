using UnityEngine;

/*
    This is archived file and server no purpose in final game
*/
public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementTime;

    // Speed
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;

    // Direction
    public Vector3 newPosition;

    // Rotation
    public Quaternion newRotation;
    public float rotationAmount;

    // Zoom
    public Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 originalZoom;

    private void Start()
    {
        //cameraTransform = transform.GetChild(0);
        movementSpeed = normalSpeed;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        originalZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    void HandleMovementInput()
    {
        // Speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        // Direction
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        // Zoom
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        // If player gets lost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newPosition = Vector3.zero;
            newRotation = Quaternion.Euler(0, 0, 0);
            newZoom = originalZoom;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount * 6;
        }
    }
}
