using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Renderer[] renderers;
    private Collider[] colliders;

    private void Start()
    {
        currentHealth = maxHealth;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took damage. HP = {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died. Respawn in 3s.");

        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;

        Invoke(nameof(Respawn), 3f);
    }

    void Respawn()
    {
        Debug.Log($"{gameObject.name} respawned.");

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        currentHealth = maxHealth;

        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
    }
}