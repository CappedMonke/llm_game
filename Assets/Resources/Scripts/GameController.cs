using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; set; }

    public bool isPlaying = false;
    public bool isMapReady = false;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update() {
        if (!isPlaying) {
            CheckIfReady();
        }
    }

    private void CheckIfReady() {
        if (!isMapReady) return;
        isPlaying = true;
        ActivateWorld();
    }

    private void ActivateWorld() {
        GameObject.Find("UI").transform.Find("UnitSelectionUI").gameObject.SetActive(true);
    }
}
