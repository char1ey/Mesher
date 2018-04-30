#version 330

in vec3 position;
in vec2 texCoord;
in vec3 normal; 
in vec3 tangent;
in vec3 biTangent;
 
uniform bool hasPosition;
uniform bool hasTexCoord;
uniform bool hasNormal;
uniform bool hasTangentBasis;

uniform vec3 lightPosition;
uniform mat4 proj;
uniform mat4 modelView; 

out vec2 texCoord0;
out vec3 normal0;
out vec3 position0;

out mat3 normalMatrix;
 
void main()
{
    normalMatrix = transpose(inverse(mat3(modelView)));

    gl_Position   = proj * modelView * vec4(position, 1.0);

    texCoord0 = texCoord;
    normal0 = normal;
	position0 = position;
}