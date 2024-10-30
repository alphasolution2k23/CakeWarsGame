using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticSoldierBullet : MonoBehaviour
{
    public float moveStartTime = 2.0f;

    void Update()
    {
        transform.Translate(-1 * transform.forward * Time.deltaTime * 100f);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<EmeraldAISystem>().Damage(1);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}
