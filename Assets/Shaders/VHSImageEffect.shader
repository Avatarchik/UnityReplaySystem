Shader "VHS"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_VHSTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _VHSTex;
			
			fixed4 frag (v2f_img i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 vhs = tex2D(_VHSTex, i.uv);
				
				float lum = col.r*.3 + col.g*.59 + col.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
				float4 result = col;
				result.rgb = lerp(col.rgb, bw, 1);
				
				return result + vhs;
			}
			ENDCG
		}
	}
}
