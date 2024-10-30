using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject nextlevel;
   
    public static GameManager instance;
    [Header("GameEssentials")]
    public bool isGameOver;
    [SerializeField] private bool IsTestingLevel=false;
    [SerializeField] private int IsTestingLevelNo;
    [SerializeField] public int EnemiesDefeated;
    [SerializeField] public float RemainingHealth;
    [SerializeField] public TextMeshProUGUI EnemiesDefeatedGameWin;
    [SerializeField] public TextMeshProUGUI CoinsGameWin;
    [SerializeField] public TextMeshProUGUI EnemiesDefeatedGamePlay;
    [SerializeField] public TextMeshProUGUI AmmoGamePlay;
    [SerializeField] public TextMeshProUGUI CoinsGamePlay;
    [SerializeField] public TextMeshProUGUI RemainingHealthGameWin;
    [SerializeField] public TextMeshProUGUI RemainingHealthGameFail;
    [SerializeField] public TextMeshProUGUI EnemiesDefeatedGameFailed;
    [SerializeField] public TextMeshProUGUI CoinsGameFailed;

    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] public GameObject HealthSlider;

    [Header("GameUI")]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject LevelCompletePannel;
    [SerializeField] private GameObject LevelFailedAmmoPannel;
    [SerializeField] private GameObject LevelFailedAmmoPannelNotEnoughCoins;
    [SerializeField] private GameObject LevelFailedHealthPannel;
    [SerializeField] private GameObject OutOfLivesPanel;
    [Header("Ability")]
    [SerializeField] private GameObject AbilitySlider;
    [SerializeField] private GameObject AbilityButton;
    [SerializeField] private GameObject SmartPhone;
    [SerializeField] private GameObject BoquestOfRoses;
    [SerializeField] private GameObject TeddyBear;
    [SerializeField] private GameObject RcCar;
    [SerializeField] private GameObject MagicKit;
    [Header("WeaponSelection")]
    public WeaponHandler weaponHandler;
    [SerializeField] private GameObject WeaponSelectionButton;
    [SerializeField] private GameObject WeaponSlider;
    [SerializeField] private Sprite [] WeaponImage;
    [SerializeField] private GameObject[] EquipBtn;
    [SerializeField] private GameObject LeftBtn;
    [SerializeField] private GameObject RightBtn;

    [SerializeField] private Image WeaponSprite;
    [SerializeField] private int WeaponIndex;



    [SerializeField]
    public ShootingController shootingController;
    public CurrentWeapon currentWeapon;
    [Header("Teddy")]
    [SerializeField] private GameObject TeddyPrefab;
    [SerializeField] private GameObject BeachBallPrefab;
    [SerializeField] private GameObject RcCarPrefab;
    [SerializeField] private GameObject BirthdayHatPrefab;
    [SerializeField] private GameObject TeddySpawnPoint;
    [SerializeField] private GameObject BeachBallSpawnPoint;
    [SerializeField] private GameObject RcCarSpawnPoint;







    [Header("WaveSystem")]
    public WaveManager WaveSystem;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if (IsTestingLevel)
        {
            PlayerPrefs.SetInt("LevelNo", IsTestingLevelNo);
        }

        PlayerPrefs.SetInt("Coins", 0);
        UpdateLevelNo();
        PlayerPrefs.SetString("ActiveWeapon", "CupCake");
        currentWeapon = CurrentWeapon.CupCake;
        PlayerPrefs.SetInt("DaneceAbilityActive", 3);
       
    }

    private void Start()
    {
        WeaponsHandler.instance.onUseWeapon += UpdateAmmoUI;
        UpdateAmmoUi("∞");
    }

    private void OnDestroy()
    {
        WeaponsHandler.instance.onUseWeapon -= UpdateAmmoUI;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("GamePaused") == 0)
        {
          //  UpdateCoinsUi();
        }
    }



    #region CoreFunctionality
    public void UpdateLevelNo()
    {
        LevelText.text = "Level "+PlayerPrefs.GetInt("LevelNo", 1).ToString();
    }
    #endregion
    #region Public Function
    public void NextLevel()
    {
        PlayerPrefs.SetInt("GamePaused", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartLevel()
    {
        if(PlayerPrefs.GetInt("CurrentHealth") <= 0)
        {
            OutOfLivesPanel.SetActive(true);
            return;
        }
        Time.timeScale = 1;
        PlayerPrefs.SetInt("GamePaused", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("GamePaused", 0);
        SceneManager.LoadScene(0);
    }
    public void PauseMenuOpen()
    {
        PlayerPrefs.SetInt("GamePaused", 1);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void OpenSettings()
    {
        PlayerPrefs.SetInt("GamePaused", 1);
        Time.timeScale = 0;
        SettingMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        PlayerPrefs.SetInt("GamePaused", 0);
        SettingMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ResumeGame()
    {
        PlayerPrefs.SetInt("GamePaused", 0);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    bool coinsRewarded = false;
    bool levelCompleted;
    public IEnumerator LevelComplete()
    {
        if (levelCompleted)
            yield break;

        levelCompleted = true;
        PlayerPrefs.SetInt("GamePaused", 1);
        yield return new WaitForSeconds(3.0f);
        EnemiesDefeatedGameWin.text = EnemiesDefeated.ToString();
        RemainingHealthGameWin.text = PlayerPrefs.GetInt("CurrentHealth").ToString();
        CoinsGameWin.text = PlayerPrefs.GetInt("Coins").ToString();
        LevelCompletePannel.SetActive(true);

        PlayerPrefs.SetInt("LevelNo", PlayerPrefs.GetInt("LevelNo", 1) + 1);

        if (!coinsRewarded)
        {
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + PlayerPrefs.GetInt("Coins"));
            coinsRewarded = true;
        }
        //Time.timeScale = 0;
    }
    public IEnumerator LevelFailedHealth()
    {
        PlayerPrefs.SetInt("GamePaused", 1);
        yield return new WaitForSeconds(0.01f);
        EnemiesDefeatedGameFailed.text = EnemiesDefeated.ToString();
        CoinsGameFailed.text = "0";
        LevelFailedHealthPannel.SetActive(true);
        Time.timeScale = 0;

        if (!coinsRewarded)
        {
            PlayerPrefs.SetInt("CurrentHealth", PlayerPrefs.GetInt("CurrentHealth") - 1);
            RemainingHealthGameFail.text = PlayerPrefs.GetInt("CurrentHealth").ToString();
            coinsRewarded = true;
        }
        //Time.timeScale = 0;
    }
    public IEnumerator LevelFailedAmmo()
    {
        PlayerPrefs.SetInt("GamePaused", 1);
        yield return new WaitForSeconds(0.01f);   
        
        LevelFailedAmmoPannel.SetActive(true);
        Time.timeScale = 0;
        //Time.timeScale = 0;
    }

    public void ContinuePlayAddAmmo()
    {
        PlayerPrefs.SetInt("GamePaused", 0);
        if (PlayerPrefs.GetInt("Coins") >= 500)
        {

            PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo") + 20);
            PlayerPrefs.SetInt("TotalAmmo", PlayerPrefs.GetInt("Ammo"));
            UpdateCoinsUi();
            LevelFailedAmmoPannel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            LevelFailedAmmoPannelNotEnoughCoins.SetActive(true);
        }
        
    }

    public void UpdateAmmoUi(string Text)
    {
        AmmoGamePlay.text = Text;
    }
    public void UpdateAmmoUI(WeaponSO weapon)
    {
        if (int.TryParse(weapon.totalAmmo, out int totalAmmo))
        {
            int ammoPerLoad = int.Parse(weapon.ammoPerReload);

            int currentLoad = totalAmmo % ammoPerLoad;
            currentLoad = currentLoad == 0 ? ammoPerLoad : currentLoad;

            totalAmmo--;

            int remainingAmmo = totalAmmo - currentLoad;

            if (remainingAmmo + 1 >= 0)
                UpdateAmmoUi(currentLoad + "/" + (remainingAmmo + 1));
            else
                UpdateAmmoUi("0/0");
        }
        else
        {
            UpdateAmmoUi("∞");
        }
    }

    public void UpdateCoinsUi()
    {
        CoinsGamePlay.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void DepletHealth()
    {
        int currentHealth = PlayerPrefs.GetInt("CurrentHealth");
        currentHealth--;

        if (currentHealth < 0)
            currentHealth = 0;

        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        Debug.Log("Health Deppleted to " + currentHealth);
    }

    public void AddCoins()
    {
        UpdateCoinsUi();
    }
    public void OnOpenAbility()
    {
        AbilityButton.gameObject.SetActive(false);
        AbilitySlider.SetActive(true);
        if (PlayerPrefs.GetInt("BuySmartPhone") == 1)
        {
            SmartPhone.GetComponent<Button>().interactable=true;
        }
        if (PlayerPrefs.GetInt("BuySmartPhone") == 0)
        {
            SmartPhone.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("BuyBoquetOfRoses") == 1)
        {
            BoquestOfRoses.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("BuyBoquetOfRoses") == 0)
        {
            BoquestOfRoses.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("BuyTeddyBear") == 1)
        {
            TeddyBear.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("BuyTeddyBear") == 0)
        {
            TeddyBear.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("BuyRcCar") == 1)
        {
            RcCar.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("BuyRcCar") == 0)
        {
            RcCar.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("BuyMagicKit") == 1)
        {
            MagicKit.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("BuyMagicKit") == 0)
        {
            MagicKit.GetComponent<Button>().interactable = false;
        }
        Invoke("OnCloseAbility", 2.0f);
    }

    //public void ApplySmartPhoneAbility()
    //{
    //    PhoneModel.SetActive(true);
    //    PlayerPrefs.SetInt("DaneceAbilityActive", 1);
    //    PhoneModel.GetComponent<Animator>().SetBool("Rotate", true);
    //    Invoke("DisableSmartPhoneAbility", 8.0f);
    //}
    //public void DisableSmartPhoneAbility()
    //{
    //    PlayerPrefs.SetInt("DaneceAbilityActive", 0);
    //    PhoneModel.GetComponent<Animator>().SetBool("Rotate", false);
    //    PhoneModel.SetActive(false);

    //}

    public void LeftBtnWeaponSelect()
    {
        if (WeaponIndex > 0)
        {
            WeaponIndex--;
            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) != 1)
            {
                LeftBtnWeaponSelect();
            }

            RightBtn.GetComponent<Button>().interactable = true;
            WeaponSprite.sprite = WeaponImage[WeaponIndex];

            foreach (var item in EquipBtn)
            {
                item.SetActive(false);
            }
            EquipBtn[WeaponIndex].SetActive(true);
            EquipBtn[WeaponIndex].GetComponent<Button>().interactable = true;
            WeaponSO weapon = WeaponsHandler.instance.GetWeaponById(WeaponIndex);
            WeaponSelectionUI.instance.UpdateUI(weapon);
        }
        else
        {
            //LeftBtn.GetComponent<Button>().interactable = false;

            WeaponIndex = WeaponImage.Length;
            LeftBtnWeaponSelect();
        }
    }
    public void RightBtnWeaponSelect()
    {
        if (WeaponIndex < WeaponImage.Length -1)
        {
            WeaponIndex++;

            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) != 1)
            {
                RightBtnWeaponSelect();
            }

            LeftBtn.GetComponent<Button>().interactable = true;
            WeaponSprite.sprite = WeaponImage[WeaponIndex];

            foreach (var item in EquipBtn)
            {
                item.SetActive(false);
            }
            EquipBtn[WeaponIndex].SetActive(true);
            EquipBtn[WeaponIndex].GetComponent<Button>().interactable = true;
            WeaponSO weapon = WeaponsHandler.instance.GetWeaponById(WeaponIndex);
            WeaponSelectionUI.instance.UpdateUI(weapon);
        }
        else
        {
            //RightBtn.GetComponent<Button>().interactable = false;
            WeaponIndex = -1;
            RightBtnWeaponSelect();
        }
    }
    public void SelectWeapon(string name)
    {
        PlayerPrefs.SetString("ActiveWeapon", name);
        //PlayerPrefs.SetInt("DaneceAbilityActive", 0);
        foreach (var item in weaponHandler.Weapon)
        {
            if (item.WeaponName == name)
            {
                item.Weapon.SetActive(true);
                if (item.IsShootable)
                {
                    shootingController.anim.SetTrigger("GunIdle");
                }
                else if(item.IsBottle)
                {
                    shootingController.anim.SetTrigger("BottleIdle");
                }
                else
                {
                    shootingController.anim.SetTrigger("ThrowIdle");
                }

            }
            else
            {
                item.Weapon.SetActive(false);
            }
        }
        if (System.Enum.TryParse(PlayerPrefs.GetString("ActiveWeapon"), true, out CurrentWeapon weapon))
        {
            currentWeapon = weapon;
            Debug.Log("Current Weapon: " + currentWeapon);
     
        }
        else
        {
            Debug.LogWarning("Invalid weapon name: " + name);
        }
        switch (currentWeapon)
        {
            case CurrentWeapon.CupCake:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.SmartPhone:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.Roses:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.PoisonousChocolate:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.ExplosiveCake:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.FrostyCocktail:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.PartyBlowers:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.PearlNecklace:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.ClownSurpriseMusicBox:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.Baloons:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.BubbleCannon:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.BirthdayCakePlates:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
            case CurrentWeapon.Pinata:
                shootingController.lastShotTime = 0f;
                foreach (var item in weaponHandler.Weapon)
                {
                    if (item.WeaponName == name)
                    {
                        shootingController.cooldown = item.cooldownTime;

                    }
                }
                break;
        }
        WeaponSelectionButton.GetComponent<Image>().sprite = WeaponImage[WeaponIndex];

    }
    public void OnCloseAbility()
    {
      
        AbilityButton.SetActive(true);
        AbilitySlider.SetActive(false);
    }


    public void OnOpenWeaponSelection()
    {
        Time.timeScale = 0;
        WeaponSelectionButton.gameObject.SetActive(false);
        WeaponSlider.SetActive(true);
        WeaponSelectionUI.instance.UpdateUI(WeaponsHandler.instance.GetCurrentWeapon());
        //Invoke("OnCloseWeaponSlider", 2.0f);
    }
    public void OnCloseWeaponSelection()
    {
        Time.timeScale = 1;
        WeaponSelectionButton.gameObject.SetActive(true);
        WeaponSlider.SetActive(false);

        //Invoke("OnCloseWeaponSlider", 2.0f);
    }
    public void OnCloseWeaponSlider()
    {
        Time.timeScale = 1;
        WeaponSelectionButton.SetActive(true);
        WeaponSlider.SetActive(false);
    }
    
    public void OnClickTeddy()
    {
        Instantiate(TeddyPrefab, TeddySpawnPoint.transform.position, TeddySpawnPoint.transform.rotation);
    }
    public void OnBeachBall()
    {
        Instantiate(BeachBallPrefab, BeachBallSpawnPoint.transform.position, Quaternion.identity);
    }

    public void OnClickRcCar()
    {
        Instantiate(RcCarPrefab, RcCarSpawnPoint.transform.position, RcCarSpawnPoint.transform.rotation);
    }
    public void OnClickBirthdayHat(Vector3 pos)
    {
        Instantiate(BirthdayHatPrefab, pos, BirthdayHatPrefab.transform.rotation);
    }

    internal void SetWeaponSpriteToCupcake()
    {
        WeaponSelectionButton.GetComponent<Image>().sprite = WeaponImage[0];
    }

    #endregion
}
//Coins stats on Game Win pannel added 
//Level Failed With Ammo 
//Coins Logic Added 
//Ammo Logic Added
//Added Infinite Enemy waves in waves system
//Added cooldown of one second in shooting 
//Level Failed Health added 
