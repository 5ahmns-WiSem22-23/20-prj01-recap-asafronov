using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;

    private Vector2 velocity;

    void LateUpdate()
    {
        Vector2 targetPosition = new Vector2(
            xCurve.Evaluate(player.position.x),
            yCurve.Evaluate(player.position.y)
        );

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime);
    }
}