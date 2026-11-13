using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject setPanel;
    public GameObject joinPanel;
    public GameObject creatPanel;

    public static bool isMusic = true;
    public GameObject musiOn;
    public GameObject musiOf;


    void Start()
    {
        musiOn.SetActive(isMusic);
        musiOf.SetActive(!isMusic);

    }

    public void LevelPanelOn()
    {
        levelPanel.SetActive(true);
    }
    public void LevelPanelOff()
    {
        levelPanel.SetActive(false);
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Pool()
    {
        SceneManager.LoadScene("Pool");
    }

    public void SetPanelOn()
    {
        setPanel.SetActive(true);
    }
    public void music()
    {
        isMusic = !isMusic;
        musiOn.SetActive(isMusic);
        musiOf.SetActive(!isMusic);
    }
    public void SetPanelOff()
    {
        setPanel.SetActive(false);
    }

    public void CreatPanelOn()
    {
        creatPanel.SetActive(true);
    }
    public void CreatPanelOff()
    {
        creatPanel.SetActive(false);
    }
    public void JoinPanelOn()
    {
        joinPanel.SetActive(true);
    }

    public void JoinPanelOff()
    {
        joinPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}