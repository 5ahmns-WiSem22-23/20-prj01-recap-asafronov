using UnityEngine;

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
    public AnimationCurve movementCurve;
    private Rigidbody2D rb;

    [Header("Animation Settings")]
    private Animator animator;
    private float currentSpeed;
    private Vector2 previousPosition;
    private Vector2 currentPosition;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        Vector2 movementDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movementDirection += Vector2.up;
        if (Input.GetKey(KeyCode.A)) movementDirection += Vector2.left;
        if (Input.GetKey(KeyCode.S)) movementDirection += Vector2.down;
        if (Input.GetKey(KeyCode.D)) movementDirection += Vector2.right;

        if (movementDirection.magnitude > 1f) movementDirection.Normalize();

        Debug.Log(movementDirection.magnitude);

        Vector2 playerTransform = new Vector2(rb.transform.position.x, rb.transform.position.y);

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction and magnitude based on the input and the curve
        //Vector3 movement = new Vector3(horizontal, vertical, 0f);
        //float magnitude = movement.magnitude;
        //movement = movement.normalized * movementCurve.Evaluate(magnitude) * speed;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.MovePosition(playerTransform + speed * Time.deltaTime * movementDirection);

        // Move the player in the direction specified by the input
        //transform.position = transform.position + movement * Time.deltaTime;
    }

    public void LateUpdate()
    {
        Debug.Log("Velocity of Rigidbody: " + rb.velocity.magnitude);
    }

    private void Update()
    {
        currentPosition = rb.transform.position;
        Vector2 positionDifference = currentPosition - previousPosition;

        currentSpeed = positionDifference.magnitude / Time.deltaTime;

        previousPosition = currentPosition;

        Debug.Log("Current Speed: " + currentSpeed);
        Debug.Log("Current Position: " + currentPosition + " | Previous Position: " + previousPosition);

        animator.SetFloat("Speed", currentSpeed);
    }
}