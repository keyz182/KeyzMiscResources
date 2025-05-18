/// Based on https://www.shadertoy.com/view/MtcGD7#
Shader "Unlit/Fire"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

        SubShader
    {
       Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        GrabPass{ "_GrabTexture" }

        Pass
            {
                ZTest On
                ZWrite Off
                Blend One Zero
                Lighting Off
                Fog{ Mode Off }

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
                float4 uv2 : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _GrabTexture;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float3 rgb2hsv(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 hsv2rgb(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            float rand(float2 n) {
                return frac(sin(cos(dot(n, float2(12.9898,12.1414)))) * 83758.5453);
            }

            float noise(float2 n) {
                const float2 d = float2(0.0, 1.0);
                float2 b = floor(n);
                float2 f = smoothstep(float2(0.0, 0.0), float2(1.0, 1.0), frac(n));
                return lerp(lerp(rand(b), rand(b + d.yx), f.x), lerp(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
            }

            float fbm(float2 n) {
                float total = 0.0, amplitude = 1.0;
                for (int i = 0; i <5; i++) {
                    total += noise(n) * amplitude;
                    n += n*1.7;
                    amplitude *= 0.47;
                }
                return total;
            }

           v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv2 = ComputeGrabScreenPos(UnityObjectToClipPos(v.vertex));
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                fixed4 bg = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv2));
                fixed4 tex = tex2D(_MainTex, UNITY_PROJ_COORD(i.uv));

                fixed4 fragCoord = i.vertex;

                const float3 c1 = float3(0.5, 0.0, 0.1);
                const float3 c2 = float3(0.9, 0.1, 0.0);
                const float3 c3 = float3(0.2, 0.1, 0.7);
                const float3 c4 = float3(1.0, 0.9, 0.1);
                const float3 c5 = float3(0.1, 0.1, 0.1);
                const float3 c6 = float3(0.9, 0.9, 0.9);

                float2 speed = float2(1.2, 0.1);
                float shift = 1.327+sin(_Time.y*2.0)/2.4;
                float alpha = tex.a;

                //change the constant term for all kinds of cool distance versions,
                //make plus/minus to switch between
                //ground fire and fire rain!
                float dist = 3.5-sin(_Time.y*0.4)/1.89;

                float2 p = fragCoord.xy * dist / _ScreenParams.xx;
                p.x -= _Time.y/1.1;
                float q = fbm(p - _Time.y * 0.01+1.0*sin(_Time.y)/10.0);
                float qb = fbm(p - _Time.y * 0.002+0.1*cos(_Time.y)/5.0);
                float q2 = fbm(p - _Time.y * 0.44 - 5.0*cos(_Time.y)/7.0) - 6.0;
                float q3 = fbm(p - _Time.y * 0.9 - 10.0*cos(_Time.y)/30.0)-4.0;
                float q4 = fbm(p - _Time.y * 2.0 - 20.0*sin(_Time.y)/20.0)+2.0;
                q = (q + qb - .4 * q2 -2.0*q3  + .6*q4)/3.8;
                float2 r = float2(fbm(p + q /2.0 + _Time.y * speed.x - p.x - p.y), fbm(p + q - _Time.y * speed.y));
                float3 c = lerp(c1, c2, fbm(p + r)) + lerp(c3, c4, r.x) - lerp(c5, c6, r.y);
                float3 color = float3(c * cos(shift * fragCoord.y / _ScreenParams.y));
                color += .05;
                color.r *= .8;
                float3 hsv = rgb2hsv(color);
                hsv.y *= hsv.z  * 1.1;
                hsv.z *= hsv.y * 1.13;
                hsv.y = (2.2-hsv.z*.9)*1.20;
                color = hsv2rgb(hsv);

                float4 genned = float4(color.x, color.y, color.z, alpha);

                return lerp(bg, genned, alpha);
            }
            ENDCG
        }
    }
}
