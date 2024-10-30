using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootDelay;

    [SerializeField] private float countdown = 20f;

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isWalk;

    public GameObject cupcakePrefab;
    public Transform bulletSpawnPoint;
    

    private void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        animator = GetComponent<Animator>();
        StartCoroutine(SetWalk());
        InvokeRepeating("Attack",1f,Random.Range(4f,8f));
    }
    private void Update()
    {
        if (isWalk) 
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Destroy(gameObject);

            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        }
    }
    IEnumerator SetWalk() 
    {
        animator.SetBool("isWalk",true);
        yield return new WaitForSeconds(0.5f);
        isWalk = true;
    } 
    public void Attack() 
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");

        StartCoroutine(ShootCupcake());
    }

    IEnumerator ShootCupcake()
    {
        yield return new WaitForSeconds(shootDelay);
        GameObject cupcake = Instantiate(cupcakePrefab, bulletSpawnPoint.position, Quaternion.identity);
        cupcake.GetComponent<ProjectileNew>().shootByEnemy = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cake") && !collision.gameObject.GetComponent<ProjectileNew>().shootByEnemy)
        {            
            Destroy(collision.gameObject);
        }
    }
}