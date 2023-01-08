using UnityEngine;
using System.Collections;

public class BurstBehavior : MonoBehaviour {

	public enum eBurstSprites {
		Square = 0,
		Circle = 1,
		Diamond = 2,
		Hex = 3,
		Star3 = 4,
		Star5 = 5,
		Heart = 6,
		Triangle = 7,

		Random = 99
	};

	// MIN, MAX OF SPEED APPLIED TO THE SINGLE
	// BURST GAMEOBJECTS
	public Vector2 RandomSpeedRange;
	
	// SET AS RANDOM OF RANGE RandomSpeedRange
    private float mSpeed;

	public Sprite[] BurstSprites;
    private bool lerpToScore = false;
    private Coroutine routine;
    private float distfromlerp = 0.6f;
    private GameObject scoreTarget;
    public bool isHarvestBurst = false;

    void Start ()
    {
        mSpeed = Random.Range (RandomSpeedRange[0], RandomSpeedRange[1]);
        routine = StartCoroutine(MyTimer());
        //scoreTarget = GameObject.Find("ScoreTarget");
        scoreTarget = GameObject.Find("Hero Circle Sprite");
    }

    private void OnDestroy()
    {
        StopCoroutine(routine);
    }

    void Update ()
    {
   	


    }

    private void FixedUpdate()
    {
        if(lerpToScore)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, scoreTarget.transform.position, 0.2f * Time.timeScale);
            var dist = scoreTarget.transform.position - this.transform.position;
            var mag = Mathf.Abs(dist.magnitude);
        }
        else
        {
            // MOVE THE GAMEOBJECT FORWARD BASED ON TRANSFORM.UP
            // THE ROATION IS SET BY THE BURST MANAGER
            transform.position += mSpeed * Time.smoothDeltaTime * transform.up;
        }
    }

    IEnumerator MyTimer()
    {
        yield return new WaitForSeconds(1);
        lerpToScore = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        if (!isHarvestBurst)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("ScoreTarget"))
            Destroy(this.gameObject);
    }


}
