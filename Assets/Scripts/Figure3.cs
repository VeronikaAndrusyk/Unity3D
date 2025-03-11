using UnityEngine;

public class Figure3 : MonoBehaviour
{
    public float speed = 2f; // Ўвидк≥сть руху
    public float radiusGrowth = 0.01f; // Ўвидк≥сть зб≥льшенн€ рад≥усу
    private float angle = 0f;
    private float radius = 0f;

    void Update()
    {
        angle += speed * Time.deltaTime; // ќбертанн€
        radius += radiusGrowth * Time.deltaTime; // «б≥льшенн€ рад≥усу

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
