using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class BeachBallController : MonoBehaviour
{
    public bool hasHitGround = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        //StartCoroutine(DropAndRoll());
    }
    //private IEnumerator DropAndRoll()
    //{
    //    // Wait until the ball hits the ground
    //  //  yield return new WaitUntil(() => this.GetComponent<Rigidbody>().velocity.y == 0);

    //    // Start rolling the ball in a straight line
    //    this.GetComponent<Rigidbody>().velocity = new Vector3(10f, 0, 0);
    //    yield
    //    // Alternatively, you can use AddForce for more control over the rolling behavior
    //    // ball.GetComponent<Rigidbody>().AddForce(new Vector3(rollSpeed, 0, 0), ForceMode.VelocityChange);
    //}
    void Update()
    {
        if (hasHitGround)
        {
            // Move the ball forward
            this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x, this.GetComponent<Rigidbody>().velocity.y, -10f);
        }
    }


  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            this.GetComponent<PlayConfatee>().EnableConfatee();
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EmeraldAISystem>().Damage(1000);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasHitGround = true;
        }
      
    }
}
