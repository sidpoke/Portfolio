//GLOBAL VARIABLES
//Projection Variables
cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

//position variables

//Texture sampling
Texture2D shaderTexture;
SamplerState SampleType;

// Vertex Shader
struct VS_INPUT
{
	float4 aPosition : POSITION;
	float2 aTex : TEXCOORD0;
};

struct PS_INPUT
{
	float4 aPosition : SV_POSITION;
	float2 aTex : TEXCOORD0;
};

PS_INPUT VS_Main(VS_INPUT input) 
{
	PS_INPUT output;
	
	//output.aPosition = input.aPosition + float4(0, 0, 0, 1);
	
	// Change the position vector to be 4 units for proper matrix calculations.
	input.aPosition.w = 1.0f;

    // Calculate the position of the vertex against the world, view, and projection matrices.
	output.aPosition = mul(input.aPosition, worldMatrix);
	output.aPosition = mul(output.aPosition, viewMatrix);
	output.aPosition = mul(output.aPosition, projectionMatrix);
	
	output.aTex = input.aTex;

	return output; 
}



// Pixel Shader
float4 PS_Main(PS_INPUT input):SV_TARGET
{
	float4 textureColor;
	textureColor = shaderTexture.Sample(SampleType, input.aTex);
	
	return textureColor;
}