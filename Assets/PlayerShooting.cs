using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("FirePoint hoặc BulletPrefab chưa được gán!");
            return;
        }

        // Tạo một tia từ tâm màn hình
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 targetDirection;

        // Nếu tia raycast trúng vật thể thì bắn về đó, ngược lại bắn theo hướng camera
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetDirection = (hit.point - firePoint.position).normalized;
        }
        else
        {
            targetDirection = ray.direction;
        }

        // Tạo đạn và cho bay theo hướng targetDirection
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(targetDirection));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = targetDirection * bulletSpeed;
        }
    }
}