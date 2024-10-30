using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float gravity = 9.8f;

    public void ShootBullet(Vector3 targetPosition)
    {
        Debug.LogError("pos: "+targetPosition);

        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the horizontal distance to the target
        float horizontalDistance = Vector3.Distance(targetPosition, transform.position);

        // Calculate time to reach target horizontally
        float timeToReachTarget = horizontalDistance / bulletSpeed;

        // Calculate the initial velocity in the horizontal direction
        float initialVelocityX = direction.x * bulletSpeed;

        // Calculate the initial velocity in the vertical direction to follow a parabolic trajectory
        float initialVelocityY = direction.y * bulletSpeed;

        // Calculate the initial velocity in the vertical direction to follow a parabolic trajectory
        float initialVelocityZ = (targetPosition.z - transform.position.z) / timeToReachTarget + (0.5f * gravity * timeToReachTarget) / bulletSpeed;

        // Apply the initial velocity to the bullet
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

        // Apply gravity to the bullet
        rb.useGravity = true;

        // Destroy the bullet after some time to prevent cluttering
        Destroy(gameObject, timeToReachTarget + 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Handle enemy hit
            //Destroy(collision.gameObject);
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
