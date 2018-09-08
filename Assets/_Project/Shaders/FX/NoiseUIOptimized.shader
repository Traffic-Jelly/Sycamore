// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sycamore/FX/Background Noise Optimized"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		[HideInInspector]
		_StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector]
		_Stencil ("Stencil ID", Float) = 0
		[HideInInspector]
		_StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector]
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector]
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector]
		_ColorMask ("Color Mask", Float) = 15

		[HideInInspector]
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_Scale("Scale", Float) = 1
		_Octaves("Octaves", Range( 0 , 3)) = 0
		[HideInInspector]_Offset("Offset", Float) = 0
		[HideInInspector]_Angle("Angle", Float) = 0
		_Bias("Bias", Float) = 0
		_ColorB("Color B", Color) = (0,0,0,1)
		_ColorA("Color A", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord2 : TEXCOORD2;
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float4 _ColorA;
			uniform float4 _ColorB;
			uniform float _Angle;
			uniform float _Offset;
			uniform float _Scale;
			uniform float _Bias;
			uniform float _Octaves;
			float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }
			float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }
			float snoise( float3 v )
			{
				const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
				float3 i = floor( v + dot( v, C.yyy ) );
				float3 x0 = v - i + dot( i, C.xxx );
				float3 g = step( x0.yzx, x0.xyz );
				float3 l = 1.0 - g;
				float3 i1 = min( g.xyz, l.zxy );
				float3 i2 = max( g.xyz, l.zxy );
				float3 x1 = x0 - i1 + C.xxx;
				float3 x2 = x0 - i2 + C.yyy;
				float3 x3 = x0 - 0.5;
				i = mod3D289( i);
				float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
				float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
				float4 x_ = floor( j / 7.0 );
				float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
				float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 h = 1.0 - abs( x ) - abs( y );
				float4 b0 = float4( x.xy, y.xy );
				float4 b1 = float4( x.zw, y.zw );
				float4 s0 = floor( b0 ) * 2.0 + 1.0;
				float4 s1 = floor( b1 ) * 2.0 + 1.0;
				float4 sh = -step( h, 0.0 );
				float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
				float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
				float3 g0 = float3( a0.xy, h.x );
				float3 g1 = float3( a0.zw, h.y );
				float3 g2 = float3( a1.xy, h.z );
				float3 g3 = float3( a1.zw, h.w );
				float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
				g0 *= norm.x;
				g1 *= norm.y;
				g2 *= norm.z;
				g3 *= norm.w;
				float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
				m = m* m;
				m = m* m;
				float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
				return 42.0 * dot( m, px);
			}
			
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.worldPosition = IN.vertex;
				float4 ase_clipPos = UnityObjectToClipPos(IN.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				OUT.ase_texcoord2 = screenPos;
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float4 screenPos = IN.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos/screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float3 rotatedValue15 = RotateAroundAxis( float3( 0,0,0 ), ( float4( ( float3(0,0,1) * _Offset ) , 0.0 ) + ( _Scale * 15.0 * ase_screenPosNorm ) ).xyz, float3( 0,0,1 ), _Angle );
				float simplePerlin3D5 = snoise( rotatedValue15 );
				float4 lerpResult17 = lerp( _ColorA , _ColorB , ( (0.0 + (simplePerlin3D5 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) + _Bias + _Octaves ));
				
				half4 color = lerpResult17;
				
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
239;92;1292;718;1432.35;-85.0093;1.6;True;False
Node;AmplifyShaderEditor.Vector3Node;12;-640,592;Float;False;Constant;_OffsetDirection;Offset Direction;5;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-880,976;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-640,736;Float;False;Property;_Offset;Offset;2;1;[HideInInspector];Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-640,896;Float;False;Constant;_SmallScalar;Small Scalar;4;0;Create;True;0;0;False;0;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-640,816;Float;False;Property;_Scale;Scale;0;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-368,816;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-368,592;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-640,512;Float;False;Property;_Angle;Angle;3;1;[HideInInspector];Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-160,688;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-640,432;Float;False;Property;_Bias;Bias;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;20;-128,512;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;15;16,496;Float;False;False;4;0;FLOAT3;0,0,1;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;21;16,720;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;5;336,496;Float;False;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;16;576,496;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;22;736,704;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-640,352;Float;False;Property;_Octaves;Octaves;1;0;Create;True;0;0;False;0;0;0;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;800,496;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;29;-640,176;Float;False;Property;_ColorB;Color B;5;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;28;-640,0;Float;False;Property;_ColorA;Color A;6;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;17;848,256;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;27;-480,992;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;-640,992;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;26;-640,1088;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1104,0;Float;False;True;2;Float;ASEMaterialInspector;0;3;Sycamore/FX/Background Noise Optimized;5056123faa0c79b47ab6ad7e8bf059a4;0;0;Default;2;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;False;True;Off;False;False;True;2;False;False;True;5;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;9;0;3;0
WireConnection;9;1;10;0
WireConnection;9;2;8;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;14;0;13;0
WireConnection;14;1;9;0
WireConnection;20;0;19;0
WireConnection;15;1;6;0
WireConnection;15;3;14;0
WireConnection;21;0;20;0
WireConnection;5;0;15;0
WireConnection;16;0;5;0
WireConnection;22;0;21;0
WireConnection;18;0;16;0
WireConnection;18;1;22;0
WireConnection;18;2;4;0
WireConnection;17;0;28;0
WireConnection;17;1;29;0
WireConnection;17;2;18;0
WireConnection;27;0;25;0
WireConnection;27;1;26;0
WireConnection;25;0;8;1
WireConnection;25;1;8;3
WireConnection;26;0;8;2
WireConnection;26;1;8;4
WireConnection;0;0;17;0
ASEEND*/
//CHKSM=900D8988B2E2365D2F77796718210327C519B06D