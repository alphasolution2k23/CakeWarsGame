using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using static AliScripts.Utils;

public class MenuController : MonoBehaviour
{
    public static MenuController menuControllerInst;
    [SerializeField] public TextMeshProUGUI Health;
    [SerializeField] public TextMeshProUGUI TotalCoins;
    [SerializeField] public TextMeshProUGUI levelText;

    [SerializeField] private GameObject NotEnoughHealth;
    // Start is called before the first frame update

    [Header("UpgradeMarket")]
  
    public WeaponUpgade [] WeaponUpgrade;
    public GameObject UpgradeBuyPanel;
    public GameObject NotEnoughPanel;
    public GameObject MaxReachedPanel;
    public GameObject BuyWarningPanel;

    [SerializeField] private Sprite[] WeaponImage;
    [SerializeField] private GameObject[] WeaponUpgradesObjects;
    [SerializeField] private string[] WeaponDescriptionObjects;
    [SerializeField] private string [] WeaponNames;

    [SerializeField] private int[] WeaponPrice;
    [SerializeField] public TextMeshProUGUI WeaponPriceText;
    [SerializeField] public TextMeshProUGUI WeaponName;
    [SerializeField] public TextMeshProUGUI WeaponDescription;


    [SerializeField] private GameObject LeftBtn;
    [SerializeField] private GameObject RightBtn;
    [SerializeField] private GameObject BuyBtn;
    [SerializeField] private GameObject purchasedText;
    public UpgradeMarketWeapons upgradingWeaponPanel;

    [SerializeField] private Image WeaponSprite;
    [SerializeField] private int WeaponIndex;

    [System.Serializable]
    public class WeaponUpgade
    {
        public GameObject WeaponUpgradeHealth;
        public GameObject WeaponUpgradeFireSpeed;
        public GameObject WeaponUpgradeDamage;
        public GameObject PlayerHP;
        public GameObject PlayerAttack;
        public GameObject SmartWatch;
        public GameObject FourLeafCover;
        public GameObject ArmoredBirthdayHat;
    }



    private void Update()
    {
        int currentHealth = PlayerPrefs.GetInt("CurrentHealth");

        if (currentHealth > LivesRestorer.instance.DefHealth)
        {
            currentHealth = LivesRestorer.instance.DefHealth;
            PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        }

        Health.text = currentHealth.ToString();
        TotalCoins.text = FormatInt(PlayerPrefs.GetInt("TotalCoins"));
    }



    private void Awake()
    {
        if (menuControllerInst == null)
        {
            menuControllerInst = this;
        }
     
        if (PlayerPrefs.GetInt("FirstRun") == 0)
        {
            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.SetInt("TotalCoins", 100);
            PlayerPrefs.SetInt("CurrentHealth", LivesRestorer.instance.DefHealth);
            PlayerPrefs.SetInt("WeaponUnlocked" + 0, 1);
        }
    }

    private void Start()
    {
        levelText.text = "Level " + PlayerPrefs.GetInt("LevelNo", 1).ToString();
    }

