#version 330 core

uniform vec2 viewportSize;

in float gs_width[];
in vec4 gs_color[];

out vec4 fs_color;

layout (lines) in;
layout (triangle_strip, max_vertices = 4) out;

void main()
{
	vec2 p1 = gl_in[1].gl_Position.xy / gl_in[1].gl_Position.w;
	vec2 p0 = gl_in[0].gl_Position.xy / gl_in[0].gl_Position.w;
	vec2 dist = p1 - p0;
	vec2 perpendicular = normalize(vec2(dist.y, -dist.x)) / viewportSize;
	
	gl_Position = vec4(p0 + perpendicular * gs_width[0], 0, 1);
	fs_color = gs_color[0];
	EmitVertex();
	
	gl_Position = vec4(p0 - perpendicular * gs_width[0], 0, 1);
	fs_color = gs_color[0];
	EmitVertex();
	
	gl_Position = vec4(p1 + perpendicular * gs_width[1], 0, 1);
	fs_color = gs_color[1];
	EmitVertex();
	
	gl_Position = vec4(p1 - perpendicular * gs_width[1], 0, 1);
	fs_color = gs_color[1];
	EmitVertex();
	EndPrimitive();
} 