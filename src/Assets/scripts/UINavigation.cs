using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject UIMain;
    public GameObject PlayerMovement;
    public GameObject DialougeText;

    public SpawnSets spawnPoint;
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        //UIScreen.SetActive(true);
        PlayerMovement.GetComponent<PlayerControl>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetButton()
    {
        SceneManager.LoadScene("Testlab");
        //player.transform.position = spawnPoint.spawnPosition;
        //playerControl.actionState = PlayerControl.ActionState.Normal;
        //player.playerState = Humans.PlayerState.Alive;
        //player.anim.isDead = false;
        //player.StartStats();
    }
    public void MainMenuBTN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("MainMenu");
        //UIScreen.SetActive(false);
    }
    public void SettingBtn()
    {
        playerControl.isInMenu = false;
        UIMain.SetActive(false);
        settingUI.SetActive(true);
    }
    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void ExitMenuBtn()
    {
        //if in anyy menu not Main menu exit to maain menu els if in main menu exit back to game
        if(settingUI.active == true)
        {
            settingUI.SetActive(false);
            UIMain.SetActive(true);
        }
    }
    public void PlayBtn()
    {
        UIMain.SetActive(false);
        PlayerMovement.GetComponent<PlayerControl>().enabled = true;
        playerControl.isInMenu = false;
    }
    public void TextExit()
    {
        PlayerMovement.GetComponent<PlayerControl>().enabled = true;
        DialougeText.SetActive(false);
    }
}

