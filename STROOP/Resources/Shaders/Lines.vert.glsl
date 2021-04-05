#version 330 core

uniform mat4 viewProjection;

layout (location = 0) in vec4 vs_position_width;
layout (location = 1) in vec4 vs_color;

out float gs_width;
out vec4 gs_color;

void main()
{
    gl_Position = viewProjection * vec4(vs_position_width.xyz, 1);
	gs_width = vs_position_width.w;
	gs_color = vs_color;
} 