using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public static NewBehaviourScript Instance { get; private set; }

    public GameObject dialogueUI;
    public bool hadInteracted;
    public PlayerControl player;
    public Player pl;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Player = GetComponent<PlayerControl>();
        //player = GetComponent<PlayerControl>();
        dialogueUI.SetActive(false);
        hadInteracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hadInteracted == false)
        {
            //Playr Dialogue 
            player.horizontalInput = 0;
            Debug.Log("player state: " + player.actionState);
            player.actionState = PlayerControl.ActionState.Normal;
            Debug.Log("player state: " + player.actionState);
            player.GetComponent<PlayerControl>().enabled = false;
            hadInteracted = true;
            dialogueUI.SetActive(true);
        }
    }
}
