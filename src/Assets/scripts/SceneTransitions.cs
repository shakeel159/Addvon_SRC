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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("LevelTwo");

            //Tramsport Player to new level instead of Scene // to much things get reset when changing scenes
            //playerObject.transform.position = new Vector3(SpawnPosition.transform.position.x, SpawnPosition.transform.position.y, SpawnPosition.transform.position.z);
            //levelOne.SetActive(false);
            //levelTwo.SetActive(true);

            SceneManager.LoadScene("TestLab");
        }
    }
}
