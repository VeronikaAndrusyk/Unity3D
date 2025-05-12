using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife(); // Втрачаємо життя
            Debug.Log("Гравець потрапив у пастку! Життів залишилось: " + GameManager.Instance.data.lives);
        }
    }
}
