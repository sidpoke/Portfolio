#include "Shader.h"

Shader::Shader()
{
	mVertexShader = 0;
	mPixelShader = 0;
	iL = 0;
	mSampleState = 0;
}

void Shader::Init(std::shared_ptr<Graphics> graphics, LPCSTR shaderfile)
{
	std::ifstream shaderDatei;
	shaderDatei.open(shaderfile, std::ios::in);
	if (shaderDatei.fail())
	{
		ErrorHandler::Error("Eingabe-Datei des Shader kann nicht geoeffnet werden :(");
		failed = true;
	}
	else
	{
		shaderDatei.seekg(0, shaderDatei.end);
		int length = shaderDatei.tellg();
		shaderDatei.seekg(0, shaderDatei.beg);
		char* buffer = new char[int(length + 1)];
		ZeroMemory(buffer, int(length + 1));

		shaderDatei.read(buffer, length);
		shaderDatei.close();

		std::cout.write(buffer, length);


		D3D11_SAMPLER_DESC samplerDesc;

		ID3DBlob* errorBlob;
		ID3DBlob* vertexBlob;
		if (ErrorHandler::Failed(D3DCompile(buffer, length, shaderfile, nullptr, nullptr, "VS_Main", "vs_4_0", 0, 0, &vertexBlob, &errorBlob)))
		{
			char* error = reinterpret_cast<char*>(errorBlob->GetBufferPointer());
			OutputDebugStringA(error);
			failed = true;
		}
		if (errorBlob != nullptr) { errorBlob->Release(); }

		ID3DBlob* pixelBlob;
		if (ErrorHandler::Failed(D3DCompile(buffer, length, shaderfile, nullptr, nullptr, "PS_Main", "ps_4_0", 0, 0, &pixelBlob, &errorBlob)))
		{
			char* error = reinterpret_cast<char*>(errorBlob->GetBufferPointer());
			OutputDebugStringA(error);
			failed = true;
		}
		
		if (errorBlob != nullptr) { errorBlob->Release(); }

		if (ErrorHandler::Failed(graphics->dev->CreateVertexShader(vertexBlob->GetBufferPointer(), vertexBlob->GetBufferSize(), 0, &mVertexShader)))
		{
			failed = true;
		}

		if (ErrorHandler::Failed(graphics->dev->CreatePixelShader(pixelBlob->GetBufferPointer(), pixelBlob->GetBufferSize(), 0, &mPixelShader)))
		{
			failed = true;
		}

		D3D11_INPUT_ELEMENT_DESC ied[2]{};

		ied[0].SemanticName = "POSITION";
		ied[0].Format = DXGI_FORMAT_R32G32B32_FLOAT;
		ied[0].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;

		ied[1].SemanticName = "TEXCOORD";
		ied[1].Format = DXGI_FORMAT_R32G32_FLOAT;
		ied[1].AlignedByteOffset = D3D11_APPEND_ALIGNED_ELEMENT;
		ied[1].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;

		if (ErrorHandler::Failed(graphics->dev->CreateInputLayout(ied, ARRAYSIZE(ied), vertexBlob->GetBufferPointer(), vertexBlob->GetBufferSize(), &iL)))
		{
			failed = true;
		}

		if (vertexBlob != nullptr) vertexBlob->Release();
		if (pixelBlob != nullptr) pixelBlob->Release();
		delete[] buffer;

		// Create a texture sampler state description.
		samplerDesc.Filter = D3D11_FILTER_MIN_MAG_MIP_LINEAR;
		samplerDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
		samplerDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
		samplerDesc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
		samplerDesc.MipLODBias = 0.0f;
		samplerDesc.MaxAnisotropy = 1;
		samplerDesc.ComparisonFunc = D3D11_COMPARISON_ALWAYS;
		samplerDesc.BorderColor[0] = 0;
		samplerDesc.BorderColor[1] = 0;
		samplerDesc.BorderColor[2] = 0;
		samplerDesc.BorderColor[3] = 0;
		samplerDesc.MinLOD = 0;
		samplerDesc.MaxLOD = D3D11_FLOAT32_MAX;

		// Create the texture sampler state.
		if (ErrorHandler::Failed(graphics->dev->CreateSamplerState(&samplerDesc, &mSampleState)))
		{
			//return false;
		}
	}
}

Shader::~Shader()
{
	if (mVertexShader != nullptr) mVertexShader->Release();
	if (mPixelShader != nullptr) mPixelShader->Release();
	if (iL != nullptr) iL->Release();
	if (mSampleState != nullptr) mSampleState->Release();
}