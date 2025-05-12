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

    [Header("Rotation Settings")]
    private float rotationSpeed = 1.5f;
    private float rotationMaxXDelta = 8f;
    private float rotationXStart;

    [Header("Zoom Settings")]
    private int zoomCurrent = 2;
    private int zoomMax = 4;
    private int zoomMin = 0;
    private float zoomSpeedMultiplier = 1.3f;
    private float zoomSpeedBonus;
    private int[] zoomAngles = { 75, 62, 50, 35, 20 };
    private int[] zoomHeight = { 80, 40, 25, 15, 5 };
    private int[] zoomFOV = { 35, 30, 25, 25, 25 };

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        movementSpeed = movementSpeedDefault;
        rotationXStart = zoomAngles[zoomCurrent];
        zoomSpeedBonus = Mathf.Pow(zoomSpeedMultiplier, (zoomMax - zoomCurrent));
    }

    // Update is called once per frame
    void Update() {
        HandleCameraMovement();
    
        if (Input.GetKeyDown(KeyCode.LeftShift)) { movementSpeed = movementSpeedDefault * movementSpeedFast; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { movementSpeed = movementSpeedDefault; }
    }

    private void HandleCameraMovement() {
        HandleRotation();

        Vector3 newPosition = HandleZoom();

        if (keyboardMovement) { newPosition += HandleKeyboardMovement(); }
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
        return transform.position + (deltaPosition * movementSpeed * zoomSpeedBonus);
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

    private void HandleRotation() {
        if (Input.GetMouseButton(1)) {
            transform.eulerAngles += new Vector3(0, rotationSpeed * Input.GetAxis("Mouse X"), 0);
            float additionalRotation = rotationSpeed * -0.5f * Input.GetAxis("Mouse Y");
            float currentRotation = transform.GetChild(0).eulerAngles.x;
            if (additionalRotation > 0) { // Going down => More Rotation
                if (currentRotation > rotationXStart + rotationMaxXDelta) return;
                else {
                    additionalRotation = Mathf.Min(rotationXStart + rotationMaxXDelta - currentRotation, additionalRotation);
                }
            } else {
                if (currentRotation < rotationXStart - rotationMaxXDelta) return;
                else {
                    additionalRotation = Mathf.Max(rotationXStart - rotationMaxXDelta - currentRotation, additionalRotation);
                }
            }
            transform.GetChild(0).eulerAngles += new Vector3(additionalRotation, 0, 0);
        }
    }

    private Vector3 HandleZoom() {
        Vector3 zoomDelta = Vector3.zero;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && zoomCurrent < zoomMax) { // Zoom in
            zoomCurrent++;
            zoomSpeedBonus = Mathf.Pow(zoomSpeedMultiplier, (zoomMax - zoomCurrent));
            if (zoomCurrent == zoomMax) zoomDelta = transform.forward * 180;
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f && zoomCurrent > zoomMin) { // Zoom out
            zoomCurrent--;
            zoomSpeedBonus = Mathf.Pow(zoomSpeedMultiplier, (zoomMax - zoomCurrent));
            if (zoomCurrent == zoomMax - 1) zoomDelta = transform.forward * -180;
        } else {
            return Vector3.zero;
        }
        transform.position = new Vector3(
            transform.position.x,
            zoomHeight[zoomCurrent],
            transform.position.z);

        rotationXStart = zoomAngles[zoomCurrent];

            transform.GetChild(0).transform.eulerAngles = new Vector3(
                zoomAngles[zoomCurrent],
                transform.GetChild(0).transform.eulerAngles.y,
                transform.GetChild(0).transform.eulerAngles.z);

        transform.GetChild(0).GetComponent<Camera>().fieldOfView = zoomFOV[zoomCurrent];

        return zoomDelta;
    }
}
