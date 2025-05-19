using UnityEngine;

public class Unit : MonoBehaviour {
    void Start() {
        UnitSelectionManager.Instance.units.Add(gameObject);
    }

    private void OnDestroy() {
        UnitSelectionManager.Instance.units.Remove(gameObject);
    }
}
