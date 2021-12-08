#version 330 core

uniform sampler2D referenceDepthMask;
uniform sampler2D referenceSolidDepth;
uniform int readDepthMask;
uniform float solidDepthBias;

void main()
{
	if ((readDepthMask > 0 && gl_FragCoord.z <= texelFetch(referenceDepthMask, ivec2(gl_FragCoord.xy), 0).x)
		|| gl_FragCoord.z > texelFetch(referenceSolidDepth, ivec2(gl_FragCoord.xy), 0).x + solidDepthBias)
		discard;
} 