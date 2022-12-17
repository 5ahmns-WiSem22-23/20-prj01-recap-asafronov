using UnityEngine;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    public KeyCode Shift;

    [Header("Speed")]
    public float speed;

    public void FixedUpdate()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        if (Input.GetKey(W)) dir.y += 1;
        if (Input.GetKey(A)) dir.x += -1;
        if (Input.GetKey(S)) dir.y += -1;
        if (Input.GetKey(D)) dir.x += 1;

        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}