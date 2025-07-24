using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour // <- ĐÚNG TÊN CLASS, viết hoa y hệt file
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}