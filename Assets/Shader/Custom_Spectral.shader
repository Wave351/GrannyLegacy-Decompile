Shader "Custom/Spectral" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint + Transparency", Vector) = (1,1,1,1)
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
	}
	// Restored replacement for the AssetRipper placeholder.
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
		LOD 200
		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0
		sampler2D _MainTex; fixed4 _Color;
		struct Input { float2 uv_MainTex; };
		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb; o.Alpha = c.a; o.Smoothness = 0.2;
		}
		ENDCG
	}
	Fallback "Transparent/Diffuse"
}
