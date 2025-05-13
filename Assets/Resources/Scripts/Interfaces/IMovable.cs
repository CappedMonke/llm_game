using UnityEngine;
using UnityEngine.AI;

public interface IMovable {
    public NavMeshAgent agent {get; }
    public void SetDestination(RaycastHit target);
}
