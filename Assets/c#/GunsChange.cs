using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunsChange : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject AK52;
    public GameObject PiPistol;
    public GameObject Shotgun;
    public GameObject AWP;
    public GameObject Bomb;
    public GameObject Baryer;

    public TextMeshProUGUI bombT;
    public TextMeshProUGUI allBombsT;

    public market Marka;

    public GameObject[] ReadyS;

    void Start()
    {
        Pistol.SetActive(true);
        Marka = FindObjectOfType<market>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(Pistol);
            SpawnRandomReadySound();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Marka.hasAK52)
        {
            SetActiveWeapon(AK52);
            SpawnRandomReadySound();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Marka.hasPiPistol)
        {
            SetActiveWeapon(PiPistol);
            SpawnRandomReadySound();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && Marka.hasShotgun)
        {
            SetActiveWeapon(Shotgun);
            SpawnRandomReadySound();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && Marka.hasAwp)
        {
            SetActiveWeapon(AWP);
            SpawnRandomReadySound();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetUtilityMode(Bomb);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) && Marka.hasBaryer)
        {
            SetUtilityMode(Baryer);
        }
    }

    void SpawnRandomReadySound()
    {
        if (menu.isMusic && ReadyS.Length > 0)
        {
            int index = Random.Range(0, ReadyS.Length);
            Instantiate(ReadyS[index], Vector3.zero, Quaternion.identity);
        }
    }

    void SetActiveWeapon(GameObject weapon)
    {
        Pistol.SetActive(false);
        AK52.SetActive(false);
        PiPistol.SetActive(false);
        Shotgun.SetActive(false);
        AWP.SetActive(false);
        Bomb.SetActive(false);
        Baryer.SetActive(false);

        weapon.SetActive(true);
        bombT.enabled = true;
        allBombsT.enabled = true;
    }

    void SetUtilityMode(GameObject utility)
    {
        Pistol.SetActive(false);
        AK52.SetActive(false);
        PiPistol.SetActive(false);
        Shotgun.SetActive(false);
        AWP.SetActive(false);
        Bomb.SetActive(false);
        Baryer.SetActive(false);

        utility.SetActive(true);

        Renderer rend = utility.GetComponent<Renderer>();
        if (rend != null) rend.enabled = true;

        bombT.enabled = false;
        allBombsT.enabled = false;
    }
}