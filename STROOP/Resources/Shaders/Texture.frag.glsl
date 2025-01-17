﻿#version 330 core

in vec3 fs_texCoord;
in vec4 fs_color;
layout(location=0) out vec4 color;

uniform sampler2DArray sampler;

void main()
{
	color = texture(sampler, fs_texCoord) * fs_color;
} 