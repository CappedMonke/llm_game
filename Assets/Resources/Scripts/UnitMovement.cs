using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour, IMovable {
    public NavMeshAgent agent { get; private set; }

    public void SetDestination(RaycastHit target) {
        agent.SetDestination(target.point);
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        
    }
}
