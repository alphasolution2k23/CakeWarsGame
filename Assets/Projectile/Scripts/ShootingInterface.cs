using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootingInterface : MonoBehaviour 
{
    [SerializeField]
    Cursors targetCursor;

    [SerializeField]
    ShootingController ObjectToThrow;

    [SerializeField]
    Text timeOfFlightText;

    [SerializeField]
    float defaultFireSpeed = 35;

    [SerializeField]
    float defaultFireAngle = 45;

    [SerializeField]
    int ammo = 20;

    private float initialFireAngle;
    private float initialFireSpeed;
    private bool useLowAngle;

    private bool useInitialAngle;

    public GameObject CooledownBtn;
   
    public CurrentWeapon currentWeapon;
    void Awake()
    {
        useLowAngle = true;

        initialFireAngle = defaultFireAngle;
        initialFireSpeed = defaultFireSpeed;

        useInitialAngle = true;
        ammo += 2 * PlayerPrefs.GetInt("LevelNo", 1);
        PlayerPrefs.SetInt("Ammo", ammo);
        PlayerPrefs.SetInt("TotalAmmo", ammo);
    }

    void Update()
    {
       
       
        if (useInitialAngle)
            ObjectToThrow.SetTargetWithAngle(targetCursor.transform.position, defaultFireAngle);
        else
            ObjectToThrow.SetTargetWithSpeed(targetCursor.transform.position, defaultFireSpeed, useLowAngle);

        if (ControlFreak2.CF2Input.GetButtonUp("Fire1") && Time.time > ObjectToThrow.lastShotTime + ObjectToThrow.cooldown)
        {
            ObjectToThrow.Fire(targetCursor.transform.position);
        }
        else if (ControlFreak2.CF2Input.GetButtonUp("Fire1") &&  ObjectToThrow.lastShotTime==0)
        {
            ObjectToThrow.Fire(targetCursor.transform.position);
        }
        else if (ObjectToThrow.lastShotTime != 0 && Time.time < ObjectToThrow.lastShotTime + ObjectToThrow.cooldown)
        {
            CooledownBtn.GetComponent<Button>().interactable = false;
            CooledownBtn.GetComponent<Animator>().SetBool("CoolDown", true);           
        }
        else if (ControlFreak2.CF2Input.GetButtonUp("Fire1") && PlayerPrefs.GetInt("Ammo") <= 0)
        {
            //GameManager.instance.StartCoroutine(GameManager.instance.LevelFailedAmmo());
        }
        else
        {
            CooledownBtn.GetComponent<Button>().interactable = true;
            CooledownBtn.GetComponent<Animator>().SetBool("CoolDown", false);
            ObjectToThrow.WeapomObj.SetActive(true);
        }

        timeOfFlightText.text = Mathf.Clamp(ObjectToThrow.lastShotTimeOfFlight - (Time.time - ObjectToThrow.lastShotTime), 0, float.MaxValue).ToString("F3");
    }

    //public void SetInitialFireAngle(string angle)
    //{
    //    initialFireAngle = Convert.ToSingle(angle);
    //}

    //public void SetInitialFireSpeed(string speed)
    //{
    //    initialFireSpeed = Convert.ToSingle(speed);
    //}

    //public void SetLowAngle(bool useLowAngle)
    //{
    //    this.useLowAngle = useLowAngle;
    //}

    //public void UseInitialAngle(bool value)
    //{
    //    useInitialAngle = value;
    //}
}
