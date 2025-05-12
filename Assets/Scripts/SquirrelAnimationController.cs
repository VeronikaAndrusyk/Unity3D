using UnityEngine;

public class SquirrelAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            animator.SetTrigger("GoTo1");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            animator.SetTrigger("GoTo2");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            animator.SetTrigger("GoTo3");
    }
}
