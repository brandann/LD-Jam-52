using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HeroClickController : MonoBehaviour
{
    //PUBLIC ---------------------------------------------

    public TextMeshProUGUI ScoreText;
    public GameObject BurstManager;

    //PRIVATE --------------------------------------------
    private float Speed = 50;
    private float BouseReduction = 0.9f;


    private Vector3 _mousePositionTarget = Vector3.zero;
    private Rigidbody2D rigidbody2D;
    private Vector3 logLastVelocity = Vector3.zero;
    private Vector3 OffScreenVector = new Vector3(10000, 10000, 100000);

    public GameObject SelectionIcon;
    public GameObject SignPlus;

    //functions ------------------------------------------


    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.Instance.isGameActive)
        {
            if (Input.GetMouseButton(0))
                SetMouseTarget();
            if (Input.GetMouseButtonUp(0))
                MoveHeroToTarget();
        }
    }

    private void LateUpdate()
    {
        
        
    }

    private void FixedUpdate()
    {
        logLastVelocity = rigidbody2D.velocity;
        CheckOnScreen();
    }

    private void CheckOnScreen()
    {
        bool dirty = false;
        if (this.transform.position.x > 13f)
            dirty = true;
        else if (this.transform.position.x < -13f)
            dirty = true;
        if (this.transform.position.y > 8f)
            dirty = true;
        else if (this.transform.position.y < -9f)
            dirty = true;

        if (dirty)
        {
            Vector3 new_pos = new Vector3();
            if (this.transform.position.x > 13f)
                new_pos.x = 10;
            else if (this.transform.position.x < -13f)
                new_pos.x = -10;
            else
                new_pos.x = this.transform.position.x;

            if (this.transform.position.y > 8f)
                new_pos.y = 5f;
            else if (this.transform.position.y < -9f)
                new_pos.y = -6f;
            else
                new_pos.y = this.transform.position.y;

            this.transform.position = new_pos;
        }


    }


    private void DrawTargetLine(Vector3 v1, Vector3 v2)
    {
        SelectionIcon.transform.position = v2;
    }


    private void SetMouseTarget()
    {
        // GET MOUSE POSITION
        Vector3 m_pos_screen = Input.mousePosition;
        m_pos_screen.z = 0;

        _mousePositionTarget = Camera.main.ScreenToWorldPoint(m_pos_screen);
        _mousePositionTarget.z = 0;

        if (Global.Instance.isGameActive)
        {
            SelectionIcon.SetActive(true);
            DrawTargetLine(this.transform.position, _mousePositionTarget);
            Time.timeScale = 0.3f;
        }
    }


    private void MoveHeroToTarget()
    {
        var dir = _mousePositionTarget - this.transform.position;
        rigidbody2D.AddForce(dir*Speed);

        SelectionIcon.SetActive(false);

        DrawTargetLine(OffScreenVector, OffScreenVector);

        if (Global.Instance.isGameActive)
        {
            Time.timeScale = 1f;
        }
    }


    void OnCollisionEnter2D(Collision2D c)
    {
        bool reflect = false;

        if (c.gameObject.tag.Equals("Wall"))
            reflect = true;
        if (c.gameObject.tag.Equals("Obstacles"))
            reflect = true;

        if (reflect)
        {
            rigidbody2D.velocity = Vector2.Reflect(logLastVelocity * BouseReduction, c.contacts[0].normal);
            //var go = GameObject.Instantiate(BurstManager);
            //go.GetComponent<BurstManager>().MakeBurst(5, new Color(245f/255f, 84f/255f, 223f/255f), this.transform.position, 1, BurstBehavior.eBurstSprites.Heart);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Slime"))
            PickupSlime(c.gameObject);
        if (c.gameObject.tag.Equals("Mushroom"))
            PickupMushroom(c.gameObject);
        if(c.gameObject.tag.Equals("Powerup"))
        {
            var pub = c.gameObject.GetComponent<PowerupBehavior>();
            Global.Instance.StartPowerup(pub.PowerupValue);
            //print("Power Up: " + pub.PowerupValue.ToString());
            pub.DropSign();
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag.Equals("BurstObject"))
            Destroy(c.gameObject);
    }

    public void PickupSlime(GameObject g)
    {
        print("Pickup Slime");
        var score = g.GetComponent<HarvestObject>().GetValue() * Global.Instance.ScoreModifier;
        Global.Instance.Score += score;
        ScoreText.text = "" + Global.Instance.Score;
        var slimeColor = g.gameObject.GetComponent<HarvestObject>().parent.GetComponent<SpriteRenderer>().color;

        var go = GameObject.Instantiate(BurstManager);
        go.GetComponent<BurstManager>().MakeBurst(score, slimeColor, g.transform.position, 1, BurstBehavior.eBurstSprites.Circle, true);
        //Log.AddLog("Slime Collected +" + score);

        var sign = Instantiate(SignPlus);
        sign.transform.position = g.transform.position;

        Destroy(g.GetComponent<HarvestObject>().parent);
    }

    public void PickupMushroom(GameObject g)
    {
        print("Pickup Mushroom");
        var score = g.GetComponent<HarvestObject>().GetValue() * Global.Instance.ScoreModifier;
        Global.Instance.Score += score;
        ScoreText.text = "" + Global.Instance.Score;

        var go = GameObject.Instantiate(BurstManager);
        go.GetComponent<BurstManager>().MakeBurst(score, new Color(111f/255f, 40f/255f, 38f/255f), g.transform.position, 1, BurstBehavior.eBurstSprites.Circle, true);
        //Log.AddLog("Mushroom Collected +" +score);

        var sign = Instantiate(SignPlus);
        sign.transform.position = g.transform.position;

        Destroy(g.GetComponent<HarvestObject>().parent);
    }
}
