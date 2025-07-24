using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawn : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer[] renderers;
    private Collider[] colliders;
    private EnemyAI enemyAI;
    private NavMeshAgent agent;

    public float respawnDelay = 3f;

    void Start()
    {
        // Lưu trạng thái ban đầu
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        enemyAI = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} died, starting respawn...");

        // Ẩn enemy
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;
        if (enemyAI != null) enemyAI.enabled = false;
        if (agent != null) agent.enabled = false;

        // Hẹn giờ hồi sinh
        Invoke(nameof(Respawn), respawnDelay);
    }

    void Respawn()
    {
        Debug.Log($"{gameObject.name} respawned.");

        // Đặt lại vị trí & hướng
        if (agent != null)
        {
            agent.enabled = true;
            agent.Warp(originalPosition); // Đặt lại vị trí hợp lệ trên NavMesh
        }
        else
        {
            transform.position = originalPosition;
        }

        transform.rotation = originalRotation;

        // Bật lại thành phần
        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
        if (enemyAI != null) enemyAI.enabled = true;
    }
}