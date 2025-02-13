Shader "Custom/BloodSplatterShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BloodTex ("Blood Texture", 2D) = "white" {}
        _BloodColor ("Blood Color", Color) = (1, 0, 0, 1)
        _BloodAmount ("Blood Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _BloodTex;
            float _BloodAmount;
            float4 _BloodColor;
            float2 _MainTex_TexelSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Fetch the base texture
                half4 baseTex = tex2D(_MainTex, i.uv);
                // Fetch the blood texture
                half4 bloodTex = tex2D(_BloodTex, i.uv);
                
                // Blending the blood with the base texture
                half4 result = lerp(baseTex, bloodTex * _BloodColor, _BloodAmount);
                
                return result;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
