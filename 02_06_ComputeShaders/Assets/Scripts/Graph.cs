using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10,500)]
    int resolution = 40;

    [SerializeField]
    FunctionLibrary.FunctionName function = FunctionLibrary.FunctionName.TwistingTorus;

    [SerializeField, Min(0.0f)]
    float functionDuration = 1.0f;

    [SerializeField]
    TransitionMode transitionMode = TransitionMode.Cycle;

    [SerializeField, Min(0.0f)]
    float transitionDuration = 1.0f;

    public enum TransitionMode {Cycle, Random};

    Transform[] points;

    float duration;

    bool transitioning; 
    
    FunctionLibrary.FunctionName transitionFunction;

    // Start is called before the first frame update
    void Awake()
    {
        float step = 2.0f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution * resolution];
        for(int i=0; i<points.Length; i++)
        { 
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            point.SetParent(transform, false);
            point.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime; 

        if (transitioning) 
        {
            if(duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if(duration >= functionDuration)
        {
            duration -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            function = PickNextFunction();
        }

        if (transitioning)
        {
            UpdateFunctionTransition();
        }
        else
        {
            UpdateFunction();
        }
    }

    void UpdateFunction ()
    {

        float time = Time.time;
        float step = 2.0f / resolution;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

        for (int i = 0; i<resolution; i++) // row
        {
            float v = (i + 0.5f) * step - 1.0f;
            for (int j = 0; j<resolution; j++) // column
            {
                int index = (i * resolution) + j;
                float u = (j + 0.5f) * step - 1.0f;
                points[index].localPosition = f(u, v, time);
            }
        }
    }

    void UpdateFunctionTransition ()
    {
        FunctionLibrary.Function from = FunctionLibrary.GetFunction(transitionFunction);
        FunctionLibrary.Function to = FunctionLibrary.GetFunction(function);
        float progress = duration / transitionDuration;

        float time = Time.time;
        float step = 2.0f / resolution;

        for (int i = 0; i < resolution; i++) // row
        {
            float v = (i + 0.5f) * step - 1.0f;
            for (int j = 0; j < resolution; j++) // column
            {
                int index = (i * resolution) + j;
                float u = (j + 0.5f) * step - 1.0f;
                points[index].localPosition = FunctionLibrary.Morph(u, v, time, from, to, progress);
            }
        }
    }

    FunctionLibrary.FunctionName PickNextFunction()
    {
        return transitionMode == TransitionMode.Cycle ?
            FunctionLibrary.GetNextFunction(function) :
            FunctionLibrary.GetRandomFunctionNameOtherThan(function);
    }


}
