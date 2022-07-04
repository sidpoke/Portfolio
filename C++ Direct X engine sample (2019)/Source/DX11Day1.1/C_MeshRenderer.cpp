#include "C_MeshRenderer.h"
C_MeshRenderer::C_MeshRenderer(Material* mat, Mesh* mesh) : mMaterial(mat), mMesh(mesh)
{
	mMatrixBuffer = 0;
}

void C_MeshRenderer::Init(std::shared_ptr<Graphics> graphics)
{
	mGraphics = graphics;
	mMaterial->Init(graphics);

	//VERTEX BUFFER
	//Setup Vertex buffer desc.
	vertexBufDes.ByteWidth = sizeof(float) * mMesh->getVertexCount();
	vertexBufDes.Usage = D3D11_USAGE_DEFAULT;
	vertexBufDes.BindFlags = D3D11_BIND_VERTEX_BUFFER;
	vertexBufDes.CPUAccessFlags = 0;

	//Fill Buffer data holder
	vertexData.pSysMem = mMesh->getVertices();

	//Create Buffer
	if (ErrorHandler::Failed(mGraphics->dev->CreateBuffer(&vertexBufDes, &vertexData, &vertexBuffer)))
	{
		//return -1;
	}

	//INDEX BUFFER
	//Fill Index buffer desc.	
	indexBufDes.ByteWidth = sizeof(unsigned long) * mMesh->getIndexCount();
	indexBufDes.Usage = D3D11_USAGE_DEFAULT;
	indexBufDes.BindFlags = D3D11_BIND_INDEX_BUFFER;
	indexBufDes.CPUAccessFlags = 0;

	// Give the subresource structure a pointer to the index data.
	indexData.pSysMem = mMesh->getIndices();

	// Create the index buffer.
	if (ErrorHandler::Failed(mGraphics->dev->CreateBuffer(&indexBufDes, &indexData, &indexBuffer)))
	{
		//return -1;
	}

	mGraphics->devCon->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

	mGraphics->devCon->IASetInputLayout(mMaterial->mShader->iL);
	mGraphics->devCon->VSSetShader(mMaterial->mShader->mVertexShader, nullptr, 0);
	mGraphics->devCon->PSSetShader(mMaterial->mShader->mPixelShader, nullptr, 0);

	UINT stride = sizeof(float) * 5;
	UINT offsets = 0;

	mGraphics->devCon->IASetVertexBuffers(0, 1, &vertexBuffer, &stride, &offsets);
	mGraphics->devCon->IASetIndexBuffer(indexBuffer, DXGI_FORMAT_R32_UINT, 0);

	pTex = mMaterial->mTexture->GetTexture();
	mGraphics->devCon->PSSetShaderResources(0, 1, &pTex);
	mGraphics->devCon->PSSetSamplers(0, 1, &mMaterial->mShader->mSampleState);

	//MATRIX BUFFER
    // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
	matrixBufferDesc.ByteWidth = sizeof(MatrixBufferType);	
	matrixBufferDesc.Usage = D3D11_USAGE_DYNAMIC;
	matrixBufferDesc.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
	matrixBufferDesc.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;

	if (ErrorHandler::Failed(mGraphics->dev->CreateBuffer(&matrixBufferDesc, NULL, &mMatrixBuffer)))
	{
		//return -1;
	}
}

void C_MeshRenderer::Draw()
{
	mGraphics->devCon->DrawIndexed(mMesh->getIndexCount(), 0, 0);
}

void C_MeshRenderer::UpdatePositionToShader(XMMATRIX world, XMMATRIX view, XMMATRIX projection)
{
	D3D11_MAPPED_SUBRESOURCE mappedResource;
	MatrixBufferType* dataPtr;
	unsigned int bufferNumber;

	world = XMMatrixTranspose(world);
	view = XMMatrixTranspose(view);
	projection = XMMatrixTranspose(projection);

	if (ErrorHandler::Failed(mGraphics->devCon->Map(mMatrixBuffer, 0, D3D11_MAP_WRITE_DISCARD, 0, &mappedResource)))
	{
		//return false
	}

	// Get a pointer to the data in the constant buffer.
	dataPtr = (MatrixBufferType*)mappedResource.pData;

	// Copy the matrices into the constant buffer.
	dataPtr->world = world;
	dataPtr->view = view;
	dataPtr->projection = projection;

	// Unlock the constant buffer.
	mGraphics->devCon->Unmap(mMatrixBuffer, 0);

	// Set the position of the constant buffer in the vertex shader.
	bufferNumber = 0;

	// Finanly set the constant buffer in the vertex shader with the updated values.
	mGraphics->devCon->VSSetConstantBuffers(bufferNumber, 1, &mMatrixBuffer);
}

C_MeshRenderer::~C_MeshRenderer()
{
	if (vertexBuffer != nullptr) vertexBuffer->Release();
	if (mMatrixBuffer != nullptr) mMatrixBuffer->Release();
}