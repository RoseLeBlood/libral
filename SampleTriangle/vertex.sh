#version 150 

in vec3 inPosition; 
in vec3 inColor; 

smooth out vec3 theColor; 

void main() 
{ 
   gl_Position = vec4(inPosition, 1.0); 
   theColor = inColor; 
}

