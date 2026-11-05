Shader "ProBuilder/Standard Vertex Color" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
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
		sampler2D _MainTex; fixed4 _Color; half _Glossiness; half _Metallic;
		struct Input { float2 uv_MainTex; float4 color : COLOR; };
		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color * IN.color;
			o.Albedo = c.rgb; o.Alpha = c.a; o.Metallic = _Metallic; o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	Fallback "Diffuse"
}
