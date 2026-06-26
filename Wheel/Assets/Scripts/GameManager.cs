using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawn Locations")]
    public GameObject[] spawnPoints;

    [Header("Obstacle Prefabs")]
    public GameObject[] obstaclePrefabs;

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            return;
        }

        foreach (GameObject point in spawnPoints)
        {
            if (point == null) continue;

            // 1. Pick a random prefab
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject chosenPrefab = obstaclePrefabs[randomIndex];

            // 2. Combine coordinates: X and Z from the spawn point, Y from the prefab
            Vector3 spawnPosition = new Vector3(
                point.transform.position.x,
                chosenPrefab.transform.position.y,
                point.transform.position.z
            );

            // 3. Extract the exact rotation from the prefab asset
            Quaternion spawnRotation = chosenPrefab.transform.rotation;

            // 4. Instantiate at the calculated world position and rotation
            GameObject spawnedObstacle = Instantiate(chosenPrefab, spawnPosition, spawnRotation);

            // 5. Keep the hierarchy clean by assigning the parent afterward 
            // (Setting worldPositionStays to true keeps our combined positions intact)
            spawnedObstacle.transform.SetParent(point.transform, true);
        }
    }
}
