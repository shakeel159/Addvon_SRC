using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    private Button buttin;

    private void Awake()
    {
        buttin = GetComponent<Button>();
        buttin.onClick.AddListener(btnClick);
    }
    public void btnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
