using UnityEngine;
using UnityEngine.Events;

public class Collectables : MonoBehaviour
{
    private PlayerMovementRotation player2;

    public float pickUpableDistance;
    public UnityEvent whenOnPickUp;

    private void Awake()
    {
        player2 = FindObjectOfType<PlayerMovementRotation>();
    }

    public void Update()
    {
        float distance2 = Vector2.Distance(player2.transform.position, transform.position);

        if (distance2 <= pickUpableDistance)
        {
            whenOnPickUp.Invoke();
            Destroy(gameObject);
            Collectable.canCollect = false;
        }
    }
}