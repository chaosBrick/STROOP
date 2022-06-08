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
layout (location = 5) in float vs_instanceLayer;

uniform mat4 viewProjection;

out vec3 fs_texCoord;
out vec4 fs_color;

void main()
{
    gl_Position = (viewProjection * vs_instanceTransform) * vec4(positions[gl_VertexID], 0, 1);
	fs_texCoord = vec3(texCoords[gl_VertexID], vs_instanceLayer);
	fs_color = vs_instanceColor;
} 