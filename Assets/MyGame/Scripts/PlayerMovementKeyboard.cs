using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    public KeyCode Shift;

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
        //rb.velocity = rb.velocity.normalized;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    public void LateUpdate()
    {
        Debug.Log("Velocity of Rigidbody: " + rb.velocity.magnitude);
    }

    public void RemoveCollectableFromAnimation()
    {
        animator.SetBool("Collected", false);
    }
}