Shader "Water/basic" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Opacity ("Opacity", Float) = 0.5

		// specular props
        _Specular ("Specular Colour", Color) = (1,1,1,1) 
        _Gloss ("Gloss (smaller == bigger specular)", Float) = 10
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Transparent" }
		
			CGPROGRAM
			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
			#pragma vertex vert  
        	#pragma fragment frag 
        	#include "UnityCG.cginc"
			//Cull Off

			uniform float4 _Color;
			uniform float4 _LightColor0; 
            uniform float4 _Specular;
            uniform float4 _Gloss;

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

                // grab light src
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // PHONG //
                //ambient
                float3 amb = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
                // diffuse
                float3 diff = _LightColor0.rgb * _Color.rgb * max(0.0, dot(normDir, lightDir));
				// specular
                float3 spec;
                if (dot(normDir, lightDir) < 0.0) { // check if the light is behind the vertex
                    spec = float3(0.0,0.0,0.0);
                } else { // otherwise add specular
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

                o.col = float4(amb + diff + spec + trlu, 1.0);
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
	FallBack "Specular"
}
