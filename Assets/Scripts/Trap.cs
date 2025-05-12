using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife(); // �������� �����
            Debug.Log("������� �������� � ������! ����� ����������: " + GameManager.Instance.data.lives);
        }
    }
}
