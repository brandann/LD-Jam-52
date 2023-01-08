using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehavior : MonoBehaviour
{
    public float duration;
    private Coroutine routine;

    // Start is called before the first frame update
    void Start()
    {
        routine = StartCoroutine(Sign());
    }

    IEnumerator Sign()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
