using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class ProjectileShooter : MonoBehaviour
{
    //public bool isMoving = false;
    public GameObject DestroyedObject; // Speed of the projectile
    //public Vector3 TargetPos;
    bool Hit = false;
    void Start()
    {

    }
    private void Update()
    {
        //if (isMoving)
        //{
        //    MoveProjectile();
        //}

    }
    //void MoveProjectile()
    //{

    //    //// Calculate the step size based on speed and frame time
    //    //float step = projectileSpeed * Time.deltaTime;

    //    //// Move the projectile towards the target position
    //    //this.transform.position = Vector3.MoveTowards(this.transform.position, TargetPos, step);

    //    //// Check if the projectile has reached the target position
    //    //if (Vector3.Distance(this.transform.position, TargetPos) < 0.001f)
    //    //{
    //    //    isMoving = false;
    //    //    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //}
    //}
    private bool EnemyHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && !Hit)
        {
            EnemyHit = true;
            switch (GameManager.instance.currentWeapon)
            {
                case CurrentWeapon.MagicKit:
                    if (other.GetComponent<EmeraldAISystem>())
                    {
                        Debug.Log("SendingDamage");

                        SendDamage(other.gameObject);
                        GameObject DestroyedObj = Instantiate(DestroyedObject, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), Quaternion.identity);
                        // other.gameObject.GetComponent<EmeraldAISystem>().Damage(30);
                        Destroy(this.gameObject);

                    }
                    break;
                case CurrentWeapon.BubbleCannon:
                    if (other.GetComponent<PearlLocator>())
                    {
                        DestroyedObject.SetActive(true);
                        DestroyedObject.transform.parent = null;
                        other.GetComponent<EmeraldAISystem>().IsMoving = false;
                        //other.GetComponent<Rigidbody>().useGravity = false;
                        //other.GetComponent<Rigidbody>().isKinematic = true;
                        
                        other.GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.NotActive;
                        other.GetComponent<EmeraldAISystem>().EnableCocktale();
                        other.GetComponent<EmeraldAISystem>().enabled = false;
                        other.GetComponent<PearlLocator>().Bubble.SetActive(true);
                        StartCoroutine(MoveUpward(other.gameObject));
                        DisableAllChildObjects(transform);
                    }
                    break;

            }
          
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            


            switch (GameManager.instance.currentWeapon)
            {
                case CurrentWeapon.MagicKit:
                    Destroy(this.gameObject);
                    break;
                case CurrentWeapon.BubbleCannon:
                    DestroyedObject.SetActive(true);
                    DestroyedObject.transform.parent = null;
                    if (!EnemyHit)
                    {
                        Destroy(this.gameObject);
                    }
                  
                    break;

            }

        }
    }
    void DisableAllChildObjects(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
            // Recursively call this method to disable all subchildren
            DisableAllChildObjects(child);
        }
    }
    private IEnumerator MoveUpward(GameObject obj)
    {

        Debug.Log("MovingBack");
        float moveDuration = 4f; // Duration to move backward
        float elapsedTime = 0.0f;

        while (elapsedTime < moveDuration)
        {
            // Move the object backward
            obj.transform.Translate(Vector3.up * 10f * Time.deltaTime);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Yield execution of this coroutine and return to the main loop until the next frame
            yield return null;
        }
        obj.GetComponent<EmeraldAISystem>().enabled = true;
        obj.GetComponent<EmeraldAISystem>().IsMoving = false;
        obj.GetComponent<EmeraldAISystem>().Damage(1000);

        obj.GetComponent<PearlLocator>().Bubble.SetActive(false);
        Destroy(this.gameObject);
    }
    public void SendDamage(GameObject Obj)
    {

        switch (GameManager.instance.currentWeapon)
        {
            case CurrentWeapon.MagicKit:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (Obj.GetComponent<EmeraldAISystem>())
                        {
                            Obj.GetComponent<EmeraldAISystem>().Damage(item.damage);
                        }
                    }
                }
                break;
            case CurrentWeapon.BubbleCannon:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (Obj.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("Sending Bubble Damage");
                           // Obj.GetComponent<EmeraldAISystem>().CombatStateRef = Com
                        }
                    }
                }
                break;

        }
    }

}
