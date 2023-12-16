using UnityEngine;
using static FunctionLibrary;
using UnityEngine.UIElements;
using static UnityEngine.Mathf;
public static class FunctionLibrary
{
    public delegate float Function(float x, float t);

    private static Function[] functions = { Wave, MultiWave, Riple};
    public enum FunctionName {  Wave, MultiWave, Riple };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static float Wave(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x+t));
    }

    public static float MultiWave(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + (0.5f*t)));
        y += (0.5f) * Mathf.Sin(2.0f * Mathf.PI * (x + t));
        return y * (2.0f/3.0f);
    }

    public static float Riple(float x, float t)
    {
        float d = Mathf.Abs(x); 
        float y = Mathf.Sin( Mathf.PI * (4.0f * d -t));
        return y / (1.0f + 10.0f * d);
    }
}