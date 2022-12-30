using UnityEngine;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    [Header("Movement Settings")]
    public float speed;
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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
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