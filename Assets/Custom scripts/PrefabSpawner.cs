using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign your prefab in the Inspector
    public Transform spawnPoint;    // Optional: Define a specific spawn location
    public Vector3 spawnOffset = Vector3.zero; // Offset from the spawnPoint or Spawner

    public void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // Determine the spawn position
            Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            position += spawnOffset;

            // Instantiate the prefab
            Instantiate(prefabToSpawn, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab not assigned!");
        }
    }
}
