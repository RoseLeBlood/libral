#version 150

out vec4 inColor;
in vec2 position; 
void main() 
{ 
	inColor = vec4( position*2, 0, 1.0 ); 
	gl_Position = vec4( position, 0.0, 1.0 ); 
}


