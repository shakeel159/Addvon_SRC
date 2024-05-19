using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class dummyScript : Humans
{
    //[SerializeField] private FlashEffect flashEffact;
    //[SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {

        Name = gameObject.name;
        currentHealth = 50f;
        attackDmg = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DmgTaken(float damage)
    {
        //flashEffact.Flash();
        base.DmgTaken(damage);
    }
}
