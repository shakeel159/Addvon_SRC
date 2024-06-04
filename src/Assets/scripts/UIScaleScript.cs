using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleScript : MonoBehaviour
{

    public Scrollbar sbar;
    public CanvasScaler scaler;
    public CanvasScaler Settingscaler;
    public float initialScaleFactor;
    public float minScaleFactor = 1f; // Minimum scale factor
    public float maxScaleFactor = 2.0f; // Maximum scale factor

    // Start is called before the first frame update
    void Start()
    {
        initialScaleFactor = scaler.scaleFactor;
        initialScaleFactor = Settingscaler.scaleFactor;
        // Set the scrollbar value to 0 initially
        sbar.value = 0;
        sbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(scaler.scaleFactor <= 1)
        {
            scaler.scaleFactor = 1;
            Settingscaler.scaleFactor = 1;
        }
    }
    //void OnGUI()
    //{

    //    // Create a horizontal Slider that controls scale factor levels. Its highest value is 1 and lowest is 0
    //    float scaleValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), sbar.value, 0.0F, 1.0F);

    //    // If the GUI slider value changes, update the scrollbar value to match
    //    if (Mathf.Abs(scaleValue - sbar.value) > 0.01f) // Threshold to prevent infinite loop
    //    {
    //        sbar.value = scaleValue;
    //    }
    //}
    void OnScrollbarValueChanged(float value)
    {
        // Clamp the value between 0 and 1 to ensure it stays within bounds
        value = Mathf.Clamp(value, 0.0f, 1.0f);
        // Calculate the new scale factor based on the scrollbar value
        scaler.scaleFactor = Mathf.Lerp(minScaleFactor, maxScaleFactor, value);
        Settingscaler.scaleFactor = Mathf.Lerp(minScaleFactor, maxScaleFactor, value);
    }
}
