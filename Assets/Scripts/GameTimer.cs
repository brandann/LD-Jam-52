using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Timer;
    public GameObject TimeUpPanel;
    public GameObject GameOverPanel;
    public GameObject HUDPanel;
    public TextMeshProUGUI TimeUpScore;
    public TextMeshProUGUI GameOverScore;
    public int initialTime;
    public float currentTime;
    private bool isPaused = false;
    private float delta_time = 0.1f;

    Color blue = new Color(107f / 255f, 205f / 255f, 227f / 255f, 215f / 255f);
    //Color blue = new Color(107, 205, 227, 200);
    Color red = new Color(227f / 255f, 21f / 255f, 34f / 255f, 150f / 255f);
    //Color red = new Color(227 , 21 , 34 , 200 );
    int small = 74;
    int larger = 225;

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
        

        if (currentTime <= 4f)
        {
            Timer.text = "" + timef;
            Timer.color = red;
            Timer.fontSize = larger;
        }
        else
        {
            Timer.text = "" + (int)currentTime;
            Timer.color = blue;
            Timer.fontSize = small;
        }
    }

    private void EndGame()
    {
        Global.Instance.Level++;

        Time.timeScale = 0;
        Global.Instance.isGameActive = false;
        HUDPanel.SetActive(false);

        if (Global.Instance.Level == 2)
        {
            TimeUpPanel.SetActive(true);
            TimeUpScore.text = "" + Global.Instance.Score;
        }
        else if (Global.Instance.Level == 3)
        {
            TimeUpPanel.SetActive(true);
            TimeUpScore.text = "" + Global.Instance.Score;
        }
        else if (Global.Instance.Level == 4)
        {
            GameOverPanel.SetActive(true);
            GameOverScore.text = "" + Global.Instance.Score;
        }
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

    public void SizeModifier(int v)
    {
        StartCoroutine(SizeModifierTimer(v));
    }

    IEnumerator SizeModifierTimer(int v)
    {
        yield return new WaitForSeconds(v);
        Global.Instance.EndPowerup(Global.Powerups.Grow);
    }
}
