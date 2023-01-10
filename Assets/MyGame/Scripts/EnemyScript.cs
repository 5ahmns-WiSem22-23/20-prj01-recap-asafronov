using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private PlayerMovementRotation playerScript;

    public Transform player;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        player = FindObjectOfType<PlayerMovementRotation>().transform;
        playerScript = FindObjectOfType<PlayerMovementRotation>();

        SpriteRenderer renderer;
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        navAgent.SetDestination(player.position);

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerScript.health--;
        }
    }
}
