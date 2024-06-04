using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceManager : MonoBehaviour
{
    public AudioSource nabkgroundMusicAudio;
    //Value from the slider, and it converts to volume level
    public Scrollbar backgroundmusicSldier;
    public float bacakgroundMusicValue;
    // Start is called before the first frame update
    void Start()
    {
        //Initiate the Slider value to half way
        bacakgroundMusicValue = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        bacakgroundMusicValue = backgroundmusicSldier.value;
        nabkgroundMusicAudio.volume = bacakgroundMusicValue;
    }
    //void OnGUI()
    //{
    //    //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
    //    bacakgroundMusicValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), bacakgroundMusicValue, 0.0F, 1.0F);
    //    //Makes the volume of the Audio match the Slider value
    //    nabkgroundMusicAudio.volume = bacakgroundMusicValue;
    //}
}
