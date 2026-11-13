using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Esc : MonoBehaviour
{
    public GameObject escPanel;
    public GameObject[] canava;

    public GameObject setPan;

    public GameObject musOn;
    public GameObject musOf;

    public MonoBehaviour AmoCont;
    public GunsChange Guns;
    public market mar;
    private GameObject activeWeaponBeforeEsc;

    private bool isEscOpen = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        // Синхронизируем иконки музыки
        musOn.SetActive(menu.isMusic);
        musOf.SetActive(!menu.isMusic);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscMenu();
        }
    }

    public void ToggleEscMenu()
    {
        isEscOpen = !isEscOpen;
        escPanel.SetActive(isEscOpen);

        Time.timeScale = isEscOpen ? 0f : 1f;

        if (mar != null)
            mar.enabled = !isEscOpen;

        foreach (GameObject obj in canava)
        {
            if (obj != escPanel)
                obj.SetActive(!isEscOpen);
        }

        Cursor.visible = isEscOpen;
        Cursor.lockState = isEscOpen ? CursorLockMode.None : CursorLockMode.Locked;

        if (AmoCont != null)
            AmoCont.enabled = !isEscOpen;

        if (isEscOpen)
        {
            if (Guns.Pistol.activeSelf) activeWeaponBeforeEsc = Guns.Pistol;
            else if (Guns.AK52.activeSelf) activeWeaponBeforeEsc = Guns.AK52;
            else if (Guns.PiPistol.activeSelf) activeWeaponBeforeEsc = Guns.PiPistol;
            else if (Guns.Shotgun.activeSelf) activeWeaponBeforeEsc = Guns.Shotgun;
            else if (Guns.AWP.activeSelf) activeWeaponBeforeEsc = Guns.AWP;
            else activeWeaponBeforeEsc = null;

            Guns.Pistol.SetActive(false);
            Guns.AK52.SetActive(false);
            Guns.PiPistol.SetActive(false);
            Guns.Shotgun.SetActive(false);
            Guns.AWP.SetActive(false);
        }
        else
        {
            if (activeWeaponBeforeEsc != null)
                activeWeaponBeforeEsc.SetActive(true);
        }
    }

    public void settOn()
    {
        setPan.SetActive(true);
    }

    public void settOf()
    {
        setPan.SetActive(false);
    }

    public void clouse()
    {
        SceneManager.LoadScene("menu");
    }

    public void musici()
    {
        menu.isMusic = !menu.isMusic;
        musOn.SetActive(menu.isMusic);
        musOf.SetActive(!menu.isMusic);
    }
}