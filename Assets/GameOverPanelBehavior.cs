using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanelBehavior : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject StartMenu;

    public GameObject Obs1;
    public GameObject Obs2;
    public GameObject Obs3;

    public TextMeshProUGUI Score;
    public GameObject HUDPanel;

    public void OnQuitButton()
    {
        Time.timeScale = 1;
        Global.Instance.ResetGlobal();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnPlayAgainButton()
    {
        
        Global.Instance.ResetGlobal();
        Global.Instance.Score = 0;
        Global.Instance.Level = 1;

        ResetScreen();

        Time.timeScale = 0;
        Global.Instance.isGameActive = false;
        HUDPanel.SetActive(false);

        // load obstacles
        StartMenu.SetActive(true);
        this.gameObject.SetActive(false);
        Obs3.SetActive(false);
        Obs2.SetActive(false);
        Obs1.SetActive(true);
    }

    public void OnNextLevelButton()
    {
        ResetScreen();

        Global.Instance.ResetGlobal();

        if(Global.Instance.Level == 2)
        {
            Obs2.SetActive(true);
            Obs1.SetActive(false);
        }
        else if(Global.Instance.Level == 3)
        {
            Obs3.SetActive(true);
            Obs2.SetActive(false);
        }

        this.gameObject.SetActive(false);

        HUDPanel.SetActive(true);
        Score.text = "" + Global.Instance.Score;

        MainCamera.GetComponent<GameTimer>().StartGame();
    }

    private void ResetScreen()
    {
        var slimes = GameObject.FindGameObjectsWithTag("Slime");
        var shrooms = GameObject.FindGameObjectsWithTag("Mushroom");
        var powerups = GameObject.FindGameObjectsWithTag("Powerup");
        var spawners = GameObject.FindGameObjectsWithTag("SpawnChecker");
        var signs = GameObject.FindGameObjectsWithTag("Sign");
        foreach (GameObject s in slimes)
        {
            var p = s.GetComponent<HarvestObject>().parent;
            Destroy(p);
        }
        foreach (GameObject s in shrooms)
        {
            var p = s.GetComponent<HarvestObject>().parent;
            Destroy(p);
        }
        foreach (GameObject s in powerups)
        {
            Destroy(s);
        }
        foreach (GameObject s in spawners)
        {
            Destroy(s);
        }
        foreach (GameObject s in signs)
        {
            Destroy(s);
        }
    }
}
