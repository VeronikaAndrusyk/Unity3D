using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 180f; // �������� ���������
    private Animator anim;
    private bool isCollected = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isCollected)
        {
            // ��������� ������� ����������� ��
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.Instance.data.coinsCollected++;
            Debug.Log("������ ������! ������: " + GameManager.Instance.data.coinsCollected);

            anim.SetTrigger("Jump"); // �������� ������� �������������

            Destroy(gameObject, 0.5f); // ��������� ������ ����� �����
        }
    }
}
