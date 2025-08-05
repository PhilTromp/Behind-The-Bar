Shader "Unlit/Background Screen"
{
    Properties
    {
        _Color1 ("Start Color", Color) = (1,0,0,1) // Rot
        _Color2 ("End Color", Color) = (0,0,1,1)   // Blau
        _GradientDirection ("Gradient Direction (0 = Vertical, 1 = Horizontal)", Range(0,1)) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "Queue"="Geometry" 
            "UniversalMaterialType" = "Unlit"
        }

        Pass
        {
            Name "GradientPass"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM

            // URP-Shader-Definitionen
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION; // Objekt-Koordinaten
                float2 uv : TEXCOORD0;        // UV-Koordinaten
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color1;
            float4 _Color2;
            float _GradientDirection; // 0 = Vertikal, 1 = Horizontal

            // Vertex Shader
            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            // Fragment Shader
            half4 frag (Varyings IN) : SV_Target
            {
                // Wähle die Richtung des Farbverlaufs
                float gradientFactor = lerp(IN.uv.y, IN.uv.x, _GradientDirection);
                
                // Interpoliert zwischen den Farben
                return lerp(_Color1, _Color2, gradientFactor);
            }
            ENDHLSL
        }
    }
}
