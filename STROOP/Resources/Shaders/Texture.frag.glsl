#version 330 core

in vec3 fs_texCoord;
in float fs_alpha;
layout(location=0) out vec4 color;

uniform sampler2DArray sampler;

void main()
{
	color = texture(sampler, fs_texCoord) * vec4(1, 1, 1, fs_alpha);
} 