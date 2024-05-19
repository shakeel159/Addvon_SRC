using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject DeadMenu;


    //public EventSystem eventSystem;
    public bool isDeadMenuActive;
    
    public GameObject mainMenuFirst;
    public GameObject DeadMenuFirst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool open = mainMenu.activeSelf;

        if (DeadMenu.activeSelf)
        {
            isDeadMenuActive = true;

            ////clear selected object
            //EventSystem.current.SetSelectedGameObject(null);
            ////select new object
            //EventSystem.current.SetSelectedGameObject(DeadMenuFirst);
            EventSystem.current.firstSelectedGameObject = DeadMenuFirst;
        }
    }

}

