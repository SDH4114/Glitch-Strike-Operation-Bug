using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class market : MonoBehaviour
{
    public int PistolPatMoney;
    public int AKPatMoney;
    public int PiPistolPatMoney;
    public int ShotPatMoney;
    public int AwpPatMoney;

    public int AKMoney;
    public int PiPistolMoney;
    public int AwpMoney;
    public int ShotMoney;
    public int baryerMoney;
    public int hpMoney;

    public int money;

    public bool hasAK52 = false;
    public bool hasPiPistol = false;
    public bool hasShotgun = false;
    public bool hasBaryer = false;
    public bool hasAwp = false;

    public GameObject Zakup;
    public GameObject[] canava;
    public TextMeshProUGUI moneyT;

    public MonoBehaviour AmoCont;
    public amoCont amoC;
    public GunsChange Guns;
    public gun pistolGun;
    public gun akGun;
    public gun piPistolGun;
    public gun shotgunGun;
    public gun AwpGun;

    private GameObject activeWeaponBeforeZakup;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool isActive = !Zakup.activeSelf;
            Zakup.SetActive(isActive);

            foreach (GameObject obj in canava)
            {
                if (obj != Zakup)
                    obj.SetActive(!isActive);
            }

            Cursor.visible = isActive;
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;

            if (AmoCont != null)
                AmoCont.enabled = !isActive;

            if (isActive)
            {
                if (Guns.Pistol.activeSelf) activeWeaponBeforeZakup = Guns.Pistol;
                else if (Guns.AK52.activeSelf) activeWeaponBeforeZakup = Guns.AK52;
                else if (Guns.PiPistol.activeSelf) activeWeaponBeforeZakup = Guns.PiPistol;
                else if (Guns.Shotgun.activeSelf) activeWeaponBeforeZakup = Guns.Shotgun;
                else if (Guns.AWP.activeSelf) activeWeaponBeforeZakup = Guns.AWP;
                else activeWeaponBeforeZakup = null;

                Guns.Pistol.SetActive(false);
                Guns.AK52.SetActive(false);
                Guns.PiPistol.SetActive(false);
                Guns.Shotgun.SetActive(false);
                Guns.AWP.SetActive(false);
            }
            else
            {
                if (activeWeaponBeforeZakup != null)
                    activeWeaponBeforeZakup.SetActive(true);
            }
        }

        if (moneyT != null)
            moneyT.text = money.ToString();

        if(Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.O))
        {
            money = 10000;
        }
    }

    public void AK()
    {
        if (money >= AKMoney && !hasAK52)
        {
            money -= AKMoney;
            hasAK52 = true;
        }
    }

    public void PiPistol()
    {
        if (money >= PiPistolMoney && !hasPiPistol)
        {
            money -= PiPistolMoney;
            hasPiPistol = true;
        }
    }

    public void Shotgun()
    {
        if (money >= ShotMoney && !hasShotgun)
        {
            money -= ShotMoney;
            hasShotgun = true;
        }
    }
    public void AWP()
    {
        if (money >= AwpMoney && !hasAwp)
        {
            money -= AwpMoney;
            hasAwp = true;
        }
    }



    public void baryer()
    {
        if (money >= baryerMoney && !hasBaryer)
        {
            money -= baryerMoney;
            hasBaryer = true;
        }
    }

    public void health()
    {
        if (money >= hpMoney && amoC.hp < 100)
        {
            money -= hpMoney;
            amoC.hp += 50;
            amoC.hp = Mathf.Clamp(amoC.hp, 0, 100);
        }
    }

    public void PistolPat()
    {
        if (money >= PistolPatMoney)
        {
            money -= PistolPatMoney;
            pistolGun.AllCountB += 16;
        }
    }

    public void AKPat()
    {
        if (money >= AKPatMoney)
        {
            money -= AKPatMoney;
            akGun.AllCountB += 42;
        }
    }

    public void PiPistolPat()
    {
        if (money >= PiPistolPatMoney)
        {
            money -= PiPistolPatMoney;
            piPistolGun.AllCountB += 18;
        }
    }

    public void ShotgunPat()
    {
        if (money >= ShotPatMoney)
        {
            money -= ShotPatMoney;
            shotgunGun.AllCountB += 6;
        }
    }

    public void AWPPat()
    {
        if (money >= AwpPatMoney)
        {
            money -= AwpPatMoney;
            AwpGun.AllCountB += 10;
        }
    }
}