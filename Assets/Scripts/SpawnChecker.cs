using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChecker : MonoBehaviour
{
    private Coroutine spawnCoroutine;
    private float SpawnCheckDuration = 0.2f;

    public GameObject MushroomSpawnPrefab;
    public GameObject SlimeSpawnPrefab;
    public GameObject[] PowerupPrefab;

    float PreFabMin = 0;
    float PrefabMax = 100;
    float PrefabSlimeRange = 74;
    float PowerupRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnCheckerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        bool hit = false;

        if (c.gameObject.tag.Equals("Wall"))
            hit = true;
        if (c.gameObject.tag.Equals("Obstacles"))
            hit = true;
        if (c.gameObject.tag.Equals("Hero"))
            hit = true;
        if (c.gameObject.tag.Equals("Mushroom"))
            hit = true;
        if (c.gameObject.tag.Equals("Slime"))
            hit = true;
        if (c.gameObject.tag.Equals("SpawnChecker"))
            hit = true;
        if (c.gameObject.tag.Equals("Powerup"))
            hit = true;

        if (hit)
        {
            StopCoroutine(spawnCoroutine);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpawnCheckerRoutine()
    {
        yield return new WaitForSeconds(SpawnCheckDuration);
        MakeSpawn();
    }

    private void MakeSpawn()
    {
        var r = Random.Range(PreFabMin, PrefabMax);
        if (r < PowerupRange)
            CreatePowerup();
        else if (r <= PrefabSlimeRange)
            CreateSlime();
        else
            CreateMushroom();
        StopCoroutine(spawnCoroutine);
        Destroy(this.gameObject);
    }

    private void CreateSlime()
    {
        var g = GameObject.Instantiate(SlimeSpawnPrefab);
        g.transform.position = this.transform.position;
        g.GetComponent<HarvestSpawnerTimer>().SetType(HarvestSpawnerTimer.HarvestType.Slime);
        StopCoroutine(spawnCoroutine);
        Destroy(this.gameObject);
    }

    private void CreateMushroom()
    {
        var g = GameObject.Instantiate(MushroomSpawnPrefab);
        g.transform.position = this.transform.position;
        g.GetComponent<HarvestSpawnerTimer>().SetType(HarvestSpawnerTimer.HarvestType.Mushroom);
        StopCoroutine(spawnCoroutine);
        Destroy(this.gameObject);
    }

    private void CreatePowerup()
    {
        var r = Random.Range(0, 5);
        var g = GameObject.Instantiate(PowerupPrefab[r]);
        g.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
}
