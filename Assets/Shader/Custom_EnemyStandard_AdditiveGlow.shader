Shader "Custom/EnemyStandard_AdditiveGlow" {
	Properties {
		_MainTex ("Base (RGB) + Alpha", 2D) = "white" {}
		_Color ("Base Color", Vector) = (1,1,1,1)
		_GlowTex ("Glow Texture (RGB)", 2D) = "black" {}
		_GlowColor ("Glow Color", Vector) = (1,0.5,0.2,1)
		_GlowIntensity ("Glow Intensity", Range(0, 10)) = 1
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
	}
	// Restored replacement for the AssetRipper placeholder.
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		sampler2D _MainTex; sampler2D _GlowTex;
		fixed4 _Color; fixed4 _GlowColor; half _GlowIntensity; half _Glossiness; half _Metallic;
		struct Input { float2 uv_MainTex; };
		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb; o.Alpha = c.a;
			o.Metallic = _Metallic; o.Smoothness = _Glossiness;
			o.Emission = tex2D(_GlowTex, IN.uv_MainTex).rgb * _GlowColor.rgb * _GlowIntensity;
		}
		ENDCG
	}
	Fallback "Standard"
}
