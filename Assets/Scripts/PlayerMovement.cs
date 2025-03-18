using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Швидкість руху вперед
    public float sideSpeed = 4f; // Швидкість руху в сторони
    public float jumpForce = 7f; // Сила стрибка
    public float accelerationMultiplier = 2f; // Прискорення
    public float accelerationTime = 2f; // Час дії прискорення

    private CharacterController controller;
    private Vector3 moveDirection;
    private float accelerationTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Рух вперед
        float currentSpeed = speed;
        if (accelerationTimer > 0)
        {
            currentSpeed *= accelerationMultiplier;
            accelerationTimer -= Time.deltaTime;
        }

        moveDirection.z = currentSpeed;

        // Рух вліво-вправо
        float horizontalInput = Input.GetAxis("Horizontal"); // Стрілки або A/D
        moveDirection.x = horizontalInput * sideSpeed;

        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        // Гравітація
        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        // Застосування руху
        controller.Move(moveDirection * Time.deltaTime);

        // Прискорення
        if (Input.GetKeyDown(KeyCode.LeftShift) && accelerationTimer <= 0)
        {
            accelerationTimer = accelerationTime;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Гравець зіткнувся з перешкодою!");
            speed = 0; // Зупинка при зіткненні
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Гравець досяг фінішу!");
        }
    }
}
