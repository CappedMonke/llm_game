using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;

    [Header("General")]
    public Transform cameraTransform;

    [Header("Additional Functionality")]
    public bool keyboardMovement = true;
    public bool dragMovement = true;

    [Header("Keyboard Movement")]
    private float movementSpeedDefault = 0.4f;
    private float movementSpeedFast = 2.5f;
    private float movementSpeed;
    public float cameraSnappiness = 1f;

    [Header("Drag Movement")]
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        movementSpeed = movementSpeedDefault;
    }

    // Update is called once per frame
    void Update() {
        HandleCameraMovement();
    
        if (Input.GetKeyDown(KeyCode.LeftShift)) { movementSpeed = movementSpeedDefault * movementSpeedFast; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { movementSpeed = movementSpeedDefault; }
    }

    private void HandleCameraMovement() {
        Vector3 newPosition = Vector3.zero;
        if (keyboardMovement) { newPosition = HandleKeyboardMovement(); }
        if (dragMovement) { newPosition += HandleDragMovement(); }

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.05f * cameraSnappiness);
    }

    private Vector3 HandleKeyboardMovement() {
        Vector3 deltaPosition = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            deltaPosition += (transform.forward);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            deltaPosition -= (transform.forward);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            deltaPosition += (transform.right);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            deltaPosition -= (transform.right);
        }
        deltaPosition.Normalize();
        return transform.position + (deltaPosition * movementSpeed);
    }

    private Vector3 HandleDragMovement() {
        if (Input.GetMouseButtonDown(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry)) {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float entry)) {
                dragCurrentPosition = ray.GetPoint(entry);
                return (dragStartPosition - dragCurrentPosition);
            }
        }
        return Vector3.zero;
    }
}
