using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Швидкість руху вперед
    public float sideSpeed = 4f; // Швидкість руху в сторони
    public float jumpForce = 7f; // Сила стрибка
    public float accelerationMultiplier = 2f; // Прискорення
    public float accelerationTime = 4f; // Час дії прискорення
    private bool isGameOver = false; // Чи закінчена гра?


    private CharacterController controller;
    private Vector3 moveDirection;// вектор для збереження напр руху
    private float accelerationTimer = 0f; //тайцмер для прискорення 

    void Start()
    {
        controller = GetComponent<CharacterController>();//кер фізикою гравця для move
    }

    void Update()
    {
        if (isGameOver) return;
        // Рух вперед
        float currentSpeed = speed;
        if (accelerationTimer > 0)
        {
            currentSpeed *= accelerationMultiplier;//збільшення шв
            accelerationTimer -= Time.deltaTime;//таймер зменшується
        }

        moveDirection.z = currentSpeed;

        // Рух вліво-вправо
        float horizontalInput = Input.GetAxis("Horizontal"); // -1A 1D
        moveDirection.x = horizontalInput * sideSpeed;

        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        // Гравітація
        moveDirection.y += Physics.gravity.y * Time.deltaTime;//кожен кадр падає вниз

        // Застосування руху
        controller.Move(moveDirection * Time.deltaTime);//робить рух плавним

        // Прискорення
        if (Input.GetKeyDown(KeyCode.LeftShift) && accelerationTimer <= 0)
        {
            accelerationTimer = accelerationTime;//зап прискор
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Гравець зіткнувся з перешкодою!");
            //speed = 0; // Зупинка при зіткненні
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Гравець досяг фінішу!");
            speed = 0;
        }
    }

    public void StopPlayer()
    {
        isGameOver = true; // Зупиняємо рух
        moveDirection = Vector3.zero;
    }




}
