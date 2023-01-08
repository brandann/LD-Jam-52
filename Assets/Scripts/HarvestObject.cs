using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestObject : MonoBehaviour
{
    public enum HarvestType { Slime, Mushroom};
    public HarvestType myHarvestType;

    private int myValue = 0;

    const int SlimeValue = 3;
    const int MushroomValue = 8;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    protected void init()
    {
        if (myHarvestType == HarvestType.Slime)
        {
            myValue = SlimeValue;
        }
        else if (myHarvestType == HarvestType.Mushroom)
        {
            myValue = MushroomValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetValue()
    {
        return myValue;
    }

}
