#version 330 core

in vec3 fs_texCoord;
layout(location=0) out vec4 color;

uniform sampler2D sampler;

void main()
{
	color = texture(sampler, vec2(fs_texCoord.x, 1 - fs_texCoord.y));
} 