Shader "Custom/LavaSimple" {
	Properties {
		_LavaTex ("Lava", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "gray" {}
		_FlowSpeed ("Flow Speed", Float) = 0.3
		_Distortion ("Distortion", Float) = 0.05
		_Pulse ("Pulse Strength", Float) = 1
		_EmissionStrength ("Emission", Float) = 2
	}
	// Restored replacement for the AssetRipper placeholder.
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			sampler2D _LavaTex; float4 _LavaTex_ST; sampler2D _NoiseTex;
			float _FlowSpeed, _Distortion, _Pulse, _EmissionStrength;
			struct v2f { float4 pos : SV_POSITION; float2 uv : TEXCOORD0; UNITY_FOG_COORDS(1) };
			v2f vert (appdata_base v) { v2f o; o.pos = UnityObjectToClipPos(v.vertex); o.uv = TRANSFORM_TEX(v.texcoord, _LavaTex); UNITY_TRANSFER_FOG(o, o.pos); return o; }
			fixed4 frag (v2f i) : SV_Target {
				float t = _Time.y * _FlowSpeed;
				float2 n = tex2D(_NoiseTex, i.uv + float2(t, t * 0.7)).rg - 0.5;
				fixed4 c = tex2D(_LavaTex, i.uv + float2(0, t) + n * _Distortion);
				c.rgb *= _EmissionStrength * 0.5 * (1 + 0.2 * _Pulse * sin(_Time.y * 2));
				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
