#pragma once
#include "Shader.h"
#include "Graphics.h"
#include "Texture.h"

class Material
{
public:
	Material(LPCSTR shaderFile, char* textureFile);
	~Material();

	void Init(std::shared_ptr<Graphics> graphics);

	std::unique_ptr<Shader> mShader;
	std::shared_ptr<Texture> mTexture;

private:
	char* mTextureFile;
	LPCSTR mShaderFile;
};

