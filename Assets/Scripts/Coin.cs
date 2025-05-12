using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 180f; // Швидкість обертання
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
            // Обертання навколо вертикальної осі
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.Instance.data.coinsCollected++;
            Debug.Log("Монета зібрана! Всього: " + GameManager.Instance.data.coinsCollected);

            anim.SetTrigger("Jump"); // Стартуємо анімацію підстрибування

            Destroy(gameObject, 0.5f); // Видаляємо монету трохи пізніше
        }
    }
}
