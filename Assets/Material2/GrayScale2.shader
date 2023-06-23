Shader "Custom/CompositeShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _GridWidth ("Grid Width", Range(30, 120)) = 40
        _GridHeight ("Grid Height", Range(30, 120)) = 40
        _GrayscaleAmount("Grayscale amount", Range(0, 1)) = 1
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            sampler2D _MainTex;
            float _GridWidth;
            float _GridHeight;
            float _GrayscaleAmount;
 
            struct appdata_t {
                float4 vertex : POSITION;
 float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
		

                // Grid shader
                float2 gridSize = float2(_GridWidth, _GridHeight);
                float2 roundedUV = floor(i.uv * gridSize) / gridSize;
                fixed4 nearestColor = tex2D(_MainTex, roundedUV);

                // Grayscale shader
                fixed4 grayscaleColor = nearestColor;
                float gray = dot(grayscaleColor.rgb, float3(0.299, 0.587, 0.114));
                grayscaleColor = lerp(grayscaleColor, fixed4(gray, gray, gray, grayscaleColor.a), _GrayscaleAmount);

                // Combine two shaders
                fixed4 color = lerp(grayscaleColor, nearestColor, 0);
                return color;
            }
            ENDCG
        }
    }
 
    FallBack "Diffuse"
}