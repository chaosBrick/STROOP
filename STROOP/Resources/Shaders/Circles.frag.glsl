#version 330 core

in vec4 fs_color;
in vec4 fs_outline_color;
in vec2 fs_texCoord;

layout(location=0) out vec4 color;

void main()
{
	float delta = fwidth(fs_texCoord).x;
	float outline = fs_outline_color.a;
	float d = clamp((1 - length(fs_texCoord)) / delta, 0.0, 1.0);
	
	float d2 = clamp((1 - (length(fs_texCoord) + delta * outline * 0.5)) / delta, 0.0, 1.0);
	
	color = mix(vec4(fs_outline_color.rgb, 1), fs_color, d2);
	color = mix(vec4(0), color, d);
} 