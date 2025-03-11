using UnityEngine;

public class Figure1 : MonoBehaviour
{
    public float height = 3f; // ������ ���� �����
    public float speed = 5f; // �������� ����
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool movingUp = true; // �� �������� ����� �����

    void Start()
    {
        startPoint = transform.position; // ��������� ������� �����
        endPoint = startPoint + new Vector3(0, height, 0); // ʳ����� ������� (���������� �����)
    }

    void Update()
    {
        MovePlate();
    }

    void MovePlate()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
            if (transform.position == endPoint)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);
            if (transform.position == startPoint)
            {
                movingUp = true;
            }
        }
    }
}
