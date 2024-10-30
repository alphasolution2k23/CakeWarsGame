using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class TeddyAttack : MonoBehaviour
{
    public float moveStartTime = 2.0f;
    public float time = 0f;

    void Update()
    {
        time += Time.deltaTime;
        if (time > moveStartTime)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 10f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("enemy"))
        {    
            other.gameObject.GetComponent<EmeraldAISystem>().Damage(1000);       
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            this.GetComponent<PlayConfatee>().EnableConfatee();
            Destroy(this.gameObject);
        }
    }


}
