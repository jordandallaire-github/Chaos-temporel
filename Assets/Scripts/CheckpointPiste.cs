using UnityEngine;

public class CheckpointPiste : MonoBehaviour
{
    public Transform[] checkpointPositions;
    public float checkpointXLimit = 5f; // Change this to 5
    public float checkpointUpdateInterval = 5f;

    private float timer = 0f;
    private Vector3[] originalPositions; // Store the original positions of the checkpoints

    void Start()
    {
        originalPositions = new Vector3[checkpointPositions.Length];
        for (int i = 0; i < checkpointPositions.Length; i++)
        {
            originalPositions[i] = checkpointPositions[i].position;
        }

        UpdateCheckpointPositions();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkpointUpdateInterval)
        {
            UpdateCheckpointPositions();
            timer = 0f;
        }
    }

    void UpdateCheckpointPositions()
    {
        for (int i = 0; i < checkpointPositions.Length; i++)
        {
            float randomX = Random.Range(-checkpointXLimit, checkpointXLimit);
            Vector3 newPosition = originalPositions[i] + new Vector3(randomX, 0, 0); // Add the random displacement to the original position
            checkpointPositions[i].position = newPosition;
        }
    }
}

