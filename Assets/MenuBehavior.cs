using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject MainCamera;
    public GameObject TutorialPanel;
    public GameObject HUDPanel;

    public void OnButtonPlayPressed()
    {
        MainCamera.GetComponent<GameTimer>().StartGame();
        Global.Instance.ResetGlobal();

        // load obstacles
        StartMenu.SetActive(false);
        HUDPanel.SetActive(true);
        Time.timeScale = 1;

        Global.Instance.isGameActive = true;
    }

    public void OnTutorial()
    {
        StartMenu.SetActive(false);
        TutorialPanel.SetActive(true);
    }

    public void OnTutorialBack()
    {
        StartMenu.SetActive(true);
        TutorialPanel.SetActive(false);
    }
}
