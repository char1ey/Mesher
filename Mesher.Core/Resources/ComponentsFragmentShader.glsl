#version 330

#define EPS 1e-5

in vec2 texCoord0;
in vec3 normal0;
in vec3 position0;

uniform bool hasTexCoord;
uniform bool hasNormal;

uniform vec4 color;

out vec4 finalColor; 
 
void main()
{   
	float power = 1;
	if(hasNormal && length(normal0.xy) > EPS)
		power = acos(dot(normal0, vec3(normalize(normal0.xy), 0)));

	finalColor = color;
}