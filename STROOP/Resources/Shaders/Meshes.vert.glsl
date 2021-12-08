#version 330 core

const vec2 positions[4] = vec2[](
	vec2(-0.5, -0.5),
	vec2( 0.5, -0.5),
	vec2(-0.5,  0.5),
	vec2( 0.5,  0.5)
);

const vec2 texCoords[4] = vec2[](
	vec2(0, 1),
	vec2(1, 1),
	vec2(0, 0),
	vec2(1, 0)
);

layout (location = 0) in mat4 vs_instanceTransform;
layout (location = 4) in vec4 vs_instanceColor;
layout (location = 5) in vec4 vs_positionBrightness;

uniform mat4 viewProjection;

out vec4 fs_color;

void main()
{
    gl_Position = (viewProjection * vs_instanceTransform) * vec4(vs_positionBrightness.xyz, 1);
	fs_color = vec4(vs_instanceColor.rgb * vs_positionBrightness.a, vs_instanceColor.a);
} 