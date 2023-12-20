using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10,100)]
    int resolution = 40;

    [SerializeField]
    FunctionLibrary.FunctionName function= FunctionLibrary.FunctionName.TwistingTorus;

    Transform[] points;

    // Start is called before the first frame update
    void Awake()
    {
        float step = 10.0f / resolution;
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
        float time = Time.time;
        float step = 2.0f / resolution;

        for (int i = 0; i < resolution; i++) // row
        {
            float v = (i + 0.5f) * step - 1.0f;
            for (int j = 0; j < resolution; j++) // column
            {
                int index = (i * resolution) + j;

                float u = (j + 0.5f) * step - 1.0f;

                FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
                points[index].localPosition = f(u, v, time);
            }
        }
    }
}
