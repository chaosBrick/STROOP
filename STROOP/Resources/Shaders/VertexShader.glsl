#version 110

attribute vec3 position;
attribute vec4 color;
attribute vec2 texCoords;

uniform mat4 view;

varying vec4 Color;
varying vec2 TexCoords;

void main()
{
    gl_Position = view * vec4(position, 1.0);
    Color = color;
    TexCoords = texCoords;
} 
