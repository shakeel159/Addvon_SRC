using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerProgress : MonoBehaviour
{
    public GameObject LevelOne;
    public GameObject LevelTwo;
    public GameObject levelOneDoor; // wehn scene is active collect all enemies tagged "enemies" in array. // when all enemies dead open door gameobject
    public GameObject LevelTwoDoor;
    public GameObject[] enemiesInScene;
    public GameObject bossInScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Find active enemies in scene
        // Find all GameObjects with the tag "enemy" and add them to the enemies array
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        bossInScene = GameObject.FindGameObjectWithTag("boss");
        if (enemiesInScene.Length == 0)
        {
            if(LevelOne.active == true)
            {
                levelOneDoor.SetActive(true);
            }
            if (LevelTwo.active == true); // and boss is dead
            {
                if (bossInScene.active == false)
                {

                    LevelTwoDoor.SetActive(true);
                }
            }
        }
    }
}
