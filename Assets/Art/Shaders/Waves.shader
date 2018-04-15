// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32884,y:32383,varname:node_3138,prsc:2|emission-1098-OUT,alpha-6168-OUT,clip-6600-A;n:type:ShaderForge.SFN_Color,id:7241,x:32250,y:32467,ptovrint:False,ptlb:Foam,ptin:_Foam,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6600,x:32453,y:32600,ptovrint:False,ptlb:Waves,ptin:_Waves,varname:node_6600,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8491-OUT;n:type:ShaderForge.SFN_TexCoord,id:27,x:31638,y:32691,varname:node_27,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:2669,x:32058,y:32552,varname:node_2669,prsc:2|A-2212-OUT,B-3058-OUT;n:type:ShaderForge.SFN_Time,id:4544,x:31696,y:32470,varname:node_4544,prsc:2;n:type:ShaderForge.SFN_Append,id:8491,x:32250,y:32633,varname:node_8491,prsc:2|A-2669-OUT,B-27-V;n:type:ShaderForge.SFN_ValueProperty,id:8059,x:31696,y:32403,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_8059,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:2212,x:31893,y:32441,varname:node_2212,prsc:2|A-8059-OUT,B-4544-T;n:type:ShaderForge.SFN_Color,id:6608,x:32250,y:32291,ptovrint:False,ptlb:Water,ptin:_Water,varname:_Foam_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4779412,c2:0.8271807,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:1098,x:32542,y:32384,varname:node_1098,prsc:2|A-6608-RGB,B-7241-RGB,T-6600-G;n:type:ShaderForge.SFN_Multiply,id:3058,x:31965,y:32805,varname:node_3058,prsc:2|A-27-U,B-2420-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2420,x:31737,y:32899,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_2420,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:6168,x:32709,y:32547,varname:node_6168,prsc:2|A-6608-A,B-7241-A,T-6600-G;proporder:8059-7241-6608-6600-2420;pass:END;sub:END;*/

Shader "Shader Forge/Waves" {
    Properties {
        _Speed ("Speed", Float ) = 0.5
        _Foam ("Foam", Color) = (1,1,1,1)
        _Water ("Water", Color) = (0.4779412,0.8271807,1,1)
        _Waves ("Waves", 2D) = "white" {}
        _Frequency ("Frequency", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Foam;
            uniform sampler2D _Waves; uniform float4 _Waves_ST;
            uniform float _Speed;
            uniform float4 _Water;
            uniform float _Frequency;
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
                float4 node_4544 = _Time;
                float2 node_8491 = float2(((_Speed*node_4544.g)+(i.uv0.r*_Frequency)),i.uv0.g);
                float4 _Waves_var = tex2D(_Waves,TRANSFORM_TEX(node_8491, _Waves));
                clip(_Waves_var.a - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Water.rgb,_Foam.rgb,_Waves_var.g);
                float3 finalColor = emissive;
                return fixed4(finalColor,lerp(_Water.a,_Foam.a,_Waves_var.g));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _Waves; uniform float4 _Waves_ST;
            uniform float _Speed;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_4544 = _Time;
                float2 node_8491 = float2(((_Speed*node_4544.g)+(i.uv0.r*_Frequency)),i.uv0.g);
                float4 _Waves_var = tex2D(_Waves,TRANSFORM_TEX(node_8491, _Waves));
                clip(_Waves_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
