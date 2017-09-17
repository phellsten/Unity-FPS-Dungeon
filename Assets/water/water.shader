// Jack Kaloger 2017
// Translucent Phong illumination shader for COMP30019
Shader "Water/basic" {
    Properties {
        // basics
        _Color ("Colour", Color) = (1,1,1,1)
        _Opacity ("Opacity", Float) = 1

        // specular props
        _Specular ("Specular Colour", Color) = (1,1,1,1) 
        _Gloss ("Gloss (smaller == bigger specular)", Float) = 10
    }
    SubShader {
        Pass {  
            Tags { "LightMode" = "ForwardBase" } 

            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag 

            #include "UnityCG.cginc"

            // global var for directional light colour
            // (Our sun)
            uniform float4 _LightColor0; 

            uniform float4 _Color;
            uniform float4 _Opacity;

            // specular values
            uniform float4 _Specular;
            uniform float _Gloss;

            struct vIn {
                float4 pos : POSITION;
                float3 norm : NORMAL;
            };
            struct vOut {
                float4 pos : SV_POSITION;
                float4 col : COLOR;
            };

            vOut vert(vIn input) {
                vOut o;

                // we need the normal from the vertex/surface, transformed from world coords
                float3 normDir = normalize( mul(input.norm, unity_WorldToObject) );
                // we also need the direction of our camera
                float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, input.pos).xyz);

                // since we're only using a directional light, grab its direction straight from the constant
                // (attenuation value of the directional light will be 1.0 so we can ignore it..)
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // PHONG //

                // Ambient component radiated light intensity
                // here, Ia is grabbed from the unity lightmodel
                // and the albedo is simply our colour :)
                float3 amb = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

                // Lambertian component/diffuse we say Intensity * reflectivity * cos(theta)
                // here, we use our surface normal and light source directions in place
                // of cos, where cos(theta) == N . L (this has to be at least 0)
                // _LightColor0 grabs our sun's colour
                float3 diff = _LightColor0.rgb * _Color.rgb * max(0.0, dot(normDir, lightDir));

                // Specular is determined by orientation ,distance from viewer
                // and light source.
                float3 spec;
                // check if the light is behind the vertex
                if (dot(normDir, lightDir) < 0.0) {
                    spec = float3(0.0,0.0,0.0);
                } else { // otherwise add specular
                    // we say specular is intensity * reflectivity * cosa^n
                    // here, we replace cos once again with the dot product
                    // our n value is gloss (the specular reflection exponent)
                    spec = _LightColor0.rgb *
                            _Specular.rgb * pow(max(0.0, dot(
                            reflect(-lightDir, normDir),
                            viewDir)), _Gloss);
                }

                // TRANSLUCENCY //

                // diffuse translucency defined as i*k*max(0,(L.-N))
                // for i = light intensity
                // k = diffuse colour
                // L = light direction
                // N = normal
                float3 trlu = _LightColor0.rgb
                            * _Color.rgb
                            * max(0.0, dot(lightDir, -normDir));

                // ...

                // float3(ambient + diffuse + specular + trlu), alpha 
                o.col = float4(amb + diff + spec + trlu, 1.0);
                // translate using the unity matrix constant
                o.pos = UnityObjectToClipPos(input.pos);

                // done here..
                return o;
            }

            float4 frag(vOut input) : COLOR {
                // just a vertex shader :)
                return input.col;
            }

            ENDCG
        }
    }
}
