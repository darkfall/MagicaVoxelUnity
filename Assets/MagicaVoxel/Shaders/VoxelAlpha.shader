Shader "MagicaVoxel/VoxelAlpha"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Color (RGB) Trans (A)", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue" = "Transparent"}

        Pass
        {
            //ZWrite On
            ColorMask 0
        }

        ZWrite Off // don't write to depth buffer 
        Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
       
        uniform float4 _Color;
        uniform sampler2D _MainTex;

        struct Input
        {
            float4 color : COLOR;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 col = IN.color * _Color;
            o.Albedo = col.rgb;
            o.Alpha = col.a;
        }
        ENDCG
    }
    Fallback "Glossy", 0
}
