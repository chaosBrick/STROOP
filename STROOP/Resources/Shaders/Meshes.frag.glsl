#version 330 core

in vec4 fs_color;

layout(location=0) out vec4 color;

void main()
{
	gl_FragColor = fs_color;
} 