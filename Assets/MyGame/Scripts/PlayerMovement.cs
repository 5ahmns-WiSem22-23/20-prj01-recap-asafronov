using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    [Header("Movement Settings")]
    public AnimationCurve speedCurve;
    public float speedControll;
    public float elapsedTime;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;


    [Header("Animation Settings")]
    public string IDLE;
    public string IDLE_PRESENT;
    public string RUNNING;
    public string RUNNING_PRESENT;

    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        movement = new Vector2(Input.GetKey(D) ? 1 : Input.GetKey(A) ? -1 : 0, Input.GetKey(W) ? 1 : Input.GetKey(S) ? -1 : 0);

        transform.localEulerAngles = new Vector3(0f, 0f, movement.x == 1 ? 90f : movement.x == -1 ? -90f : movement.y == -1 ? 0f : 180f);

        animator.Play(movement != Vector2.zero ? Collectable.canCollect ? RUNNING : RUNNING_PRESENT : Collectable.canCollect ? IDLE : IDLE_PRESENT);

        if (movement == new Vector2(0f, 0f))
        {
            elapsedTime = 0f;
        }
        else elapsedTime += Time.deltaTime;

        speed = speedCurve.Evaluate(elapsedTime) * speedControll;
    }

    public void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    public void RemoveCollectableFromAnimation()
    {
        animator.SetBool("Collected", false);
    }
}