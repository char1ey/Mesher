#version 330

in vec3 position;
 
uniform mat4 proj;
uniform mat4 modelView;
 
void main()
{
    gl_Position = proj * modelView * vec4(position, 1.0);
}