Shader "Fractals/Mandelbrot" {
	Properties {

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.3
		_Metallic ("Metallic", Range(0,1)) = 0.9
		_Zoom ("Zoom", Range(0,40000)) = 30000
		_Pan ("iPan", Vector) = (0,0,0,0)
		_Aspect ("Aspect Ratio", float) = 0.5
		_Iterations ("Iterations", Range(1,600)) = 100
		_Hue("Hue", Vector) = (0,0.6,1)
		_R("R",float) = 5
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
		// fixed4 _Color;
		float4 _Pan;
		float _Zoom;
		float _Iterations;
		float _Aspect;
		float3 _Hue;
		float _R;
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			 float2 c = 0.0001*(IN.uv_MainTex-0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy;

			  float m = 0;
				int i;
    			float2 z = c;
    			for(i=0; i<_Iterations; i++) {
        			float x = (z.x * z.x - z.y * z.y) + c.x;
        			float y = (z.y * z.x + z.x * z.y) + c.y;
					m++;
        			if((x * x + y * y) > 4.0) 
					break;
        			z.x = x;
        			z.y = y;
    				}


			 float4 color;
if (m == floor(_Iterations))
{
	color = float4(0,0,0,1);
}
else
//color = float4(sin(m/4),sin(m/5),sin(m/7),1)/4+0.75;
color = 0.5 + 0.5*cos( 3.0 + m*0.15 + float4(_Hue.x,_Hue.y,_Hue.z,1.0));

			o.Albedo = color;
			//o.Emision = color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Alpha = color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
