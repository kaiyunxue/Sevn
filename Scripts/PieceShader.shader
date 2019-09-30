
Shader "Unlit/PieceShader"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {} 
		_BackTex("Back Texture", 2D) = "white" {} 
		_BackTex60("Back Texture60", 2D) = "white" {}
		_Rate("Rate", Range(0, 1)) = 0

	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		// ZWrite Off
		// Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			sampler2D _BackTex;
			sampler2D _BackTex60;
			float4 _MainTex_ST;
			float4 _Color;
			fixed4 _MainColor;
			fixed4 _BackColor;
			float _Rate;

			v2f vert(appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 backCol = tex2D(_BackTex, i.texcoord);
				fixed4 backCol60 = tex2D(_BackTex60, i.texcoord);
				fixed4 texCol = tex2D(_MainTex, i.texcoord);
				fixed4 col = texCol;
				col.rgb = col.rgb * (_Rate * backCol.rgb + (1 - _Rate) * backCol60);
				col = col * _Color;
				col.a = 0.0;
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}