#version 330 core

uniform mat4 viewProjection;
uniform float unitDivisor;

const vec3 barycentric[3] = vec3[](
	vec3(1, 0, 0),
	vec3(0, 1, 0),
	vec3(0, 0, 1)
);

in vec4 gs_worldPosition_showunits[];
in vec4 gs_color[];
in vec4 gs_outline_color[];
in vec3 gs_outline_thickness[];

out vec4 fs_worldPosition_showunits;
out vec4 fs_color;
out vec4 fs_outline_color;
out vec3 fs_outline_thickness;
out vec3 fs_barycentric;
out vec4 fs_coord01;
out vec2 fs_coord2;

layout (triangles) in;
layout (triangle_strip, max_vertices = 3) out;

vec4 TranslateToInflate(float x1, float z1, float x2, float z2)
{
	float xDiff = (x2 - x1);
	float zDiff = (z2 - z1);
	float f = unitDivisor * -2.0f / sqrt(xDiff * xDiff + zDiff * zDiff) * gs_worldPosition_showunits[0].w;
	xDiff *= f;
	zDiff *= f;
	return vec4(x1 + zDiff, z1 - xDiff, x2 + zDiff, z2 - xDiff);
}

vec4 TranslateToInflate(vec2 a, vec2 b)
{
	return TranslateToInflate(a.x, a.y, b.x, b.y);
}

vec2 Intersection(vec2 p1, vec2 d1, vec2 p2, vec2 d2)
{
	// Line AB represented as a1x + b1y = c1
	float a = d1.y - p1.y;
	float b = p1.x - d1.x;
	float c = a * (p1.x) + b * (p1.y);
	// Line CD represented as a2x + b2y = c2
	float a1 = d2.y - p2.y;
	float b1 = p2.x - d2.x;
	float c1 = a1 * (p2.x) + b1 * (p2.y);
	float det = a * b1 - a1 * b;
	
	float x = (b1 * c - b * c1) / det;
	float y = (a * c1 - a1 * c) / det;
	return vec2(x, y);
}

void main()
{
	vec4 lines[3];
	vec4 srcVertex[3];
	if (gs_worldPosition_showunits[0].w != 0) {
		lines = vec4[](
			TranslateToInflate(gl_in[0].gl_Position.xz, gl_in[1].gl_Position.xz),
			TranslateToInflate(gl_in[1].gl_Position.xz, gl_in[2].gl_Position.xz),
			TranslateToInflate(gl_in[2].gl_Position.xz, gl_in[0].gl_Position.xz)
		);
		for (int i = 0; i < 3; i++) {
			int nextIndex = (i + 1) % 3;
			float vertexY = gl_in[nextIndex].gl_Position.y;
			vec2 intersection = Intersection(lines[i].xy, lines[i].zw, lines[nextIndex].xy, lines[nextIndex].zw);
			srcVertex[i] = vec4(intersection.x, vertexY, intersection.y, 1);
		}
	}
	else {
		for (int i = 0; i < 3; i++) {
			srcVertex[i] = gl_in[i].gl_Position;
		}
	}
	
	vec4 coord01 = vec4(gl_in[0].gl_Position.xz, gl_in[1].gl_Position.xz);
	vec2 coord2 = gl_in[2].gl_Position.xz;
	
	for (int i = 0; i < 3; i++) {
		gl_Position = viewProjection * srcVertex[i];
		fs_worldPosition_showunits = vec4(srcVertex[i].xyz, gs_worldPosition_showunits[0].w);
		fs_coord01 = coord01;
		fs_coord2 = coord2;
		fs_color = gs_color[i];
		fs_outline_color = gs_outline_color[i];
		fs_outline_thickness = gs_outline_thickness[i];
		fs_barycentric = barycentric[i];
		EmitVertex();
	}
    EndPrimitive();
} 