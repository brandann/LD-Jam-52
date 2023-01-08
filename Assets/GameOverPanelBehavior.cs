using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1;
        Global.Instance.ResetGlobal();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnPlayButton()
    {
        Time.timeScale = 1;
        //Global.Instance.ResetGlobal();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
