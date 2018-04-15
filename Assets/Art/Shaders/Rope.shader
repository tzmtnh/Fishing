// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33172,y:32769,varname:node_3138,prsc:2|emission-448-OUT;n:type:ShaderForge.SFN_TexCoord,id:4272,x:31508,y:32611,varname:node_4272,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Frac,id:7286,x:32390,y:32839,varname:node_7286,prsc:2|IN-6137-OUT;n:type:ShaderForge.SFN_Add,id:6137,x:32202,y:32839,varname:node_6137,prsc:2|A-2158-OUT,B-9871-OUT;n:type:ShaderForge.SFN_Multiply,id:2158,x:31969,y:32727,varname:node_2158,prsc:2|A-4272-V,B-4679-OUT;n:type:ShaderForge.SFN_Vector1,id:4679,x:31766,y:32812,varname:node_4679,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9871,x:31982,y:32934,varname:node_9871,prsc:2|A-4272-U,B-6275-OUT;n:type:ShaderForge.SFN_Vector1,id:6275,x:31766,y:32982,varname:node_6275,prsc:2,v1:5;n:type:ShaderForge.SFN_Posterize,id:9707,x:32629,y:32839,varname:node_9707,prsc:2|IN-7286-OUT,STPS-4410-OUT;n:type:ShaderForge.SFN_Vector1,id:4410,x:32390,y:32988,varname:node_4410,prsc:2,v1:3;n:type:ShaderForge.SFN_Lerp,id:448,x:32890,y:32796,varname:node_448,prsc:2|A-8132-RGB,B-7308-RGB,T-9707-OUT;n:type:ShaderForge.SFN_Color,id:8132,x:32558,y:32468,ptovrint:False,ptlb:Color0,ptin:_Color0,varname:node_8132,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.875,c2:0.6336207,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:7308,x:32558,y:32661,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_7308,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:8132-7308;pass:END;sub:END;*/

Shader "Shader Forge/Rope" {
    Properties {
        _Color0 ("Color0", Color) = (0.875,0.6336207,0,1)
        _Color1 ("Color1", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color0;
            uniform float4 _Color1;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_4410 = 3.0;
                float3 emissive = lerp(_Color0.rgb,_Color1.rgb,floor(frac(((i.uv0.g*0.5)+(i.uv0.r*5.0))) * node_4410) / (node_4410 - 1));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
