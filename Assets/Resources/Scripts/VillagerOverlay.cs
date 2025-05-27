using UnityEngine;
using UnityEngine.EventSystems;

public class VillagerOverlay : MonoBehaviour, IBeginDragHandler, IDragHandler {
    private RectTransform rectTransform;
    private Vector2 dragOffset;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(800, -300);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos
        );

        dragOffset = localPointerPos;
    }

    public void OnDrag(PointerEventData eventData) {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint)) {
            rectTransform.anchoredPosition = localPoint - dragOffset;
        }
    }
}
