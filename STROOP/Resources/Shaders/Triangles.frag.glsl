#version 330 core

in vec4 fs_worldPosition_showunits;
in vec4 fs_color;
in vec4 fs_outline_color;
in vec3 fs_outline_thickness;
in vec3 fs_barycentric;
in vec4 fs_coord01;
in vec2 fs_coord2;

layout(location=0) out vec4 color;

uniform vec2 pixelsPerUnit;
uniform uint unitShift;

uniform sampler2DArray sampler;

float edgeFactor(){
    vec3 d = fwidth(fs_barycentric);
    vec3 a3 = smoothstep(vec3(0.0), d * fs_outline_thickness, fs_barycentric);
	a3 = pow(a3, max(fs_outline_thickness, vec3(1.0)));
    return min(min(a3.x, a3.y), a3.z);
}

bool PointInTriangle(int x, int z, int x1, int z1, int x2, int z2, int x3, int z3)
{
        // Check that the point is within the triangle bounds.
        if ((z1 - z) * (x2 - x1) - (x1 - x) * (z2 - z1) < 0) {
            return false;
        }
        if ((z2 - z) * (x3 - x2) - (x2 - x) * (z3 - z2) < 0) {
            return false;
        }
        if ((z3 - z) * (x1 - x3) - (x3 - x) * (z1 - z3) < 0) {
            return false;
        }
		return true;
}

void main()
{
	float f;
	if (fs_worldPosition_showunits.w == 0)
		f = edgeFactor();
	else 
	{
		vec2 dasDaHalt = (fs_worldPosition_showunits.xz);
		ivec2 unit = ivec2(dasDaHalt) >> unitShift;
		ivec2 p1 = ivec2(fs_coord01.xy) >> unitShift;
		ivec2 p2 = ivec2(fs_coord01.zw) >> unitShift;
		ivec2 p3 = ivec2(fs_coord2.xy) >> unitShift;
		
		if (!PointInTriangle(unit.x, unit.y, p1.x, p1.y, p2.x, p2.y, p3.x, p3.y))
			discard;
		
		vec2 frac = fract(dasDaHalt);
		frac = 1 - max(frac, vec2(1) - frac);
		f = clamp(pixelsPerUnit.x * min(frac.x, frac.y), 0, 1);
		f *= clamp(pixelsPerUnit.x, 0, 1);
	}
	color = mix(fs_outline_color, fs_color, f);
} 