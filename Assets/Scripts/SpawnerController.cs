using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject SpawnCheckerPrefab;
    public GameObject SpawnAreaObject;

    private float minx = 0;
    private float maxx = 0;
    private float miny = 0;
    private float maxy = 0;

    // Start is called before the first frame update
    void Start()
    {
        minx = SpawnAreaObject.transform.position.x - (SpawnAreaObject.transform.localScale.x / 2);
        maxx = SpawnAreaObject.transform.position.x + (SpawnAreaObject.transform.localScale.x / 2);
        miny = SpawnAreaObject.transform.position.y - (SpawnAreaObject.transform.localScale.y / 2);
        maxy = SpawnAreaObject.transform.position.y + (SpawnAreaObject.transform.localScale.y / 2);
        StartCoroutine(PlaceSpawnChecker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaceSpawnChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Global.Instance.isGameActive)
            {
                var go = GameObject.Instantiate(SpawnCheckerPrefab);
                go.transform.position = GetRandomPosition();
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(minx, maxx);
        float y = Random.Range(miny, maxy);
        return new Vector3(x, y, 0);
    }
}
