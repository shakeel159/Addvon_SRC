using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    public bool bossFightStart;
    // Start is called before the first frame update
    void Start()
    {
        bossFightStart = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossFightStart = true;
        }

    }

    //when exit on left end boss fight
}
