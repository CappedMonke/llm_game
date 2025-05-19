using UnityEngine;
using UnityEngine.AI;

public abstract class Movement : MonoBehaviour {
    protected NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(RaycastHit target) {
        agent.SetDestination(target.point);
    }
}
