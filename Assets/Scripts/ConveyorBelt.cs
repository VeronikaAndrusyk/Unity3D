using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 2.0f; // �������� ���� �������
    public Vector3 direction = Vector3.forward; // �������� ����

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.linearVelocity = direction * speed;
        }
    }
}
