using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Transform player;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        player = FindObjectOfType<PlayerMovementKeyboard>().transform;
    }

    private void Update()
    {
        navAgent.SetDestination(player.position);
    }
}
