// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:6,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33538,y:32929,varname:node_3138,prsc:2|emission-752-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32144,y:32714,ptovrint:False,ptlb:Low,ptin:_Low,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.109375,c2:0.6080579,c3:0.875,c4:1;n:type:ShaderForge.SFN_Color,id:7680,x:32144,y:32898,ptovrint:False,ptlb:Deep,ptin:_Deep,varname:node_7680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.02676254,c2:0.1265161,c3:0.2426471,c4:1;n:type:ShaderForge.SFN_TexCoord,id:7548,x:31772,y:33048,varname:node_7548,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:4302,x:32463,y:32912,varname:node_4302,prsc:2|A-7241-RGB,B-7680-RGB,T-7035-OUT;n:type:ShaderForge.SFN_Step,id:9207,x:32731,y:33018,varname:node_9207,prsc:2|A-7035-OUT,B-9247-OUT;n:type:ShaderForge.SFN_Sin,id:8430,x:32557,y:33282,varname:node_8430,prsc:2|IN-2538-OUT;n:type:ShaderForge.SFN_Time,id:1819,x:32001,y:33272,varname:node_1819,prsc:2;n:type:ShaderForge.SFN_Add,id:2538,x:32383,y:33282,varname:node_2538,prsc:2|A-2158-OUT,B-9840-OUT;n:type:ShaderForge.SFN_RemapRange,id:9247,x:32735,y:33282,varname:node_9247,prsc:2,frmn:-1,frmx:1,tomn:0.002,tomx:0.003|IN-8430-OUT;n:type:ShaderForge.SFN_Multiply,id:2158,x:32383,y:33146,varname:node_2158,prsc:2|A-7548-U,B-6940-OUT;n:type:ShaderForge.SFN_Multiply,id:9840,x:32209,y:33319,varname:node_9840,prsc:2|A-1819-T,B-1353-OUT;n:type:ShaderForge.SFN_RemapRange,id:8641,x:32735,y:33481,varname:node_8641,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:0.001|IN-8430-OUT;n:type:ShaderForge.SFN_Step,id:5853,x:33058,y:33187,varname:node_5853,prsc:2|A-8641-OUT,B-7035-OUT;n:type:ShaderForge.SFN_Multiply,id:752,x:33326,y:33036,varname:node_752,prsc:2|A-9503-OUT,B-5853-OUT;n:type:ShaderForge.SFN_Lerp,id:9503,x:33037,y:32909,varname:node_9503,prsc:2|A-4302-OUT,B-5912-RGB,T-9207-OUT;n:type:ShaderForge.SFN_Color,id:5912,x:32567,y:32626,ptovrint:False,ptlb:Foam,ptin:_Foam,varname:node_5912,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_OneMinus,id:7035,x:31988,y:33087,varname:node_7035,prsc:2|IN-7548-V;n:type:ShaderForge.SFN_ValueProperty,id:1353,x:32001,y:33428,ptovrint:False,ptlb:WaterSpeed,ptin:_WaterSpeed,varname:node_1353,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_ValueProperty,id:6940,x:32170,y:33220,ptovrint:False,ptlb:WaterFrequency,ptin:_WaterFrequency,varname:node_6940,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:50;proporder:7241-7680-5912-1353-6940;pass:END;sub:END;*/

Shader "Shader Forge/Water" {
    Properties {
        _Low ("Low", Color) = (0.109375,0.6080579,0.875,1)
        _Deep ("Deep", Color) = (0.02676254,0.1265161,0.2426471,1)
        _Foam ("Foam", Color) = (1,1,1,1)
        _WaterSpeed ("WaterSpeed", Float ) = 10
        _WaterFrequency ("WaterFrequency", Float ) = 50
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
            Blend One OneMinusSrcColor
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Low;
            uniform float4 _Deep;
            uniform float4 _Foam;
            uniform float _WaterSpeed;
            uniform float _WaterFrequency;
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
                float node_7035 = (1.0 - i.uv0.g);
                float3 node_4302 = lerp(_Low.rgb,_Deep.rgb,node_7035);
                float4 node_1819 = _Time;
                float node_8430 = sin(((i.uv0.r*_WaterFrequency)+(node_1819.g*_WaterSpeed)));
                float3 emissive = (lerp(node_4302,_Foam.rgb,step(node_7035,(node_8430*0.0005+0.0025)))*step((node_8430*0.0005+0.0005),node_7035));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
