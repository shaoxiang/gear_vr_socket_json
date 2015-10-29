// Simplified VertexLit shader. Differences from regular VertexLit one:
// - no per-material color
// - no specular
// - no emission

Shader "PFC/Mobile/Diffuse" {
Properties {
	//_MainTex ("Base (RGB)", 2D) = "white" {}
	_Color ("Main Color", Color) = (1,1,1,1)
}

// ---- Dual texture cards
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	// Non-lightmapped
	Pass {
		Tags { "LightMode" = "Vertex" }
		
		Material {
			Diffuse [_Color]
			Ambient [_Color]
		} 
		Lighting On
		//SetTexture [_MainTex] {
		//	Combine texture * primary DOUBLE, texture * primary
		//} 
	}
	
	// Lightmapped, encoded as dLDR
	Pass {
		Tags { "LightMode" = "VertexLM" }
		
		BindChannels {
			Bind "Vertex", vertex
			Bind "normal", normal
			Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
			Bind "texcoord", texcoord1 // main uses 1st uv
		}
		
		SetTexture [unity_Lightmap] {
			matrix [unity_LightmapMatrix]
			combine texture
		}
		//SetTexture [_MainTex] {
		//	combine texture * previous DOUBLE, texture * primary
		//}
	}
	
	// Lightmapped, encoded as RGBM
	Pass {
		Tags { "LightMode" = "VertexLMRGBM" }
		
		BindChannels {
			Bind "Vertex", vertex
			Bind "normal", normal
			Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
			Bind "texcoord", texcoord1 // main uses 1st uv
		}
		
		SetTexture [unity_Lightmap] {
			matrix [unity_LightmapMatrix]
			combine texture * texture alpha DOUBLE
		}
		//SetTexture [_MainTex] {
		//	combine texture * previous QUAD, texture * primary
		//}
	}	
}

// ---- Single texture cards (requires 2 passes for lightmapped)
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100

	// Non-lightmapped
	Pass {
		Tags { "LightMode" = "Vertex" }
		
		Material {
			Diffuse [_Color]
			Ambient [_Color]
		} 
		Lighting On
		//SetTexture [_MainTex] {
		//	Combine texture * primary DOUBLE, texture * primary
		//} 
	}	
	// Lightmapped, encoded as dLDR
	Pass {
		// 1st pass - sample Ligltmap
		Tags { "LightMode" = "VertexLM" }

		BindChannels {
			Bind "Vertex", vertex
			Bind "texcoord1", texcoord0 // lightmap uses 2nd uv
		}		
		SetTexture [unity_Lightmap] {
			matrix [unity_LightmapMatrix]
			combine texture
		}
	}
	Pass {
		// 2nd pass - multiply with _MainTex
		Tags { "LightMode" = "VertexLM" }
		ZWrite Off
		Fog {Mode Off}
		Blend DstColor Zero
		//SetTexture [_MainTex] {
		//	combine texture
		//}
	}
}
}
