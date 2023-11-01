using UnityEngine;

public class CheckpointPiste : MonoBehaviour
{
    public Transform[] checkpointPositions;
    public float checkpointXLimit = 10f;
    public float checkpointUpdateInterval = 5f;

    private float timer = 0f;

    void Start()
    {
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
            Vector3 newPosition = new Vector3(randomX, checkpointPositions[i].position.y, checkpointPositions[i].position.z);
            checkpointPositions[i].position = newPosition;
        }
    }
}
