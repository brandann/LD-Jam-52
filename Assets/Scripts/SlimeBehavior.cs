using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour  
{
    private float Duration = 3.14f;
    private float ColorDuration = 0.2f;
    private float Speed = 20;

    public Rigidbody2D rigidBody2d;

    private Vector3 direction;

    GameObject HeroObject;

    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddForceRoutine());
        StartCoroutine(ColorSwap());
        HeroObject = GameObject.Find("Hero Circle Sprite");

        this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 4)];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        direction = rigidBody2d.velocity;
        if(HeroObject)
        {
            var dist = HeroObject.transform.position - this.transform.position;
            if (dist.magnitude < Global.Instance.MagnetValue)
            {
                rigidBody2d.AddForce(dist.normalized * 1);
            }
        }
    }

    IEnumerator AddForceRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        SetDirection();
        while (true)
        {
            yield return new WaitForSeconds(Duration);
            SetDirection();
        }
    }

    IEnumerator ColorSwap()
    {
        while (true)
        {
            var s = this.gameObject.GetComponent<SpriteRenderer>();
            s.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            yield return new WaitForSeconds(ColorDuration);
        }
    }

    public void SetDirection()
    {
        Random.InitState((int)Time.timeSinceLevelLoad * 10000);
        var d = Random.insideUnitCircle;
        d = d.normalized;
        d *= Random.Range(1, 4);
        direction = d * Speed;
        rigidBody2d.AddForce(direction);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        bool hit = false;

        if (c.gameObject.tag.Equals("Wall"))
            hit = true;
        if (c.gameObject.tag.Equals("Obstacles"))
            hit = true;        
        if (c.gameObject.tag.Equals("SlimeBody"))
            hit = true;
        if (c.gameObject.tag.Equals("MushroomBody"))
            hit = true;

        if (hit)
        {
            rigidBody2d.velocity = Vector2.Reflect(direction * 0.9f, c.contacts[0].normal);
        }
    }
}
