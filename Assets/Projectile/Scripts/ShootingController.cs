using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour 
{
    [SerializeField]
    Transform handBase;

    [SerializeField]
    public Transform hand;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    Transform smokePuffPoint;

    [SerializeField]
    public Animator anim;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    GameObject EffectShoot;

    [SerializeField] public 
      List<GameObject> WeaponsThrowPrefabs;
    public GameObject WeapomObj;

   

    [SerializeField]
    public float cooldown = 1;

    public float currentSpeed;
    private float currentAngle;
    private float currentTimeOfFlight;

    public float lastShotTime { get;  set; }
    public float lastShotTimeOfFlight { get; private set; }

    public void SetTargetWithAngle(Vector3 point, float angle)
    {
        currentAngle = angle;

        Vector3 direction = point - firePoint.position;
        float yOffset = -direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        currentSpeed = ProjectileMath.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, angle * Mathf.Deg2Rad);

        // projectileArc.UpdateArc(currentSpeed, distance, Physics.gravity.magnitude, currentAngle * Mathf.Deg2Rad, direction, true);
        SetTurret(direction, currentAngle);

        currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle * Mathf.Deg2Rad, yOffset, Physics.gravity.magnitude);
    }

    public void SetTargetWithSpeed(Vector3 point, float speed, bool useLowAngle)
    {
        currentSpeed = speed;

        Vector3 direction = point - firePoint.position;
        float yOffset = direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;     

        float angle0, angle1;
        bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out angle0, out angle1);

        if (targetInRange)
            currentAngle = useLowAngle ? angle1 : angle0;

        //projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
        SetTurret(direction, currentAngle * Mathf.Rad2Deg);

        currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle, -yOffset, Physics.gravity.magnitude);
    }

    public void Fire(Vector3 cursorTransform)
    {
        WeaponSO _weapon = WeaponsHandler.instance.GetCurrentWeapon();
        if (int.TryParse(_weapon.totalAmmo, out int ammo))
        {
             if(ammo <= 0)
                return;
        }

        foreach (var item in GameManager.instance.weaponHandler.Weapon)
        {
            if (item.WeaponName == PlayerPrefs.GetString("ActiveWeapon"))
            {
                if (item.IsShootable)
                {
                    //anim.SetBool("GunIdle",true);
                    lastShotTime = Time.time;
                    lastShotTimeOfFlight = currentTimeOfFlight;
                    item.Weapon.transform.GetComponent<Shoot>().ShootNow();
                }
                else if(item.IsBottle)
                {
                    anim.SetTrigger("BootleShoot");
                    lastShotTime = Time.time;
                    lastShotTimeOfFlight = currentTimeOfFlight;
                    item.Weapon.transform.GetComponent<SodaShoot>().ShootNow();
                }
                else if (item.IsBeachBall)
                {
                    GameManager.instance.OnBeachBall();
                }
                else if (item.IsBirthdayHat)
                {
                    GameManager.instance.OnClickBirthdayHat(cursorTransform);
                }
                else if (item.IsRcCar)
                {
                    GameManager.instance.OnClickRcCar();
                }
                else
                {
                    Debug.Log("Active Weapon: " + PlayerPrefs.GetString("ActiveWeapon"));
                   // anim.SetBool("GunIdle", false);
                    foreach (var item1 in WeaponsThrowPrefabs)
                    {
                        if (item1.name == PlayerPrefs.GetString("ActiveWeapon"))
                        {
                            projectilePrefab = item1;
                        }
                    }
                    anim.SetTrigger("Throw");
                }

                WeaponsHandler.instance.UseWeapon(item.weaponId);
            }
        }
    }

    public void FireNow()
    {
        WeapomObj.SetActive(false);

        Quaternion rotation = Quaternion.identity;
        if(projectilePrefab.name == "BirthdayCakePlates")
            rotation = Quaternion.Euler(-90, 0, 0);
        GameObject p = Instantiate(projectilePrefab, firePoint.position, rotation);
        p.GetComponent<Rigidbody>().velocity = hand.up * currentSpeed;
        //Vector3 direction = (Cursors.CursorsInst.HitPoint - firePoint.position).normalized;
        //p.GetComponent<Rigidbody>().velocity = direction * currentSpeed;
        Instantiate(EffectShoot, smokePuffPoint.position, Quaternion.LookRotation(smokePuffPoint.position));

        lastShotTime = Time.time;
        lastShotTimeOfFlight = currentTimeOfFlight;
        PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo") - 1);
    }

    private void SetTurret(Vector3 planarDirection, float turretAngle)
    {
        handBase.rotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(-90, -90, 0);
        hand.localRotation = Quaternion.Euler(90, 90, 0) * Quaternion.AngleAxis(turretAngle, Vector3.forward);
    }
}