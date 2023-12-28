using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField, Range(1,8)]
    int depth = 4;

    Fractal CreateChild(Vector3 direction, Quaternion rotation)
    {
        Fractal child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = 0.75f * direction;
        child.transform.localRotation = rotation; 
        child.transform.localScale = 0.5f * Vector3.one;
        return child;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Fractal " + depth;
        // base case
        if (depth <= 0)
        {
            return;
        }

        // recursive step
        Fractal childA = CreateChild(Vector3.up, Quaternion.identity);
        Fractal childB = CreateChild(Vector3.right, Quaternion.Euler(0.0f, 0.0f, -90.0f));
        Fractal childC = CreateChild(Vector3.left, Quaternion.Euler(0.0f, 0.0f, 90.0f));
        Fractal childD = CreateChild(Vector3.forward, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        Fractal childE = CreateChild(Vector3.back, Quaternion.Euler(-90.0f, 0.0f, 0.0f));

        childA.transform.SetParent(this.transform, false);
        childB.transform.SetParent(this.transform, false);
        childC.transform.SetParent(this.transform, false);
        childD.transform.SetParent(this.transform, false);
        childE.transform.SetParent(this.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 22.5f * Time.deltaTime, 0.0f);
    }
}
