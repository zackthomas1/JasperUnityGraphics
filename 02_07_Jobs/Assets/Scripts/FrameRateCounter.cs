using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    public enum DisplayMode { FPS, MS }
    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    [SerializeField, Range(1.0f, 2.0f)]
    float sampleDuration = 1.0f;

    int frames;
    float duration;
    float bestDuration = float.MaxValue;
    float worseDuration;

    private void Update()
    {

        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;

        if(frameDuration < bestDuration)
        {
            bestDuration = frameDuration;
        }
        if (frameDuration > worseDuration)
        {
            worseDuration = frameDuration;
        }

        if (duration >= sampleDuration)
        {

            if(displayMode == DisplayMode.FPS)
            {
                display.SetText("FPS\nBest: {0:0}\nAverage: {1:0}\nWorse: {2:0}", 
                    1.0f / bestDuration, 
                    frames / duration, 
                    1.0f / worseDuration
                );
            }
            else if(displayMode == DisplayMode.MS)
            {
                display.SetText("MS\nBest: {0:1}\nAverage: {1:1}\nWorse: {2:1}",
                    1000f * bestDuration, 
                    1000f * duration / frames, 
                    1000f * worseDuration
                    );
            }

            frames = 0;
            duration = 0.0f;
            bestDuration = float.MaxValue;
            worseDuration = 0.0f;
        }
    }
}
