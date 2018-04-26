#version 330

#define MAX_LIGHTS_COUNT 32

#define LIGHT_TYPE_UNDEFINED 0
#define LIGHT_TYPE_DIRECTIONAL 1
#define LIGHT_TYPE_POINT 2
#define LIGHT_TYPE_SPOT 4

uniform struct Material
{
	bool hasColorAmbient;
	vec4 colorAmbient;

	bool hasColorDiffuse;
	vec4 colorDiffuse;

	bool hasColorSpecular;
	vec4 colorSpecular;

	bool hasShininess;
	float shininess;

	bool hasTextureAmbient;
	sampler2D textureAmbient;

	bool hasTextureDiffuse;
	sampler2D textureDiffuse;

	bool hasTextureSpecular;
	sampler2D textureSpecular;

	bool hasTextureNormal;
	sampler2D textureNormal;  
} material;

uniform struct Light 
{
	int lightType;
	vec3 ambientColor;
	vec3 diffuseColor;
	vec3 specularColor; 
	vec3 position;
	vec3 direction; 
	float innerAngle;
	float outerAngle;
	float attenuationConstant;
	float attenuationLinear;
	float attenuationQuadratic;

} lights[MAX_LIGHTS_COUNT];

uniform int lightsCount;

uniform mat4 proj;
uniform mat4 modelView; 
uniform vec3 cameraPosition;

in vec2 texCoord0;
in vec3 normal0;
in vec3 position0;

in mat3 normalMatrix;

out vec4 finalColor; 
 
float getAtenuation(float d, Light light)
{
	float attenuation = light.attenuationConstant + light.attenuationLinear * d + light.attenuationQuadratic * d * d;
	if(attenuation == 0.0)
		return 0.0;
    return 1.0 / attenuation;
}

vec4 getAmbientMaterial()
{
	vec4 ret = vec4(1);

	if(material.hasColorAmbient)
		ret *= material.colorAmbient;
	if(material.hasTextureAmbient)
		ret *= texture2D(material.textureAmbient, texCoord0);

	return ret;
}

vec4 getDiffuseMaterial()
{
	vec4 ret = vec4(1);

	if(material.hasColorDiffuse)
		ret *= material.colorDiffuse;
	if(material.hasTextureDiffuse)
		ret *= texture2D(material.textureDiffuse, texCoord0);

	return ret;
}

vec4 getSpecularMaterial()
{
	vec4 ret = vec4(1);

	if(material.hasColorSpecular)
		ret *= material.colorSpecular;
	if(material.hasTextureSpecular)
		ret *= texture2D(material.textureSpecular, texCoord0);

	return ret;
}

vec4 ApplyLight(Light light, vec3 normal, vec3 surfacePos, vec3 surfaceToCamera)
{
    vec3 surfaceToLight;
    float attenuation = 1.0;
    if(light.lightType == LIGHT_TYPE_DIRECTIONAL)
	{
        surfaceToLight = normalize(light.position);
        attenuation = 1.0;
    } 
	else
	{
        surfaceToLight = normalize(light.position - surfacePos);
        float distanceToLight = length(light.position - surfacePos);
        attenuation = getAtenuation(distanceToLight, light);

		if(light.lightType == LIGHT_TYPE_SPOT)
		{
			float lightToSurfaceAngle = degrees(acos(dot(-surfaceToLight, normalize(light.direction))));

			if(lightToSurfaceAngle <= light.innerAngle)
				attenuation = 1.0;
			if(lightToSurfaceAngle > light.outerAngle)
				attenuation = 0.0;
		}
    }

    vec4 ambient = vec4(light.ambientColor, 1) * getAmbientMaterial();

    float diffuseCoefficient = max(0.0, dot(normal, surfaceToLight));
    vec4 diffuse = diffuseCoefficient * vec4(light.diffuseColor, 1) * getDiffuseMaterial();

    float specularCoefficient = 0.0;
    if(diffuseCoefficient > 0.0)
        specularCoefficient = pow(max(0.0, dot(surfaceToCamera, reflect(-surfaceToLight, normal))), 1);
    vec4 specular = specularCoefficient * vec4(light.specularColor, 1) * getSpecularMaterial();

    return ambient + attenuation * (specular + diffuse);
}

void main()
{   
	vec3 normal = normalMatrix * normal0;

    vec3 surfaceToCamera = normalize(cameraPosition - position0);

    vec4 linearColor = vec4(0);	

	if(lightsCount == 0)
		linearColor = getDiffuseMaterial() + getAmbientMaterial();		

    for(int i = 0; i < lightsCount; ++i)
        linearColor += ApplyLight(lights[i], normal, position0, surfaceToCamera);
        
    vec4 gamma = vec4(vec3(1.0/1.2), 1);
    finalColor = pow(linearColor, gamma);

	vec4 sampledColor = texture2D(material.textureDiffuse, texCoord0);  

	finalColor = finalColor;
}