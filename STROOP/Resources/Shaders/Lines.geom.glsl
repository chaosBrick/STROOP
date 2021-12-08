#version 330 core

uniform vec2 viewportSize;

in float gs_width[];
in vec4 gs_color[];

out vec4 fs_color;

layout (lines) in;
layout (triangle_strip, max_vertices = 4) out;

void main()
{
	vec3 p1 = gl_in[1].gl_Position.xyz / gl_in[1].gl_Position.w;
	vec3 p0 = gl_in[0].gl_Position.xyz / gl_in[0].gl_Position.w;
	vec2 dist = p1.xy - p0.xy;
	vec2 perpendicular = normalize(vec2(dist.y, -dist.x)) / viewportSize;
	
	float w0 =  gl_in[0].gl_Position.w;
	float w1 =  gl_in[1].gl_Position.w;
	
	gl_Position = vec4(vec3(p0.xy + perpendicular * gs_width[0], p0.z) * w0, w0);
	fs_color = gs_color[0];
	EmitVertex();
	
	gl_Position = vec4(vec3(p0.xy - perpendicular * gs_width[0], p0.z) * w0, w0);
	fs_color = gs_color[0];
	EmitVertex();
	
	gl_Position = vec4(vec3(p1.xy + perpendicular * gs_width[1], p1.z) * w1, w1);
	fs_color = gs_color[1];
	EmitVertex();
	
	gl_Position = vec4(vec3(p1.xy - perpendicular * gs_width[1], p1.z) * w1, w1);
	fs_color = gs_color[1];
	EmitVertex();
	EndPrimitive();
} 