using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehavior : MonoBehaviour
{
    private float force = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        bool move = false;
        if (c.gameObject.tag.Equals("Slime"))
            move = true;
/*        if (c.gameObject.tag.Equals("Mushroom"))
            move = true;*/

        if (move)
        {
            var par = c.gameObject.GetComponent<HarvestObject>().parent;
            var rb = par.GetComponent<Rigidbody2D>();

            var dist = this.transform.position - par.transform.position;
            dist = dist.normalized * force;

            rb.AddForce(dist);
        }
    }
}
