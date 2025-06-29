using UnityEngine;

public class MainMenuCameraOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform target;
    public float radius = 50f;
    public float baseHeight = 30f;
    public float orbitSpeed = 10f;

    [Header("Wobble Settings")]
    public float wobbleAmplitude = 2f;
    public float wobbleFrequency = 1f;

    private float currentAngle = 0f;
    private float timeCounter = 0f;

    void Update()
    {
        if (target == null) return;

        timeCounter += Time.deltaTime;

        currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle > 360f) currentAngle -= 360f;

        float rad = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;

        float y = baseHeight + Mathf.Sin(timeCounter * wobbleFrequency * 2 * Mathf.PI) * wobbleAmplitude;

        Vector3 newPosition = new Vector3(x, y, z) + target.position;
        transform.position = newPosition;

        transform.LookAt(target.position);
    }
}
