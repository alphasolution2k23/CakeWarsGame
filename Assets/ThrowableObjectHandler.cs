using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class ThrowableObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject ObjectModelEffect;
    [SerializeField] private GameObject effect2;
    [SerializeField] private BoxCollider ObjectModelColl;
    [SerializeField] private bool Roses;
    private GameObject ComponentFound;
    public GameObject blastObject;
    private bool Hit = false;
    public enum ObjectToThrow
    {
        Phone,
        Roses,
        PoisionousChocklate,
        ExplosiveCake,
        FrostyCocktail,
        PearlNecklace,
        ClownSurpriseMusicBox,
        Baloons,
        BirthdayCakePlates,
        Pinata
    }
    public ObjectToThrow objectToThrow;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            if (objectToThrow == ObjectToThrow.Phone)
            {
                ObjectModelEffect.SetActive(true);
                // PlayerPrefs.SetInt("DaneceAbilityActive", 1);
                ObjectModelColl.enabled = true;

                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<AudioSource>().Play();
                Invoke(nameof(DisableIt), 30f);
            }
            else if (objectToThrow == ObjectToThrow.FrostyCocktail)
            {
                ObjectModelEffect.SetActive(true);

                ObjectModelColl.enabled = true;
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Invoke(nameof(DisableIt), 10.0f);
            }
            else if (objectToThrow == ObjectToThrow.Roses)
            {
                ObjectModelEffect.SetActive(true);
                ObjectModelColl.enabled = true;
                // Set rotation on the X-axis
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Invoke("DisableIt", 7f);
            }
            else if (objectToThrow == ObjectToThrow.PoisionousChocklate)

            {

                ObjectModelEffect.SetActive(true);
                ObjectModelColl.enabled = true;
                // Set rotation on the X-axis
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Invoke("DisableChocklate", 5.0f);
            }
            else if (objectToThrow == ObjectToThrow.ExplosiveCake)
            {
                ObjectModelEffect.SetActive(true);
                ObjectModelEffect.transform.parent = null;
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(this.gameObject);
            }
            else if (objectToThrow == ObjectToThrow.Baloons)
            {
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                ComponentFound = null;
                StartCoroutine(GrowAndDestroy());
            }
            else if (objectToThrow == ObjectToThrow.PearlNecklace)
            {
                ObjectModelEffect.SetActive(true);
                ObjectModelEffect.transform.parent = null;
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(this.gameObject);
            }
            else if (objectToThrow == ObjectToThrow.BirthdayCakePlates)
            {
                ObjectModelEffect.SetActive(true);
                ObjectModelEffect.transform.parent = null;
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(this.gameObject);
            }
            else if (objectToThrow == ObjectToThrow.ClownSurpriseMusicBox)
            {
                ObjectModelEffect.SetActive(true);

                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                PlayerPrefs.SetInt("DaneceAbilityActive", 1);

                Invoke(nameof(DisableClownSurpriseMusicBox), 15f);
            }
            else if (objectToThrow == ObjectToThrow.Pinata)
            {
                transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                // Destroy(this.gameObject);
                ObjectModelEffect.SetActive(true);
                Invoke(nameof(DisablePinata), 6);
            }

        }

        if (other.gameObject.CompareTag("enemy") && !Hit)
        {
            if (objectToThrow == ObjectToThrow.PoisionousChocklate)

            {
                //  Hit = true;
                Debug.Log("EnemyHit" + other.gameObject.name);
                // this.GetComponent<SphereCollider>().enabled = false;
                ComponentFound = other.gameObject;
                InvokeRepeating(nameof(SendDamage), 0.1f, 1f);
            }
            else if (objectToThrow == ObjectToThrow.BirthdayCakePlates)
            {
                Debug.Log("EnemyHit" + other.gameObject.name);
                if (other.GetComponent<EmeraldAISystem>())
                {
                    ObjectModelEffect.SetActive(true);
                    ObjectModelEffect.transform.parent = null;
                    if (other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                    {
                        other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().GetComponent<MeshExploder>().Explode();
                    }
                    ComponentFound = other.gameObject;
                    SendDamage();
                }
            }

            else if (objectToThrow == ObjectToThrow.Baloons)

            {
                Hit = true;
                Debug.Log("EnemyHit" + other.gameObject.name);
                transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                StartCoroutine(GrowAndDestroy());
                ComponentFound = other.gameObject;
                // this.GetComponent<SphereCollider>().enabled = false;

            }
            else if (objectToThrow == ObjectToThrow.FrostyCocktail)
            {
                //Hit = true;
                //Debug.Log("EnemyHit" + other.gameObject.name);
                //if (other.GetComponent<EmeraldAISystem>())
                //{
                //    Debug.Log("FrostyCocktailHit");
                //    other.GetComponent<EmeraldAISystem>().EnableCocktale();

                //}
                //ObjectModelEffect.SetActive(true);
                //ComponentFound = other.gameObject;

                //Invoke(nameof(DisableFrostyCocktail), 5f);
            }
        }
    }
    private float growthRate = 0.3f;
    private float maxSize = 3f;
    private float lifetime = 2.0f;
    private bool isGrowing = true;

    private IEnumerator GrowAndDestroy()
    {
        float startTime = Time.time;
        Vector3 originalScale = transform.localScale;

        while (isGrowing && transform.localScale.x < maxSize)
        {
            // Calculate 
            float scaleIncrement = growthRate * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
            // Move the ballo
            transform.localPosition += Vector3.up * 1f * Time.deltaTime;

            // Stop growing 
            if (transform.localScale.x >= maxSize)
            {
                isGrowing = false;
            }

            // Destroy
            if (Time.time - startTime >= lifetime)
            {
                if (ComponentFound != null)
                {
                    // Destroy the balloon after it stops growing
                    if (ComponentFound.GetComponent<EmeraldAISystem>())
                    {
                        Debug.Log("SendingDamage");
                        ObjectModelEffect.SetActive(true);

                        ObjectModelEffect.transform.parent = null;

                        SendDamage();
                        // other.gameObject.GetComponent<EmeraldAISystem>().Damage(30);
                        Destroy(this.gameObject);

                    }
                }
                else
                {
                    ObjectModelEffect.SetActive(true);
                    ObjectModelEffect.transform.parent = null;
                    Destroy(this.gameObject);
                }
                yield break;
            }

            yield return null;
        }
        if (ComponentFound != null)
        {
            // Destroy the balloon after it stops growing
            if (ComponentFound.GetComponent<EmeraldAISystem>())
            {
                ObjectModelEffect.SetActive(true);

                ObjectModelEffect.transform.parent = null;

                SendDamage();
                Debug.Log("Destroy Object");
                Destroy(this.gameObject);

            }
            {
                ObjectModelEffect.SetActive(true);
                ObjectModelEffect.transform.parent = null;
                Destroy(this.gameObject);
            }
        }

    }
    public void DisableClownSurpriseMusicBox()
    {
        PlayerPrefs.SetInt("DaneceAbilityActive", 0);
        Instantiate(blastObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void DisablePinata()
    {
        effect2.transform.parent = null;
        effect2.SetActive(true);
        Destroy(this.gameObject);
    }

    GameObject PearlFound;
    public void DisableP()
    {
        Hit = false;
        // PearlFound.SetActive(false);
        if (ComponentFound.GetComponent<EmeraldAISystem>())
        {
            Debug.Log("DisablePearl");
            ComponentFound.GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.Active;
            ComponentFound.GetComponent<EmeraldAISystem>().IsMoving = true;
        }
        if (ComponentFound.GetComponent<PearlLocator>())
        {
            Debug.Log("DisablePearl");
            ComponentFound.GetComponent<PearlLocator>().Pearl.SetActive(false);

        }
        ComponentFound.GetComponent<EmeraldAISystem>().BackToCombat();
    }
    public GameObject FindChildByName(GameObject parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true); // true to include inactive children

        foreach (Transform child in children)
        {
            if (child.gameObject.name == childName)
            {
                return child.gameObject;
            }
        }

        return null; // Return null if no child with the specified name is found
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("enemy"))
        //{
        //    if (Roses)
        //    {
        //        if (other.GetComponent<EmeraldAISystem>())
        //        {
        //            other.GetComponent<EmeraldAISystem>().ChangeToOldFaction();
        //        }
        //    }
        //}
    }

    public void DisableSmartPhone()
    {
        Hit = false;
        //  PlayerPrefs.SetInt("DaneceAbilityActive", 0);
        if (ComponentFound.GetComponent<EmeraldAISystem>())
        {
            Debug.Log("ChangingFaction");
            ComponentFound.GetComponent<EmeraldAISystem>().DisableSmartPhone();

        }
        Destroy(this.gameObject);
    }
    public void DisableFrostyCocktail()
    {
        Hit = false;
        //  PlayerPrefs.SetInt("FreezAbilityActive", 0);
        if (ComponentFound.GetComponent<EmeraldAISystem>())
        {
            Debug.Log("ChangingFaction");
            ComponentFound.GetComponent<EmeraldAISystem>().BackToCombat();

        }
        Destroy(this.gameObject);
    }

    public void DisableIt()
    {
        Hit = false;
        Destroy(this.gameObject);
    }
    public void DisableChocklate()
    {
        Hit = false;
        CancelInvoke(nameof(SendDamage));
        Destroy(this.gameObject);
    }
    public void DisableCake()
    {
        Hit = false;
        Destroy(this.gameObject);
    }

    public void SendDamage()
    {

        switch (GameManager.instance.currentWeapon)
        {
            case CurrentWeapon.CupCake:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;
            case CurrentWeapon.SmartPhone:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);
                        }
                    }
                }
                break;
            case CurrentWeapon.Roses:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;
            case CurrentWeapon.PoisonousChocolate:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;
            case CurrentWeapon.ExplosiveCake:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;

            case CurrentWeapon.PearlNecklace:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;
            case CurrentWeapon.Baloons:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;
            case CurrentWeapon.BirthdayCakePlates:
                GameManager.instance.shootingController.lastShotTime = 0f;
                foreach (var item in GameManager.instance.weaponHandler.Weapon)
                {
                    if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
                    {
                        if (ComponentFound.GetComponent<EmeraldAISystem>())
                        {
                            Debug.Log("SendingDamage");
                            ComponentFound.GetComponent<EmeraldAISystem>().Damage(item.damage);

                        }

                    }
                }
                break;

        }
    }

    public float rotationSpeedY = 45.0f;

    void Update()
    {
        ///float rotationY = rotationSpeedY * Time.deltaTime;
        //  transform.Rotate(0, 0, rotationY);
    }
}
