using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEditor.UI;

[RequireComponent(typeof(TMP_Text))]
public class FpsCounter : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _expSmoothingFactor = 0.9f;
    [SerializeField] private float _refreshFrequency = 0.4f;

    private float _timeSinceUpdate = 0f;
    private float _averageFps = 1f;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // Exponentially weighted moving average (EWMA)
        _averageFps = _expSmoothingFactor * _averageFps + (1f - _expSmoothingFactor) * 1f / Time.unscaledDeltaTime;

        if (_timeSinceUpdate < _refreshFrequency)
        {
            _timeSinceUpdate += Time.deltaTime;
            return;
        }

        int fps = Mathf.RoundToInt(_averageFps);
        _text.text = fps.ToString();

        _timeSinceUpdate = 0f;
    }
}
