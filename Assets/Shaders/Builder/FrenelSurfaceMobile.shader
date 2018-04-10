Shader "Custom/FresnelSurfaceMobile" {
	Properties {
		_InnerColor ("Inner Color", Color) = (1,1,1,1)
		_RimColor ("Rim Color", Color) = (0.26, 0.19, 0.16, 0.0)
		_RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
		_AmbientFactor("Ambient Factor", Range(0.0, 1.0)) = 0.5
	}
	SubShader {
		Tags 
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector" = "True" 
			"RenderType" = "Transparent"
		}

		LOD 200

		Cull Off
		Blend OneMinusDstColor One
		Lighting Off
		ZWrite Off

		Pass
		{
			CGPROGRAM

			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
		
			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD0;
				float3 objectPos : TEXCOORD1;
			};

			float4 _InnerColor;
			float4 _RimColor;
			float _RimPower;
			float _AmbientFactor;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.objectPos = v.vertex.xyz;
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(UnityWorldSpaceViewDir(mul(unity_ObjectToWorld, v.vertex)));

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half rim = 1.0 - abs(dot(normalize(i.viewDir), i.normal));
				// rim = _AmbientFactor + (1 - _AmbientFactor) * rim;

				half power = pow(rim, _RimPower);
				power = _AmbientFactor + (1 - _AmbientFactor) * power;

				fixed4 col = _RimColor * power;
				return col;
			}
		
		
			ENDCG
		}
		
	}
	FallBack "Transparent/VertexLit"
}