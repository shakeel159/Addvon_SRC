using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shroom : MonoBehaviour
{
    private EnemyPotrol patrolScript;
    // Start is called before the first frame update
    public void Start()
    {
        patrolScript = GetComponent<EnemyPotrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolScript.currentHealth <= 10)
        {
            patrolScript.currentHealth = 100;

        }
    }
}
