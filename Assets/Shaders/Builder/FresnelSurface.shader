Shader "Custom/FresnelSurface" {
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

		CGPROGRAM
		
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		
		struct Input {
			float3 viewDir;
		};

		float4 _InnerColor;
		float4 _RimColor;
		float _RimPower;
		float _AmbientFactor;
		
		
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _InnerColor;

			//half viewDotNorm = dot(normalize(IN.viewDir), o.Normal);

			//half power = 0;

			//if(viewDotNorm >= 0){
			//	half rim = 1.0 - viewDotNorm;
			//	power = pow(rim, _RimPower) * (1 - _BackFaceFactor);
			//} else {
			//	power = _BackFaceFactor;
			//}
			
			//o.Emission = _RimColor.rgb * power;
			
			//half rim = 1.0 - abs(dot(normalize(IN.viewDir), o.Normal));
			//rim = _AmbientFactor + (1 - _AmbientFactor) * rim;

			//half power = pow(rim, _RimPower);
			//power = clamp(power, 0.1, 1.0);
			half rim = 1.0 - abs(dot(normalize(IN.viewDir), o.Normal));
			// rim = _AmbientFactor + (1 - _AmbientFactor) * rim;

			half power = pow(rim, _RimPower);
			power = _AmbientFactor + (1 - _AmbientFactor) * power;

			o.Emission = _RimColor.rgb * power;
		}
		ENDCG
	}
	FallBack "Diffuse"
}