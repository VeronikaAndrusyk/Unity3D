using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab; // ������ �������
    public Transform spawnPoint; // ̳��� ������
    public KeyCode spawnKey = KeyCode.Space; // ������ ������

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            Instantiate(boxPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
