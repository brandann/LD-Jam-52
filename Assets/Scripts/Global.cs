using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global 
{
    private static Global instance = null;

    private int score = 0;
    public int Level = 1;

    private float magnetvalue = 0;
    private int scoremodifier = 1;

    public bool isGameActive = false;

    public void ResetGlobal()
    {
        scoremodifier = 1;
        magnetvalue = 0;
        instance.isGameActive = true;
    }

    public float MagnetValue
    {
        get { return magnetvalue; }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
        }
    }

    public int ScoreModifier
    {
        get { return scoremodifier; }
    }

    public static Global Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Global();
            }
            return instance;
        }
    }


    public enum Powerups { AddTime = 0, Magnet = 1, Score = 2, Grow = 3, Bomb = 4}
    private int delta_score = 1;
    private int dur_score = 10;
    private int delta_magnet = 4;
    private int dur_magnet = 10;
    private int delta_time = 10;
    private int delta_grow = 2;
    private int dur_grow = 10;

    public void StartPowerup(Powerups pu)
    {
        var CameraObject = GameObject.Find("Main Camera");
        var timer = CameraObject.GetComponent<GameTimer>();
        switch (pu)
        {
            case Powerups.AddTime:
                timer.AddTime(delta_time);
                break;
            case Powerups.Magnet:
                magnetvalue += delta_magnet;
                timer.StartMagnetTimer(dur_magnet);
                break;
            case Powerups.Score:
                scoremodifier += delta_score;
                timer.StartScoreModifierTimer(dur_score);
                break;
            case Powerups.Grow:
                //var slimesg = GameObject.FindGameObjectsWithTag("Slime");
                //var shroomsg = GameObject.FindGameObjectsWithTag("Mushroom");
                var herog = GameObject.Find("Hero Circle Sprite");
                herog.transform.localScale *= delta_grow;
                timer.SizeModifier(dur_grow);
                break;
            case Powerups.Bomb:
                var slimes = GameObject.FindGameObjectsWithTag("Slime");
                var shrooms = GameObject.FindGameObjectsWithTag("Mushroom");
                var hero = GameObject.Find("Hero Circle Sprite");
                var heroContol = hero.GetComponent<HeroClickController>();
                foreach (GameObject s in slimes)
                {
                    var mag = Vector3.Distance(s.transform.position, hero.transform.position);
                    if (Mathf.Abs(mag) < 8)
                        heroContol.PickupSlime(s);
                }
                foreach (GameObject s in shrooms)
                {
                    var mag = Vector3.Distance(s.transform.position, hero.transform.position);
                    if (Mathf.Abs(mag) < 8)
                        heroContol.PickupMushroom(s);
                }
                break;
        }
    }

    public void EndPowerup(Powerups pu)
    {
        switch (pu)
        {
            case Powerups.AddTime:
                // do nothing here
                break;
            case Powerups.Magnet:
                magnetvalue -= delta_magnet;
                break;
            case Powerups.Score:
                scoremodifier += delta_score;
                break;
            case Powerups.Grow:
                var herog = GameObject.Find("Hero Circle Sprite");
                herog.transform.localScale /= delta_grow;
                break;
        }
    }


}
