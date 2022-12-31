using UnityEngine;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    [Header("Movement Settings")]
    public AnimationCurve speedCurve;
    public float speedControll;
    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Animation Settings")]
    public Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetKey(W)){
            movement = Vector2.up;
        }

        if (Input.GetKey(A))
        {
            movement = Vector2.left;
        }

        if (Input.GetKey(S))
        {
            movement = Vector2.down;
        }

        if (Input.GetKey(D))
        {
            movement = Vector2.right;
        }

        speed = speedCurve.Evaluate(Time.time);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    
    public void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * speed *Time.deltaTime);
    }

    public void RemoveCollectableFromAnimation()
    {
        animator.SetBool("Collected", false);
    }
}