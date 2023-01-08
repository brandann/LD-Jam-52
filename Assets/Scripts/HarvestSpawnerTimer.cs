using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSpawnerTimer : MonoBehaviour
{
    public enum HarvestType { Slime, Mushroom };
    public HarvestType myHarvestType;

    private int myValue = 0;

    const int SlimeValue = 1;
    const int MushroomValue = 3;

    public GameObject MushroomPrefab;
    public GameObject SlimePrefab;

    private Coroutine routine;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {
        StopCoroutine(routine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(HarvestType ht)
    {
        myHarvestType = ht;
        if (myHarvestType == HarvestType.Slime) myValue = SlimeValue;
        else if (myHarvestType == HarvestType.Mushroom) myValue = MushroomValue;

        routine = StartCoroutine(SpawnTimerRoutine());
    }

    IEnumerator SpawnTimerRoutine()
    {
        yield return new WaitForSeconds(myValue);

        MakeSpawn();

        Destroy(this.gameObject);
    }

    public void ForceSpawn()
    {
        StopCoroutine(routine);
        MakeSpawn();
        Destroy(this.gameObject);
    }

    private void MakeSpawn()
    {
        GameObject g = null;
        if (myHarvestType == HarvestType.Slime)
        {
            g = SlimePrefab;
            var go = GameObject.Instantiate(g);
            go.transform.position = this.transform.position;
            go.GetComponent<SlimeBehavior>().SetDirection();
        }
        else if (myHarvestType == HarvestType.Mushroom)
        {
            g = MushroomPrefab;
            var go = GameObject.Instantiate(g);
            go.transform.position = this.transform.position;
        }
    }
}
