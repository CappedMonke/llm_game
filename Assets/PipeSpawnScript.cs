using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float maxHeightOffset = 7;
    public float maxHeightDelta = 5;
    private float lastHeight = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        spawnPipe();
    }

    // Update is called once per frame
    void Update() {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else{
            timer = 0;
            spawnPipe();
        }

    }

    void spawnPipe ()
    {
        float lowestPoint = transform.position.y - maxHeightOffset;
        float highestPoint = transform.position.y + maxHeightOffset;
        float newHeight;
        do {
            newHeight = Random.Range(lowestPoint, highestPoint);
        }
        while (Unity.Mathematics.math.abs(lastHeight - newHeight) > maxHeightDelta);

        Instantiate(pipe, new Vector3(transform.position.x, newHeight, -0.1f), transform.rotation);
    }
}
