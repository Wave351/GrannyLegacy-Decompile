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
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
}