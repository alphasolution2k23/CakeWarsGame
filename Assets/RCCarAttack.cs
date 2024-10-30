using EmeraldAI;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RCCarAttack : MonoBehaviour
{
    public float destroyTime = 10.0f;
    public float speed = 10f;
    public GameObject blastEffect;
    public float roamRadius = 20f; // Radius for random roaming

    private NavMeshAgent agent;
    private bool reachedTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        // Ensure the RC car is placed correctly on the NavMesh
        SnapToNavMesh();

        StartRoaming();
        StartCoroutine(BurstAndDestroy());
    }

    // Snap the RC car to the NavMesh if it's slightly off
    void SnapToNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;  // Adjust position to the closest valid NavMesh point
        }
        else
        {
            Debug.LogWarning("RC car could not find a valid NavMesh position!");
        }
    }

    void StartRoaming()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
    }

    void Update()
    {
        if (!reachedTarget && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartRoaming();
        }
    }

    private IEnumerator BurstAndDestroy()
    {
        yield return new WaitForSeconds(destroyTime);

        Instantiate(blastEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<EmeraldAISystem>().Damage(10);
            Destroy(this.gameObject); // Destroy on enemy hit
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject); // Destroy on wall hit
        }
    }
}
