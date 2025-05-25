using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    private void Awake() {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }
    void Start() {
        UnitSelectionManager.Instance.units.Add(gameObject);
    }

    private void OnDestroy() {
        UnitSelectionManager.Instance.units.Remove(gameObject);
    }
}
