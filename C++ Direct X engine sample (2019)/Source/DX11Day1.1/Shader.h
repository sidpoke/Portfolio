#pragma once

#include <string>
#include <fstream>
#include <iostream>
#include <D3Dcompiler.h>

#include "ErrorHandler.h"
#include "Graphics.h"

class Shader
{
public:
	Shader();
	~Shader();

	void Init(std::shared_ptr<Graphics> graphics, LPCSTR shaderfile);
	bool hasFailed() { return failed; }

	ID3D11VertexShader* mVertexShader;
	ID3D11PixelShader* mPixelShader;
	ID3D11InputLayout* iL;
	ID3D11SamplerState* mSampleState;

private:
	bool failed = false;
};

