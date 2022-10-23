Shader "Unlit/NewUnlitShader"
{
    Properties
    {
                _CellColour("Cell Colour", Color) = (1,1,1,1)
        [HDR]_BorderColour("Border Colour", Color) = (1,1,1,1)
        _BorderWidth("Border width", Range(0,1)) = 0.1
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _CellColour;
            fixed4 _BorderColour;
            float _BorderWidth;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,0);
                if (i.uv.x < _BorderWidth || i.uv.x > 1 - _BorderWidth || i.uv.y < _BorderWidth || i.uv.y > 1 - _BorderWidth)
                {
                    col = _BorderColour;
                }
                else
                {
                    col = _CellColour;

                }
                return col;
            }
            ENDCG
        }
    }
}
