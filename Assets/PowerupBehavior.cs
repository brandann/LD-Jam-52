using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    public Global.Powerups myPowerup;
    public GameObject SignPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Global.Powerups PowerupValue { get { return myPowerup; } }

    public void DropSign()
    {
        var g = GameObject.Instantiate(SignPrefab);
        g.transform.position = this.transform.position;
    }

}
