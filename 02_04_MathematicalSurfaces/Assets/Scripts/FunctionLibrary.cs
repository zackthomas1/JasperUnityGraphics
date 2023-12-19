using UnityEngine;
using static FunctionLibrary;
using UnityEngine.UIElements;
using static UnityEngine.Mathf;
public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    private static Function[] functions = { 
        PlaneXZ, 
        Wave, 
        MultiWave, 
        Riple, 
        SphereSphericalCoords, 
        Cylinder, 
        Sphere, 
        SphereCollapseRadius,
        EvenDistribtionSphere,
        VaryingUSphere,
        VaryingVSphere,
        TwistingSphere,
        Torus,
        TwistingTorus
    };
    public enum FunctionName { 
        PlaneXZ,  
        Wave, 
        MultiWave, 
        Riple, 
        SphereSphericalCoords, 
        Cylinder, 
        Sphere, 
        SphereCollapseRadius, 
        EvenDistribtionSphere,
        VaryingUSphere,
        VaryingVSphere,
        TwistingSphere,
        Torus,
        TwistingTorus
    };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Vector3 PlaneXZ(float u, float v, float t)
    {
        return new Vector3(u, 0.0f, v);
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        return new Vector3(u, Mathf.Sin(Mathf.PI * (u + v + t)), v);
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (u + (0.5f*t)));
        y += (0.5f) * Mathf.Sin(2.0f * Mathf.PI * (u + t));
        y += Mathf.Sin(Mathf.PI * (u + v + 0.25f * t));
        y = y * (2.0f / 3.0f);
        return new Vector3(u, y, v);
    }

    public static Vector3 Riple(float u, float v, float t)
    {
        float d = Mathf.Sqrt(u*u + v*v); 
        float y = Mathf.Sin( Mathf.PI * (4.0f * d - t));
        y = y / (1.0f + 10.0f * d);
        return new Vector3(u, y, v);
    }

    public static Vector3 SphereSphericalCoords(float u, float v, float t)
    {
        Vector3 p;
        float theta = 2.0f * Mathf.PI * (u+t*0.1f);
        float phi = Mathf.PI * v;
        p.x = Mathf.Sin(phi) * Mathf.Cos(theta);
        p.y = Mathf.Cos(phi);
        p.z = Mathf.Sin(phi) * Mathf.Sin(theta);

        return p;
    }
    
    public static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        p.x = Mathf.Sin(Mathf.PI * u);
        p.y = v;
        p.z = Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 SphereCollapseRadius(float u, float v, float t)
    {
        Vector3 p;
        float r = Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = r * Mathf.Sin(u * Mathf.PI);
        p.y = v;
        p.z = r * Mathf.Cos(Mathf.PI * u);

        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = Mathf.Cos(0.5f * Mathf.PI * v) ;
        p.x = r * Mathf.Sin(u * Mathf.PI);
        p.y = Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = r * Mathf.Cos(Mathf.PI * u);

        return p;
    }

    public static Vector3 EvenDistribtionSphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.5f + 0.5f * Mathf.Sin(Mathf.PI * t * 0.25f);
        float s = r * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 VaryingUSphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Mathf.Sin(8.0f * Mathf.PI * u);
        float s = r * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 VaryingVSphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Mathf.Sin(8.0f * Mathf.PI * v);
        float s = r * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }
    public static Vector3 TwistingSphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Mathf.Sin(Mathf.PI * (6.0f * u + 4.0f * v + t));
        float s = r * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r * Mathf.Sin(0.5f * Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.75f;
        float r2 = 0.25f;
        float s = r1 + r2 * Mathf.Cos(Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r2 * Mathf.Sin(Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 TwistingTorus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.7f + 0.1f * Mathf.Sin(Mathf.PI*(6.0f * u + t * 0.5f));
        float r2 = 0.15f + 0.05f * Mathf.Sin(Mathf.PI * (8.0f * u + 4.0f * v + 2.0f * t));
        float s = r1 + r2 * Mathf.Cos(Mathf.PI * v);
        p.x = s * Mathf.Sin(Mathf.PI * u);
        p.y = r2 * Mathf.Sin(Mathf.PI * v);
        p.z = s * Mathf.Cos(Mathf.PI * u);
        return p;
    }
}