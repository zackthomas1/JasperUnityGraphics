using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GPUGraph : MonoBehaviour
{

    const int maxResolution = 1000;
    [SerializeField, Range(10, maxResolution)]
    int resolution = 500;

    public enum FunctionName { Wave, MultiWave, Ripple, Sphere, TwistingSphere, TwistingTorus};
    [SerializeField]
    FunctionName function = FunctionName.Wave;

    FunctionName previousFunction;
    float duration = 0.0f;
    bool transitioning;
    float holdDuration = 4.0f;
    float transitionDuration = 2.0f;
    int kernelIndex;
    [SerializeField]
    bool cycle = false;

    [SerializeField]
    ComputeShader computeShader;

    [SerializeField]
    Material material;

    [SerializeField]
    Mesh mesh;

    ComputeBuffer positionsBuffer;

    static readonly int positionsId = Shader.PropertyToID("_Positions");
    static readonly int resolutionId = Shader.PropertyToID("_Resolution");
    static readonly int stepId = Shader.PropertyToID("_Step");
    static readonly int timeId = Shader.PropertyToID("_Time");
    static readonly int transitionProgressId = Shader.PropertyToID("_TransitionProgress");

    void UpdateFunctionOnGPU ()
    {
        float step = 2.0f / resolution;
        int groups = Mathf.CeilToInt(resolution / 8.0f);

        computeShader.SetInt(resolutionId, resolution);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);

        if (cycle)
        {
            duration += Time.deltaTime;


            if (transitioning)
            {

                if (duration >= transitionDuration)
                {
                    duration -= transitionDuration;
                    transitioning = false;
                }
            }
            else if(duration >= holdDuration)
            {
                duration -= holdDuration;
                transitioning = true;
                previousFunction = function;
                function = function == FunctionName.TwistingTorus ? FunctionName.Wave : function + 1;
            }

            if (transitioning)
            {
                computeShader.SetFloat(transitionProgressId, Mathf.SmoothStep(0.0f, 1.0f, duration / transitionDuration));

                kernelIndex = (int)previousFunction * 6 + 1;
                computeShader.SetBuffer(kernelIndex, positionsId, positionsBuffer);
                computeShader.Dispatch(kernelIndex, groups, groups, 1);
            }
            else
            {
                transitioning = false;
                computeShader.SetFloat(transitionProgressId, 0.0f);

                kernelIndex = (int)function + (int)function * 6;
                computeShader.SetBuffer(kernelIndex, positionsId, positionsBuffer);
                computeShader.Dispatch(kernelIndex, groups, groups, 1);
            }
        }
        else
        {
            kernelIndex = (int)function + (int)function * 6;
            computeShader.SetBuffer(kernelIndex, positionsId, positionsBuffer);
            computeShader.Dispatch(kernelIndex, groups, groups, 1);
        }

        material.SetBuffer(positionsId, positionsBuffer);
        material.SetFloat(stepId, step);

        Bounds bounds = new Bounds(Vector3.zero, Vector3.one * (2.0f + 2.0f / resolution));
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, resolution * resolution);
    }

    void OnEnable()
    {
        positionsBuffer = new ComputeBuffer(maxResolution * maxResolution, 3 * 4);
    }

    private void OnDisable()
    {
        positionsBuffer.Release();
        positionsBuffer = null; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunctionOnGPU();
    }

}
