#version 330 core

in vec4 fs_color;
in vec4 fs_outline_color;
in vec3 fs_texCoord;

layout(location=0) out vec4 color;

void main()
{
	float f;
	vec3 del = max(abs(dFdx(fs_texCoord)), abs(dFdy(fs_texCoord)));
	float delta = min(del.x, del.y);
	float outline = fs_outline_color.a;
	
	if (fs_texCoord.z == 1.0) {
		f = max(
				abs(fs_texCoord.x), 
				abs(fs_texCoord.y)
			);
	}
	else {
		f = length(fs_texCoord);
	}

	float d = clamp((1 - (f - delta * outline * 0.5)) / delta, 0.0, 1.0);
	float d2 = clamp((1 - (f + delta * outline * 0.5)) / delta, 0.0, 1.0);
	
	color = mix(vec4(fs_outline_color.rgb, 1), fs_color, d2);
	color = mix(vec4(0), color, d);
} 