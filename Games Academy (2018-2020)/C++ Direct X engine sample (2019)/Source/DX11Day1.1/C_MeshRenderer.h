#pragma once
#include <list>
#include <memory>
#include <vector>
#include <directxmath.h>

#include "IComponent.h"
#include "Graphics.h"
#include "Material.h"
#include "Mesh.h"

using namespace DirectX;

class C_MeshRenderer : public IComponent
{
public:
	C_MeshRenderer(Material* mat, Mesh* mesh);
	~C_MeshRenderer();

	void Init(std::shared_ptr<Graphics> graphics);
	void Draw();

	void Start() {}
	void Update() {}
	void End() {}

	void UpdatePositionToShader(XMMATRIX world, XMMATRIX view, XMMATRIX projection);

	ID3D11Buffer* vertexBuffer;				
	D3D11_BUFFER_DESC vertexBufDes{};
	D3D11_SUBRESOURCE_DATA vertexData{};

	ID3D11Buffer* indexBuffer;				
	D3D11_BUFFER_DESC indexBufDes{};
	D3D11_SUBRESOURCE_DATA indexData{};

	ID3D11Buffer* mMatrixBuffer;
	D3D11_BUFFER_DESC matrixBufferDesc{};

private:
	struct MatrixBufferType
	{
		XMMATRIX world;
		XMMATRIX view;
		XMMATRIX projection;
	};

	std::shared_ptr<Graphics> mGraphics;
	Material* mMaterial;
	Mesh* mMesh;
	ID3D11ShaderResourceView* pTex;
};

