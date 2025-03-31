using UnityEngine;

public class VectorCrossProduct : MonoBehaviour
{
    void Start()
    {
        // Вектори
        Vector3 a = new Vector3(1, 3, 4);
        Vector3 b = new Vector3(-2, 2, -6);

        // Векторний добуток
        Vector3 crossProduct = Vector3.Cross(a, b);

        // Довжина векторного добутку
        float magnitude = crossProduct.magnitude;

        // Виведення результату
        Debug.Log("Векторний добуток: " + crossProduct);
        Debug.Log("Довжина векторного добутку: " + magnitude);
    }
}
