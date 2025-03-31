using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab; // Префаб коробки
    public Transform spawnPoint; // Місце спавну
    public KeyCode spawnKey = KeyCode.Space; // Клавіша спавну

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            Instantiate(boxPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
