using UnityEngine;

public class Figure2 : MonoBehaviour
{
    public float swingAngle = 45f;
    public float speed = 2f;

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * swingAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
