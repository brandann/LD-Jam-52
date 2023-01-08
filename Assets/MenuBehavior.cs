using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPlayPressed()
    {
        MainCamera.GetComponent<GameTimer>().StartGame();
        Global.Instance.ResetGlobal();

        // load obstacles
        StartMenu.SetActive(false);
    }
}
