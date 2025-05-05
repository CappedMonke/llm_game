using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdScript : MonoBehaviour {
    public Rigidbody2D rigidBody2D;
    public float flapStrength;
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rigidBody2D.linearVelocity = rigidBody2D.linearVelocity + Vector2.up * flapStrength;
        }
        float speed = rigidBody2D.linearVelocity.y;
        
        transform.eulerAngles = new Vector3(0, 0, speed * 1.5f);
    }
}
