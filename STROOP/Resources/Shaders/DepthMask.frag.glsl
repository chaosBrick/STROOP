#version 330 core

uniform sampler2D referenceDepthMask;
uniform sampler2D referenceSolidDepth;
uniform int readDepthMask;
uniform float solidDepthBias;

uniform float zNear;
uniform float zFar;

float linearize_depth(float d)
{
    return zNear * zFar / (zFar + d * (zNear - zFar));
}

void main()
{
	if ((readDepthMask > 0 && gl_FragCoord.z <= texelFetch(referenceDepthMask, ivec2(gl_FragCoord.xy), 0).x)
		|| linearize_depth(gl_FragCoord.z) > linearize_depth(texelFetch(referenceSolidDepth, ivec2(gl_FragCoord.xy), 0).x) + solidDepthBias)
		discard;
} 