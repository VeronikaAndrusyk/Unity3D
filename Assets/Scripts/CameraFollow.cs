using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;// посилання на об'єкт за яким стежить камера
    public Vector3 offset = new Vector3(0, 3, -5);// відстань між камерою і гравцем

    void LateUpdate()//після всіх оновлень фізики
    {
        transform.position = target.position + offset;
    }
}
