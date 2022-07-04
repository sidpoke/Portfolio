#pragma once
#include <d3d11.h>
#include <stdio.h>
#include <fstream>
#include "ErrorHandler.h"
#include "Graphics.h"

struct TargaHeader
{
	unsigned char data1[12];
	unsigned short width;
	unsigned short height;
	unsigned char bpp;
	unsigned char data2;
};

class Texture
{
public:
	Texture();
	~Texture();
	void Init(std::shared_ptr<Graphics> graphics, char* file);

	ID3D11ShaderResourceView* GetTexture() 
	{
		return mTextureView;
	}

private:
	unsigned char errorTexture[4]{ 255, 0, 255, 255 };

	bool LoadTextureTGA(char* file, int& width, int& height);
	unsigned char* mTargaData;
	ID3D11Texture2D* mTexture;
	ID3D11ShaderResourceView* mTextureView;
};

