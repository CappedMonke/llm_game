using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionUI : MonoBehaviour {
    Camera cam;

    public RectTransform boxVisual;
    Rect selectionBox;

    Vector2 startPos;
    Vector2 endPos;

    public List<GameObject> unitsHovered = new List<GameObject>();

    void Start() {
        cam = Camera.main;
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        DrawVisual();
    }

    private void Update() {
        // Clicking
        if (Input.GetMouseButtonDown(0)) {
            startPos = Input.mousePosition;
            selectionBox = new Rect();
        }
        // Dragging
        if (Input.GetMouseButton(0)) {
            if (boxVisual.rect.width > 0 || boxVisual.rect.height > 0) {
                SelectUnits();
            }
            endPos = Input.mousePosition;
            DrawVisual();
            DrawSelection();

        }
        if (Input.GetMouseButtonUp(0)) {
            startPos = Vector2.zero;
            endPos = Vector2.zero;
            DrawVisual();
            unitsHovered.Clear();
        }
    }

    private void DrawVisual() {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPos;

        boxVisual.position = (boxStart + boxEnd) / 2;
        boxVisual.sizeDelta = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
    }

    private void DrawSelection() {
        selectionBox.xMin = Mathf.Min(Input.mousePosition.x, startPos.x);
        selectionBox.xMax = Mathf.Max(Input.mousePosition.x, startPos.x);

        selectionBox.yMin = Mathf.Min(Input.mousePosition.y, startPos.y);
        selectionBox.yMax = Mathf.Max(Input.mousePosition.y, startPos.y);
    }

    private void SelectUnits() {
        foreach (GameObject unit in UnitSelectionManager.Instance.units) {
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position))) {
                unitsHovered.Add(unit);
                if (!UnitSelectionManager.Instance.unitsSelected.Contains(unit)) {
                    UnitSelectionManager.Instance.SelectUnit(unit, true);
                }
            } else {
                if (UnitSelectionManager.Instance.unitsSelected.Contains(unit) && unitsHovered.Contains(unit)) {
                    UnitSelectionManager.Instance.SelectUnit(unit, true);
                    unitsHovered.Remove(unit);
                }
            }
        }
    }

}
