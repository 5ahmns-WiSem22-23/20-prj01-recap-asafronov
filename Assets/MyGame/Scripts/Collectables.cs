using UnityEngine;
using UnityEngine.Events;

public class Collectables : MonoBehaviour
{
    private Transform player;
    private Gamelogic gamelogic;

    public float pickUpableDistance;
    public UnityEvent whenOnPickUp;

    private void Awake()
    {
        gamelogic = FindObjectOfType<Gamelogic>();
        player = FindObjectOfType<PlayerMovementKeyboard>().transform;
    }

    public void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= pickUpableDistance)
        {
            whenOnPickUp.Invoke();
            Destroy(this.gameObject);
            //needs refinement. How to make events for Prefabs?
            gamelogic.canCollect = false;
        }
    }
}