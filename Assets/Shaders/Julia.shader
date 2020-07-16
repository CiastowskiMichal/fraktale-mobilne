Shader "Fractals/Julia" {
	Properties {

		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.3
		_Metallic ("Metallic", Range(0,1)) = 0.9
		_Zoom ("Zoom", float) = 1
		_Pan ("Pan", Vector) = (1,1,1,1)
		_Aspect ("Aspect Ratio", float) = 0.5
		_Iterations ("Iterations", Range(1,128)) = 64
		_SeedX("SeedX", Range(-1,1)) = 0
		_SeedY("SeedY", Range(-1,1)) = 0
		_Hue("Hue", Vector) = (0,5,10)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _SeedX;
		float _SeedY;
		float4 _Pan;
		float _Zoom;
		float _Iterations;
		float _Aspect;
		float3 _Hue;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			
			float2 v = 0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy;
			float m = 0;

			 const float r = 5;

			 for (int n=0;n<_Iterations;++n)
			 {
				
				float x = (v.x * v.x - v.y *v.y) + _SeedX;
				float y = (2* v.x * v.y) + _SeedY;

				m++;
        		if((x * x + y * y) > 4.0) 
					break;
				v.x = x;
        		v.y = y;
			 } 
			 float4 color;
if (m== floor(_Iterations))
{
	color = float4(0,0,0,1);
}
else
//color = float4(sin(m/_Hue.x),sin(m/_Hue.y),sin(m/_Hue.z),1)/4+0.75;
color = 0.5 + 0.5*cos( 3.0 + m*0.15 + float4(_Hue.x,_Hue.y,_Hue.z,1.0));

			o.Albedo = color;
			//o.Emision = color;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			// Metallic and smoothness come from slider variables
			//o.Alpha = color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
