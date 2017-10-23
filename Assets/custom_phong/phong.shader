// Jack Kaloger 2017
// Phong illumination shader for COMP30019
// based on the work provided in labs, as well as the wikibooks tutorial:
// https://en.wikibooks.org/wiki/Cg_Programming/Unity/Diffuse_Reflection
Shader "Custom/Phong" {
    Properties {
        // basics
        _Color ("Colour", Color) = (1,1,1,1)

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

            // colour
            uniform float4 _Color;

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
                float4 colour = _Color;

                // we need the normal from the vertex/surface, transformed from world coords
                float3 normDir = normalize( mul(input.norm, unity_WorldToObject) );
                // we also need the direction of our camera
                float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, input.pos).xyz);

                // since we're only using a directional light, grab its direction straight from the constant
                // (attenuation value of the directional light will be 1.0 so we can ignore it..)
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // Phong illumination model is made up of Ambient, Diffuse and Specular components

                // Ambient component radiated light intensity
                // here, Ia is grabbed from the unity lightmodel
                // and the albedo is simply our colour :)
                float3 amb = UNITY_LIGHTMODEL_AMBIENT.rgb * colour.rgb;

                // Lambertian component/diffuse we say Intensity * reflectivity * cos(theta)
                // here, we use our surface normal and light source directions in place
                // of cos, where cos(theta) == N . L (this has to be at least 0)
                // _LightColor0 grabs our sun's colour
                float3 diff = _LightColor0.rgb * colour.rgb * max(0.0, dot(normDir, lightDir));

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

                // float3(ambient + diffuse + specular), alpha 
                o.col = float4(amb + diff + spec, 1.0);
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