#version 330

in vec3 position;
in vec2 texCoord;
in vec3 normal; 
 
uniform vec4 viewPort;
uniform float clipDistance;

uniform bool hasTexCoord;
uniform bool hasNormal;

out vec2 texCoord0;
out vec3 normal0;
out vec3 position0;
 
void main()
{
	vec3 p = vec3((position.xy - viewPort.xy) / viewPort.zw * 2 - 1, position.z / clipDistance);

    gl_Position = vec4(p.xyz, 1.0);

    texCoord0 = texCoord;
    normal0 = normal;
	position0 = p;
}