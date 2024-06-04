using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerControl control;

    public GameObject levelOne;
    public GameObject levelTwo;

    private bool isEnter = false;

    public GameObject SpawnPosition;

    private void Awake()
    {
        control = playerObject.GetComponent<PlayerControl>();
        levelTwo.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("LevelTwo");
            isEnter = true;
            //Tramsport Player to new level instead of Scene // to much things get reset when changing scenes
            levelTwo.SetActive(true);
            playerObject.transform.position = new Vector3(SpawnPosition.transform.position.x, SpawnPosition.transform.position.y, SpawnPosition.transform.position.z);
            //SceneManager.LoadScene("TestLab");
        }
        if(isEnter == true)
        {
            levelOne.SetActive(false);
            isEnter = false;
            
        }
    }
}
