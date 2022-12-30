using UnityEngine;
using UnityEngine.Events;

public class Collectables : MonoBehaviour
{
    private Transform playerTransform;
    private Gamelogic gamelogic;
    private PlayerMovementKeyboard player;

    public float pickUpableDistance;
    public UnityEvent whenOnPickUp;

    private void Awake()
    {
        gamelogic = FindObjectOfType<Gamelogic>();
        playerTransform = FindObjectOfType<PlayerMovementKeyboard>().transform;
        player = FindObjectOfType<PlayerMovementKeyboard>();
    }

    public void Update()
    {
        float distance = Vector2.Distance(playerTransform.transform.position, transform.position);

        if (distance <= pickUpableDistance)
        {
            whenOnPickUp.Invoke();
            Destroy(gameObject);
            //needs refinement. How to make events for Prefabs?
            gamelogic.canCollect = false;

            player.animator.SetBool("Collected", true);
        }
    }
}