using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TMPro.TextMeshProUGUI display;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1.0f;

    [SerializeField]
    DisplayMode displayeMode = DisplayMode.FPS;

    public enum DisplayMode {FPS, MS};

    int frames;
    float duration, bestDuration = float.MaxValue, worstDuration;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;

        if(frameDuration < bestDuration)
        {
            bestDuration = frameDuration; 
        }

        if(frameDuration > worstDuration)
        {
            worstDuration = frameDuration;
        }

        if (duration >= sampleDuration)
        {
            if(displayeMode == DisplayMode.FPS)
            {
                display.SetText("FPS\n" +
                    "Best: {0:0}\n" +
                    "Average: {1:0}\n" +
                    "Worse: {2:0}", 
                    1.0f / bestDuration, frames / duration, 1.0f / worstDuration
                );
            }
            else
            {
                display.SetText("MS\nBest: {0:1}\n" +
                    "Average: {1:1}\n" +
                    "Worse: {2:1}", 
                    1000.0f * bestDuration, 1000.0f * (duration / frames), 1000.0f * worstDuration
                );
            }

            frames = 0;
            duration = 0.0f;
            bestDuration = float.MaxValue;
            worstDuration = 0.0f;
        }
    }
}
