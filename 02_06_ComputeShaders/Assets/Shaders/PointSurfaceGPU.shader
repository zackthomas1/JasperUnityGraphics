Shader "Custom/PointSurfaceGPU"
{
    Properties
    {
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface ConfigureSurface Standard fullforwardshadows addshadow
        #pragma editor_sync_compilation     //Forces Unity to immediately compile shader before using it for the first time.
        #pragma target 4.5

        #include "PointGPU.hlsl"

        struct Input
        {
            float3 worldPos;
        };

        half _Smoothness;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        #pragma instancing_options assumeuniformscaling procedural:ConfigureProcedural

        void ConfigureSurface (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = saturate(IN.worldPos * 0.5 + 0.5);
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
