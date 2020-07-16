Shader "Fractals/DragonShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Zoom ("Zoom", Range(0,200)) = 100
		_Pan ("iPan", Vector) = (0,0,0,0)
		_Aspect ("Aspect Ratio", float) = 0.5
		_Iterations ("Iterations", Range(0,19.9)) = 0
		_Hue("Hue", Vector) = (0,5,10)
		//_uTime("Time",Range(0,19.9)) = 0
	}
	CGINCLUDE


	float alternate(float p, float d){;
	return sign(frac(p*d*.5)*2.-1.);
}

float3 rainbow(float t){
    return sin(t+float3(0,.33,.66)*6.28)*.5+.5;
}

float3 TwinDragon(float2 p, float utime,float3 _Hue1){
    float time       = frac(utime*0.05)*20.;
    
    //scaling
    p = (p*1-2)/1*1.5;
    
    //-------------the fractal stuff----------- ----THIS IS ANIMATIONS----(so remove them if you want)
	p.y += alternate(p.x, 4096. )/8192. * clamp(time-18.,0.,1.5)/1.5;
    p.x -= alternate(p.y, 2048. )/4096. * clamp(time-16.5,0.,1.5)/1.5;
	p.y += alternate(p.x, 1024. )/2048. * clamp(time-15.,0.,1.5)/1.5;
    p.x -= alternate(p.y, 512. )/1024. * clamp(time-13.5,0.,1.5)/1.5;
    p.y += alternate(p.x, 256. )/512. * clamp(time-12.,0.,1.5)/1.5;
    p.x -= alternate(p.y, 128. )/256. * clamp(time-10.5,0.,1.5)/1.5;
    p.y += alternate(p.x,  64. )/128. * clamp(time-9.,0.,1.5)/1.5;
    p.x -= alternate(p.y,  32. )/ 64. * clamp(time-7.5,0.,1.5)/1.5;
    p.y += alternate(p.x,  16. )/ 32. * clamp(time- 6.,0.,1.5)/1.5;
    p.x -= alternate(p.y,   8. )/ 16. * clamp(time- 4.5,0.,1.5)/1.5;
    p.y += alternate(p.x,   4. )/  8. * clamp(time- 3.,0.,1.5)/1.5;
    p.x -= alternate(p.y,   2. )/  4. * clamp(time- 1.5,0.,1.5)/1.5;

    // prettifying
    float2  block  = ceil(p+.5);               //index for blocks from which the fractal is shifted
    float3  color  = rainbow(block.x*_Hue1.x+block.y*_Hue1.y);  //rainbow palette using block index as t
    float dis    = length(frac(p+.5)*2.-1.);//distance to middle of block
          color *= .5+dis*.7;                    //using distance within block for some more pretty.
    
    return color;
}

	ENDCG
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
		float4 _Pan;
		float _Zoom;
		float _Iterations;
		float _Aspect;
		float3 _Hue;
		//float _uTime;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

// float alternate(float p, float d){
// 	return sign(fract(p*d*.5)*2.-1.);
// }



// float3 TwinDragon(float2 p){

    
//     //scaling
//     //p = (p*2.-iResolution.xy)/iResolution.y*1.5;
    
//     //-------------the fractal stuff----------- ----THIS IS ANIMATIONS----(so remove them if you want)
//     p.y += alternate(p.x, 256. )/512. ;
//     p.x -= alternate(p.y, 128. )/256. ;
//     p.y += alternate(p.x,  64. )/128. ;
//     p.x -= alternate(p.y,  32. )/ 64. ;
//     p.y += alternate(p.x,  16. )/ 32. ;
//     p.x -= alternate(p.y,   8. )/ 16. ;
//     p.y += alternate(p.x,   4. )/  8. ;
//     p.x -= alternate(p.y,   2. )/  4. ;

//     // prettifying
//     float2  block  = ceil(p+.5);               //index for blocks from which the fractal is shifted
//     float3  color  = sin(block.x*4.+block.y+float3(0,.33,.66)*6.28)*.5+.5;  //rainbow palette using block index as t
//     float dis    = length(fract(p+.5)*2.-1.);//distance to middle of block
//           color *= .5+dis*.7;                    //using distance within block for some more pretty.
    
//     return color;
// }



		void surf (Input IN, inout SurfaceOutputStandard o) {


			//float2 uv = 0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy;

			float2 d = float2(0,0);
    
    //some antialiasing
    	float3 col = (
        TwinDragon(0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy+d.xy,_Iterations,_Hue)+
        TwinDragon(0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy,_Iterations,_Hue)+
        TwinDragon(0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy,_Iterations,_Hue)+
        TwinDragon(0.05*(IN.uv_MainTex - 0.5) * _Zoom * float2(1, _Aspect) - _Pan.xy,_Iterations,_Hue)
    	)*.25;

			
	//fragColor = float4( col, 1.0 );


			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// o.Albedo = c.rgb;
			o.Albedo = float4( col, 1.0 );
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