    public void GamePlay()
    {
        if (PlayerPrefs.GetInt("CurrentHealth") >= 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            NotEnoughHealth.SetActive(true);
        }
      
    }
    public GameObject OBJECTTOUPDATE;
    public void CheckUpgradeWeapon(GameObject ObjectToUpdate)
    {
        OBJECTTOUPDATE = ObjectToUpdate;
        UpgradeBuyPanel.SetActive(true);
        return;

        int Level = PlayerPrefs.GetInt("WeaponLevel" + ObjectToUpdate.name);
        Debug.Log("Level Nowwwwwwwwwwwwww" + Level);
        if (PlayerPrefs.GetInt("TotalCoins") >= 30 && Level < 2 && PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) == 1  )
        {
            OBJECTTOUPDATE = ObjectToUpdate;
            UpgradeBuyPanel.SetActive(true);
        }
        if (ObjectToUpdate.GetComponent<NotBuyAble>())
        {
            if (PlayerPrefs.GetInt("TotalCoins") >= 30 && Level < 2 && !ObjectToUpdate.GetComponent<NotBuyAble>().Buy)
            {
                OBJECTTOUPDATE = ObjectToUpdate;
                UpgradeBuyPanel.SetActive(true);
            }
        }    
        else if (PlayerPrefs.GetInt("TotalCoins") >= 30 && Level >= 2 && PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) == 1)
        {
            MaxReachedPanel.SetActive(true);

        }
        else if(PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) == 0 )
        {
            if (ObjectToUpdate.GetComponent<NotBuyAble>())
            {
            }
            else
            {
                BuyWarningPanel.SetActive(true);
            }
                
        }
        else if (PlayerPrefs.GetInt("TotalCoins") <= 30)
        {
            NotEnoughPanel.SetActive(true);
        }

    }
    public string UpgradingWeaponsName;
    public int UpgradingStatNumber;

    public void SetUpgradingName(string weaponName)
    {
        UpgradingWeaponsName = weaponName;
    }
    public void SetUpgradingStatNum(int statNum)
    {
        UpgradingStatNumber = statNum;
    }

    public void YesUpgradeWeapon()
    {

        if (PlayerPrefs.GetInt("TotalCoins") < 30)
            return;

        Debug.Log("UUpdating Weapon: " + UpgradingWeaponsName + "Stat" + UpgradingStatNumber);
        PlayerPrefs.SetInt(UpgradingWeaponsName + "Stat" + UpgradingStatNumber, PlayerPrefs.GetInt(UpgradingWeaponsName + "Stat" + UpgradingStatNumber, 0) + 1);

        if (PlayerPrefs.GetInt(UpgradingWeaponsName + "Stat" + UpgradingStatNumber) > 5)
        {
            PlayerPrefs.SetInt(UpgradingWeaponsName + "Stat" + UpgradingStatNumber, 5);
        }

        PlayerPrefs.SetInt("TotalCoins" , PlayerPrefs.GetInt("TotalCoins" ) - 30);
        UpgradeBuyPanel.SetActive(false);
        upgradingWeaponPanel.RefreshUI();
    }

    public void UpgradeWeapon(int Level,GameObject Obj)
    {
        Debug.Log("Level" + Level);
        for (int i = 0; i < Level; i++)
        {
            Obj.transform.GetChild(i).gameObject.SetActive(true);
        }

        //foreach (Transform child in Obj.transform)
        //{
           
        //    if (index <= Level)
        //    {
        //        child.gameObject.SetActive(true);

        //    }
        //    index++;
        //}
    }

    public void OnEnableCheckUpgrade()
    {
        foreach (var upgrade in WeaponUpgrade)
        {
            int PlayerHP = PlayerPrefs.GetInt("WeaponLevel" + upgrade.PlayerHP.name);
            //Debug.Log("Weapon Upgrade PlayerHP:   " + PlayerHP);
            for (int i = 0; i < PlayerHP; i++)
            {
                upgrade.PlayerHP.transform.GetChild(i).gameObject.SetActive(true);
            }
            int PlayerAttack = PlayerPrefs.GetInt("WeaponLevel" + upgrade.PlayerAttack.name);
            //Debug.Log("Weapon Upgrade PlayerAttack:   " + PlayerAttack);
            for (int i = 0; i < PlayerAttack; i++)
            {
                upgrade.PlayerAttack.transform.GetChild(i).gameObject.SetActive(true);
            }
            int SmartWatch = PlayerPrefs.GetInt("WeaponLevel" + upgrade.SmartWatch.name);
            //Debug.Log("Weapon Upgrade SmartWatch:   " + SmartWatch);
            for (int i = 0; i < SmartWatch; i++)
            {
                upgrade.SmartWatch.transform.GetChild(i).gameObject.SetActive(true);
            }
            int FourLeafCover = PlayerPrefs.GetInt("WeaponLevel" + upgrade.FourLeafCover.name);
            //Debug.Log("Weapon Upgrade FourLeafCover:   " + FourLeafCover);
            for (int i = 0; i < FourLeafCover; i++)
            {
                upgrade.FourLeafCover.transform.GetChild(i).gameObject.SetActive(true);
            }
            int ArmoredBirthdayHat = PlayerPrefs.GetInt("WeaponLevel" + upgrade.ArmoredBirthdayHat.name);
            //Debug.Log("Weapon Upgrade ArmoredBirthdayHat:   " + ArmoredBirthdayHat);
            for (int i = 0; i < ArmoredBirthdayHat; i++)
            {
                upgrade.ArmoredBirthdayHat.transform.GetChild(i).gameObject.SetActive(true);
            }

            if (PlayerHP >= 2)
            {
                upgrade.PlayerHP.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (PlayerAttack >= 2)
            {
                upgrade.PlayerAttack.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
    public void BuyWeapon()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= WeaponPrice[WeaponIndex])
        {
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - WeaponPrice[WeaponIndex]);
            PlayerPrefs.SetInt("WeaponUnlocked" + WeaponIndex, 1);
            BuyBtn.SetActive(false);

            purchasedText.gameObject.SetActive(true);
            purchasedText.GetComponent<TMP_Text>().text = "Purchased";
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }

    public void RefreshUI()
    {
        TotalCoins.text = FormatInt(PlayerPrefs.GetInt("TotalCoins"));
    }
    public void LeftBtnWeaponSelect()
    {
        if (WeaponIndex > 0)
        {
            RightBtn.GetComponent<Button>().interactable = true;
            WeaponIndex--;
            if (WeaponIndex == 15)
            {
                LeftBtnWeaponSelect();
                return;
            }
            if (WeaponIndex == 17)
            {
                LeftBtnWeaponSelect();
                return;
            }
            WeaponSprite.sprite = WeaponImage[WeaponIndex];
            //WeaponUpgradesObjects[WeaponIndex].SetActive(true);
            WeaponName.text = WeaponNames[WeaponIndex].ToString();
            WeaponDescription.text = WeaponDescriptionObjects[WeaponIndex].ToString();
            OnEnableCheckUpgrade();

            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) == 1)
            {
                BuyBtn.SetActive(false);

                purchasedText.gameObject.SetActive(true);
                purchasedText.GetComponent<TMP_Text>().text = "Purchased";
            }
            else
            {
                WeaponPriceText.text = WeaponPrice[WeaponIndex].ToString();
                BuyBtn.SetActive(true);

                purchasedText.gameObject.SetActive(false);
                purchasedText.GetComponent<TMP_Text>().text = "Purchasing";
            }

        }
        else
        {
            LeftBtn.GetComponent<Button>().interactable = false;

        }
    }
    public void RightBtnWeaponSelect()
    {
        if (WeaponIndex < WeaponImage.Length - 1)
        {
            LeftBtn.GetComponent<Button>().interactable = true;
            WeaponIndex++;
            if (WeaponIndex == 15)
            {
                RightBtnWeaponSelect();
                return;
            }
            if (WeaponIndex == 17)
            {
                RightBtnWeaponSelect();
                return;
            }
            WeaponSprite.sprite = WeaponImage[WeaponIndex];
            //WeaponUpgradesObjects[WeaponIndex].SetActive(true);
            WeaponName.text = WeaponNames[WeaponIndex].ToString();
            WeaponDescription.text = WeaponDescriptionObjects[WeaponIndex].ToString();
            OnEnableCheckUpgrade();
            if (PlayerPrefs.GetInt("WeaponUnlocked" + WeaponIndex) == 1)
            {
                BuyBtn.SetActive(false);

                purchasedText.gameObject.SetActive(true);
                purchasedText.GetComponent<TMP_Text>().text = "Purchased";
            }
            else
            {
                WeaponPriceText.text = WeaponPrice[WeaponIndex].ToString();
                BuyBtn.SetActive(true);

                purchasedText.gameObject.SetActive(false);
                purchasedText.GetComponent<TMP_Text>().text = "Purchasing";
            }
        }
        else
        {
            RightBtn.GetComponent<Button>().interactable = false;
        }
    }

    public void AddCoins(int coins)
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + coins);
    }
    public void BuySmartPhone()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= 200)
        {
            PlayerPrefs.SetInt("BuySmartPhone", 1);
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - 200);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }
    public void BuyBoquetOfRoses()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= 500)
        {
            PlayerPrefs.SetInt("BuyBoquetOfRoses", 1);
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - 500);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }
    public void BuyTeddyBear()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= 300)
        {
            PlayerPrefs.SetInt("BuyTeddyBear", 1);
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - 300);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }
    public void BuyRcCar()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= 500)
        {
            PlayerPrefs.SetInt("BuyRcCar", 1);
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - 500);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }
    public void BuyMagicKit()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= 200)
        {
            PlayerPrefs.SetInt("BuyMagicKit", 1);
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - 200);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
        }
    }
}
