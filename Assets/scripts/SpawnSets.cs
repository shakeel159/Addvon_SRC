using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSets : MonoBehaviour
{
    public GameObject player;
    public GameObject[] levels;

    public GameObject[] spawnPoint;

    public Vector2 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levels[0].gameObject.activeSelf)
        {
            //player.transform.position = new Vector3(SpawnPosition.transform.position.x, SpawnPosition.transform.position.y, SpawnPosition.transform.position.z);
            spawnPosition = new Vector3(spawnPoint[0].transform.position.x, spawnPoint[0].transform.position.y);
        }
    }
}
