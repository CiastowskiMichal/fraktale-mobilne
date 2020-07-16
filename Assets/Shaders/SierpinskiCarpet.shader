Shader "Fractals/SierpinskiCarpet" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.3
		_Metallic ("Metallic", Range(0,1)) = 0.9
		_iResolution("iResolution", Vector) = (1,1,1)
		_Pan("iPan", Vector) = (1,1,1)
		_Iterations("Iterations", Range(1,20)) = 7
		_Aspect ("Aspect Ratio", float) = 1
		_Hue("Hue", Vector) = (0,5,10)
		_Zoom("Zoom",Range(0,1200)) = 450
		_APoints("APoints", Vector) = (2,2,4,0)
		_BPoints("BPoints", Vector) = (2,-2,-4,0)
		_CPoints("CPoints", Vector) = (-2,2,0,4)
		_DPoints("DPoints", Vector) = (-2,-2,0,-4)
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

		float3 _iResolution;
		float2 _Pan;
		float _Iterations;
		float _Aspect;
		float3 _Hue;
		float _Zoom;
		float4 _APoints;
		float4 _BPoints;
		float4 _CPoints;
		float4 _DPoints;
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

	
		

// return distance and address
		




		void surf (Input IN, inout SurfaceOutputStandard o) {

			const float2 va = float2( _APoints.x, _APoints.y );
			const float2 vb = float2( _BPoints.x, _BPoints.y );
			const float2 vc = float2( _CPoints.x, _CPoints.y );
			const float2 vd = float2( _DPoints.x, _DPoints.y );
			const float2 vaa = float2( _APoints.z, _APoints.w );
			const float2 vbb = float2( _BPoints.z, _BPoints.w );
			const float2 vcc = float2( _CPoints.z, _CPoints.w );
			const float2 vdd = float2( _DPoints.z, _DPoints.w );
			float2 uv = 0.01*(IN.uv_MainTex.xy - 0.5)*_Zoom * float2(1, _Aspect) - _Pan.xy;

			float2 r;

			float a = 0.0;
			float2 c;
			float dist, d, t;
			for( int i=0; i<_Iterations; i++ )
		{
			d = dot(uv-va,uv-va);                 
			c = va; 
			dist=d; 
			t=0.0;
        	d = dot(uv-vb,uv-vb); 
			if (d < dist) 
				{ 
					c = vb; 
					dist=d; 
					t=1.0; 
				}
        	d = dot(uv-vc,uv-vc); 
			if (d < dist) 
				{ 
					c = vc; 
					dist=d; 
					t=2.0; 
				}
				d = dot(uv-vd,uv-vd); 
			if (d < dist) 
				{ 
					c = vd; 
					dist=d; 
					t=4.0; 
				}
			uv = c + 2.0*(uv - c);
			a = t + a*3.0;


d = dot(uv-vaa,uv-vaa);                 
			c = vaa; 
			dist=d; 
			t=0.0;
        	d = dot(uv-vbb,uv-vbb); 
			if (d < dist) 
				{ 
					c = vbb; 
					dist=d; 
					t=1.0; 
				}
        	d = dot(uv-vcc,uv-vcc); 
			if (d < dist) 
				{ 
					c = vcc; 
					dist=d; 
					t=2.0; 
				}
				d = dot(uv-vdd,uv-vdd); 
			if (d < dist) 
				{ 
					c = vdd; 
					dist=d; 
					t=4.0; 
				}
			uv = c + 2.0*(uv - c);
			a = t + a*3.0;



		}
r = float2( dot(uv,uv)/pow(2.0, 7.0), a/pow(3.0,7.0) );

	
			float3 col = 0.5 + 0.5*sin( 3.1416*r.y + _Hue );
			col *= 1.0 - smoothstep( 0.0, 0.02, r.x );
	
			//fragColor = vec4( col, 1.0 );
			o.Albedo = float4( col, 1.0 );
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * col;
			//o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
