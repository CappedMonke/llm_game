using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UnitSelectionManager : MonoBehaviour {
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private Camera cam;
    [Header("Masks")]
    public LayerMask maskClickable;
    public LayerMask maskGround;

    [Header("Marker")]
    public GameObject groundMarker;

    [Header("Overlays")]
    public GameObject singleUnitOverlay;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        cam = Camera.main;
    }


    void Update() {
        if (!GameController.Instance.isPlaying) return;
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, maskClickable)) {
                    SelectUnit(hit.collider.gameObject, Input.GetKey(KeyCode.LeftShift));
                } else {
                    // Prevent deselection when multiselect is active
                    if (!Input.GetKey(KeyCode.LeftShift)) DeselectAll();
                }
            }
        }

        if (unitsSelected.Count > 0) {
            if (Input.GetMouseButtonDown(1)) {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, maskGround)) {
                    groundMarker.transform.position = hit.point;
                    groundMarker.SetActive(true);

                    foreach (GameObject unit in unitsSelected) {
                        unit.GetComponent<UnitMovement>().SetDestination(hit);
                    }
                }
            }
        }
    }

    private void DeselectAll() {
        foreach (GameObject unit in unitsSelected) {
            SetUnitMovement(unit, false);
        }
        unitsSelected.Clear();

        groundMarker.SetActive(false);
        singleUnitOverlay.SetActive(false);
    }

    public void SelectUnit(GameObject selected, bool multiSelect) {
        if (!multiSelect) DeselectAll();

        if (!unitsSelected.Contains(selected)) {
            unitsSelected.Add(selected);
            SetUnitMovement(selected, true);
        } else {
            unitsSelected.Remove(selected);
            SetUnitMovement(selected, false);
        }

        if (unitsSelected.Count == 1) singleUnitOverlay.SetActive(true);
        else singleUnitOverlay.SetActive(false);
    }

    private void SetUnitMovement(GameObject selected, bool movementEnabled) {
        selected.GetComponent<UnitMovement>().enabled = movementEnabled;
        selected.transform.GetChild(0).gameObject.SetActive(movementEnabled);
    }

    public void ActivateUnits() {
        foreach (GameObject unit in units) {
            unit.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
