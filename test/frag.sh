#version 150 

uniform sampler2D tex;

smooth in vec3 theColor; 
out vec4 outputColor; 

void main() 
{ 
   outputColor = texture(tex, gl_PointCoord) + vec4(theColor, 1.0); 
}

