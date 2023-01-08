using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Timer;
    public GameObject GameOverPanel;
    public GameObject HUDPanel;
    public TextMeshProUGUI GameOverScore;
    public int initialTime;
    public float currentTime;
    private bool isPaused = false;
    private float delta_time = 0.1f;

    private Coroutine routine;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame()
    {
        currentTime = initialTime;
        routine = StartCoroutine(MyTimer());
    }

    private void OnDestroy()
    {
        StopCoroutine(routine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MyTimer()
    {
        bool running = true;
        while (running)
        {
            yield return new WaitForSeconds(delta_time);
            if(!isPaused)
            {
                currentTime -= (delta_time * Time.timeScale);
                PostTime();
                if (currentTime <= 0)
                {
                    EndGame();
                    running = false;
                }
            }
        }
    }

    private void PostTime()
    {
        int timei = (int)(currentTime * 100);
        float timef = ((float) timei) / 100f;
        Timer.text = "" + timef;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        Global.Instance.isGameActive = false;
        HUDPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        GameOverScore.text = "" + Global.Instance.Score;
    }

    public void AddTime(int v)
    {
        currentTime += v;
    }

    public void StartMagnetTimer(int v)
    {
        StartCoroutine(MagnetTimer(v));
    }

    IEnumerator MagnetTimer(int v)
    {
        yield return new WaitForSeconds(v);
        Global.Instance.EndPowerup(Global.Powerups.Magnet);
    }

    public void StartScoreModifierTimer(int v)
    {
        StartCoroutine(ScoreModifierTimer(v));
    }

    IEnumerator ScoreModifierTimer(int v)
    {
        yield return new WaitForSeconds(v);
        Global.Instance.EndPowerup(Global.Powerups.Score);
    }
}
