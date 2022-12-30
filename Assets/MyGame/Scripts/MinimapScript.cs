using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.position = player.position + new Vector3(0f, 0f, -10f);
    }
}