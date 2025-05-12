using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public float spawnDistance = 10f;//відстань перед гравцем
    public float spawnInterval = 20f;//створенн перешкоди інтервал

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 4f, spawnInterval);//викликає функцію одразу а потім через кожен інтервал
    }

    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 1, player.position.z + spawnDistance);//до z гравця додаємо spawnDic
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);//стуорюємо перешкоду, копія об'єкта, розміщення, без обертання
    }
}
