Shader "Bakery/Skybox" {
	Properties {
		_Tint ("Tint Color", Vector) = (0.5,0.5,0.5,0.5)
		_Exposure ("Exposure", Float) = 1
		_MatrixRight ("Right", Vector) = (1,0,0,0)
		_MatrixUp ("Up", Vector) = (0,1,0,0)
		_MatrixForward ("Forward", Vector) = (0,0,1,0)
		[NoScaleOffset] _Tex ("Cubemap   (HDR)", Cube) = "white" {}
		_NoTexture ("No texture", Float) = 0
		_Hemispherical ("Hemispherical", Float) = 0
	}
	// Restored replacement for the AssetRipper placeholder.
	SubShader {
		Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
		Cull Off ZWrite Off
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			samplerCUBE _Tex; half4 _Tex_HDR; half4 _Tint; half _Exposure;
			float4 _MatrixRight, _MatrixUp, _MatrixForward; half _NoTexture;
			struct v2f { float4 pos : SV_POSITION; float3 dir : TEXCOORD0; };
			v2f vert (appdata_base v) {
				v2f o; o.pos = UnityObjectToClipPos(v.vertex);
				float3 d = v.vertex.xyz;
				o.dir = float3(dot(d, _MatrixRight.xyz), dot(d, _MatrixUp.xyz), dot(d, _MatrixForward.xyz));
				return o;
			}
			half4 frag (v2f i) : SV_Target {
				half3 c = _NoTexture > 0.5 ? half3(1,1,1) : DecodeHDR(texCUBE(_Tex, i.dir), _Tex_HDR);
				c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb * _Exposure;
				return half4(c, 1);
			}
			ENDCG
		}
	}
	Fallback Off
}
