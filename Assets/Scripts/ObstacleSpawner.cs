using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public float spawnDistance = 10f;
    public float spawnInterval = 1f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 0f, spawnInterval);
    }

    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 1, player.position.z + spawnDistance);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
