Shader "MagicaVoxel/FlatColor" {
	Properties {

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		pass {

			CGPROGRAM

			#pragma vertex vertPass 
			#pragma fragment fragPass 

			#include "UnityCG.cginc"
			
			struct VertOut
			{
				float4 position : POSITION;
				float4 color : COLOR;
			};

			struct VertIn
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			VertOut vertPass(VertIn input, float3 normal : NORMAL)
			{
				VertOut output;
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.color = input.color;
				return output;
			}

			struct FragOut
			{
				float4 color : COLOR;
			};

			FragOut fragPass(float4 color : COLOR)
			{
				FragOut output;
				output.color = color;
				return output;
			}

			ENDCG

		}

	} 
	FallBack "Diffuse"
}
