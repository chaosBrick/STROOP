#version 330 core

layout (location = 0) in vec4 vs_position_showunits;
layout (location = 1) in vec4 vs_color;
layout (location = 2) in vec4 vs_outline_color;
layout (location = 3) in vec3 vs_outline_thickness;

out vec4 gs_worldPosition_showunits;
out vec4 gs_color;
out vec4 gs_outline_color;
out vec3 gs_outline_thickness;

void main()
{
    gl_Position = vec4(vs_position_showunits.xyz, 1);
	gs_worldPosition_showunits = vs_position_showunits;
	gs_color = vs_color;
	gs_outline_color = vs_outline_color;
	gs_outline_thickness = vs_outline_thickness;
} 