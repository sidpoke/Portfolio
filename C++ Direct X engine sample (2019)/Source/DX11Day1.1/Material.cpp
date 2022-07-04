#include "Material.h"

Material::Material(LPCSTR shaderFile, char* textureFile) : mTextureFile(textureFile), mShaderFile(shaderFile)
{
	mTexture = std::make_shared<Texture>();
	mShader = std::make_unique<Shader>();
}

void Material::Init(std::shared_ptr<Graphics> graphics)
{
	mTexture->Init(graphics, mTextureFile);
	mShader->Init(graphics, mShaderFile);
}

Material::~Material()
{
	//delete mIed;
}