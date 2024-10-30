using UnityEngine;

public class CupcakeLauncher : MonoBehaviour
{
    public GameObject cupcakePrefab;
    public Transform target;
    public float launchSpeed = 10f;
    public float gravity = 9.81f;
    public Transform bulletSpawnPoint;

    public float throwHeight = 5f; // Height of the arc
    public float throwDuration = 2f; // Time taken to reach the target
    public LayerMask collisionMask;
    public Animator handAnim;
    void Update()
    {
        if (ControlFreak2.CF2Input.GetButtonDown("Fire1")) // Example: Fire1 is the left mouse button
        {
            //handAnim.ResetTrigger("Attack");
            //handAnim.SetTrigger("Attack");
            //Invoke("LaunchCupcake", 0.25f);
            LaunchCupcake();
        }

        if(ControlFreak2.CF2Input.touchCount > 0)
        {
            foreach (ControlFreak2.InputRig.Touch touch in ControlFreak2.CF2Input.touches)
            {
                // Check if the touch phase is just began
                if (touch.phase == TouchPhase.Began)
                {
                    // Call LaunchCupcake function when touch is detected
                    LaunchCupcake();
                }
            }
        }
    }

    void LaunchCupcake()
    {

        GameObject cupcake = Instantiate(cupcakePrefab, bulletSpawnPoint.position, Quaternion.identity);
       
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("ground") )
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}

}
