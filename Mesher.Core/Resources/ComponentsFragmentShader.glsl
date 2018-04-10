#version 330

in vec2 texCoord0;
in vec3 normal0;
in vec3 position0;

uniform bool hasTexCoord;
uniform bool hasNormal;

uniform vec4 color;

out vec4 finalColor; 
 
void main()
{   
	finalColor = color;
}