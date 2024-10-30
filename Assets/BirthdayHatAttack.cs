using EmeraldAI;
using GogoGaga.TME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayHatAttack : MonoBehaviour
{
    [SerializeField] LeantweenCustomAnimator customAnim1;
    [SerializeField] LeantweenCustomAnimator customAnim2;

    private void Awake()
    {
        customAnim1.start_vector = new Vector3(transform.position.x, customAnim1.start_vector.y, transform.position.z);
        customAnim1.end_vector = new Vector3(transform.position.x, customAnim1.end_vector.y, transform.position.z);
        customAnim2.start_vector = new Vector3(transform.position.x, customAnim2.start_vector.y, transform.position.z);
        customAnim2.end_vector = new Vector3(transform.position.x, customAnim2.end_vector.y, transform.position.z);

        customAnim1.PlayAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<EmeraldAISystem>().Damage(1000);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
