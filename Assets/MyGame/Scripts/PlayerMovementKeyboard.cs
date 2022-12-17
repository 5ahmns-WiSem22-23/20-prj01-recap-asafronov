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

    public void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction and magnitude based on the input and the curve
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        float magnitude = movement.magnitude;
        movement = movement.normalized * movementCurve.Evaluate(magnitude) * speed;

        // Move the player in the direction specified by the input
        transform.position = transform.position + movement * Time.deltaTime;
    }
}