Shader "Custom/GeometryGrass"
{
    Properties
    {
        _BottomColor("Bottom Color", Color) = (0,1,0,1)
        _TopColor("Top Color", Color) = (1,1,0,1)
        _GrassHeight("Grass Height", Float) = 1
        _GrassWidth("Grass Width", Float) = 0.06
        _RandomHeight("Grass Height Randomness", Float) = 0.25
        _WindSpeed("Wind Speed", Float) = 100
        _WindStrength("Wind Strength", Float) = 0.05
        _Radius("Interactor Radius", Float) = 0.3
        _Strength("Interactor Strength", Float) = 5
        _Rad("Blade Radius", Range(0,1)) = 0.6
        _BladeForward("Blade Forward Amount", Float) = 0.38
        _BladeCurve("Blade Curvature Amount", Range(1, 4)) = 2
        _AmbientStrength("Ambient Strength", Range(0,1)) = 0.5
        _MinDist("Min Distance", Float) = 40
        _MaxDist("Max Distance", Float) = 60
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
                float4 tangent : TANGENT;
            };

            struct v2g
            {
                float4 pos : SV_POSITION;
                float3 norm : NORMAL;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 tangent : TANGENT;
            };

            half _GrassHeight;
            half _GrassWidth;
            half _WindSpeed;
            float _WindStrength;
            half _Radius, _Strength;
            float _Rad;

            float _RandomHeight;
            float _BladeForward;
            float _BladeCurve;

            float _MinDist, _MaxDist;

            uniform float3 _PositionMoving;

            v2g vert(Attributes v)
            {
                v2g OUT;
                OUT.pos = v.positionOS;
                OUT.norm = TransformObjectToWorldNormal(v.normal);
                OUT.uv = v.texcoord;
                OUT.color = v.color;
                OUT.tangent = v.tangent;
                return OUT;
            }

            float rand(float3 co)
            {
                return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
            }

            float3x3 AngleAxis3x3(float angle, float3 axis)
            {
                float c, s;
                sincos(angle, s, c);

                float t = 1 - c;
                float x = axis.x;
                float y = axis.y;
                float z = axis.z;

                return float3x3(
                    t * x * x + c, t * x * y - s * z, t * x * z + s * y,
                    t * x * y + s * z, t * y * y + c, t * y * z - s * x,
                    t * x * z - s * y, t * y * z + s * x, t * z * z + c
                );
            }

            float3 GetWindEffect(float3 vertexPos)
            {
                return float3(
                    sin(_Time.x * _WindSpeed + vertexPos.x) + sin(_Time.x * _WindSpeed + vertexPos.z * 2),
                    0,
                    cos(_Time.x * _WindSpeed + vertexPos.x * 2) + cos(_Time.x * _WindSpeed + vertexPos.z)
                ) * _WindStrength;
            }

            struct g2f
            {
                float4 pos : SV_POSITION;
                float3 norm : NORMAL;
                float2 uv : TEXCOORD0;
                float3 diffuseColor : COLOR;
                float3 worldPos : TEXCOORD3;
                float fogFactor : TEXCOORD5;
            };

            g2f GrassVertex(float3 vertexPos, float width, float height, float offset, float curve, float2 uv, float3x3 rotation, float3 faceNormal, float3 color) {
                g2f OUT;
                float3 offsetvertices = vertexPos + mul(rotation, float3(width, height, curve) + float3(0, 0, offset));

                OUT.pos = TransformWorldToHClip(offsetvertices);
                OUT.norm = faceNormal;
                OUT.diffuseColor = color;
                OUT.uv = uv;
                OUT.worldPos = offsetvertices;
                OUT.fogFactor = ComputeFogFactor(OUT.pos.z);
                return OUT;
            }

            [maxvertexcount(48)]
            void geom(point v2g IN[1], inout TriangleStream<g2f> triStream)
            {
                float forward = rand(IN[0].pos.yyz) * _BladeForward;
                float3 faceNormal = float3(0, 1, 0);
                float3 worldPos = TransformObjectToWorld(IN[0].pos.xyz);

                float distanceFromCamera = distance(worldPos, _WorldSpaceCameraPos);
                float distanceFade = 1 - saturate((distanceFromCamera - _MinDist) / _MaxDist);

                float3 windEffect = GetWindEffect(IN[0].pos.xyz);

                for (int j = 0; j < 4; j++)
                {
                    float3x3 facingRotationMatrix = AngleAxis3x3(rand(IN[0].pos.xyz) * TWO_PI + j, float3(0, 1, -0.1));
                    float3x3 transformationMatrix = facingRotationMatrix;

                    faceNormal = mul(faceNormal, transformationMatrix);
                    float radius = j / 4.0f;
                    float offset = (1 - radius) * _Rad;

                    for (int i = 0; i < 5; i++)
                    {
                        float t = i / 5.0f;
                        float segmentHeight = _GrassHeight * t;
                        float segmentWidth = _GrassWidth * (1 - t);
                        segmentWidth = i == 0 ? _GrassWidth * 0.3 : segmentWidth;

                        float segmentForward = pow(abs(t), _BladeCurve) * forward;

                        float3x3 transformMatrix = i == 0 ? facingRotationMatrix : transformationMatrix;

                        float3 newPos = worldPos + windEffect * t;

                        if (newPos.y >= 0.35 && newPos.y <= 8.0) {
                            triStream.Append(GrassVertex(newPos, segmentWidth, segmentHeight, offset, segmentForward, float2(0, t), transformMatrix, faceNormal, IN[0].color.rgb));
                        }
                    }
                }
            }

            half4 frag(g2f IN) : SV_Target
            {
                return half4(IN.diffuseColor, 1.0);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
