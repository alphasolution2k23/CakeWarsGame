using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private void Update()
    {
        // Example: Shoot bullet when left mouse button is clicked
        if (ControlFreak2.CF2Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        // Instantiate a bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        // Get the BulletController component and shoot it towards the target position (enemy if present, otherwise mouse position)
        bullet.GetComponent<BulletController>().ShootBullet(GetTargetPosition());
    }

    private Vector3 GetTargetPosition()
    {
        // Check if there are enemies present
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length > 0)
        {
            GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
            return randomEnemy.transform.position;
        }
        else
        {
            // If no enemies are present, target the position of the mouse click in world coordinates
            Ray ray = Camera.main.ScreenPointToRay(ControlFreak2.CF2Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            return Vector3.zero; // Default to origin if no target found
        }
    }
}
