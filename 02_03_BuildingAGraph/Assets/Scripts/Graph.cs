using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10,100)]
    int resolution = 10;

    Transform[] points;

    // Start is called before the first frame update
    void Awake()
    {
        float step = 2.0f / resolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution];
        for(int i=0; i<points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            point.SetParent(transform,false);

            position.x = (i + 0.5f) * step - 1.0f;
            point.localPosition = position;
            point.localScale = scale;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for(int i = 0; i < points.Length; i++)
        {
            Vector3 position = points[i].localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));

            points[i].localPosition = position;
        }
    }
}
