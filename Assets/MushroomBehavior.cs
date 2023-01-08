using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    private Coroutine routine;
    private float MushroomDecayTimer = 15;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        routine = StartCoroutine(MyTimer());
        SetRandomSprite();
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
        while (true)
        {
            yield return new WaitForSeconds(MushroomDecayTimer);

            Destroy(this.gameObject);
        }
    }

    private void SetRandomSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
