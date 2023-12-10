using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnInterval = 5f;
    public float carSpeed = 5f;
    public float spawnRadius = 10f;
    public int maxCars = 10;
    private List<GameObject> carList = new List<GameObject>();

    private void Start()
    {
        // Spawn initial cars
        for (int i = 0; i < 10; i++)
        {
            SpawnCar();
        }

        // Start spawning cars
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    private void SpawnCar()
    {
        // Check if the maximum number of cars has been reached
        if (carList.Count >= maxCars)
        {
            return;
        }

        // Generate a random position within the spawn area
        Vector3 spawnPosition = transform.position + Random.onUnitSphere * spawnRadius;

        // Ensure the car spawns at a consistent height
        spawnPosition.y = transform.position.y;

        // Instantiate a new car at the random position
        GameObject newCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);

        // Set the parent of the new car to the same parent as the spawner
        newCar.transform.parent = transform.parent;

        // Apply a random rotation to the car
        newCar.transform.Rotate(Vector3.up, Random.Range(0f, 360f));

        // Apply a random force to the car to make it move randomly (only on x and z axes)
        Vector3 randomForce = new Vector3(Random.onUnitSphere.x, 0f, Random.onUnitSphere.z).normalized * carSpeed;
        newCar.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);

        // Add the new car to the list of cars
        carList.Add(newCar);
    }
}
