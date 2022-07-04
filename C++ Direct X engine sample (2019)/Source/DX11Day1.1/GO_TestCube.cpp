#include "GO_TestCube.h"

GO_TestCube::GO_TestCube()
{
	//Insert every Component we need for this object
	Material* mat = new Material("Shader.hlsl", "earth.tga");	//also available: tidus.tga
	Mesh* mesh = new Mesh("teapot.obj");	//also available: cube.obj

	addComponent<C_MeshRenderer>(std::make_shared<C_MeshRenderer>(mat, mesh));
}

void GO_TestCube::Start()
{

}

void GO_TestCube::Update()
{
	get<C_MeshRenderer>()->Draw();
}

void GO_TestCube::End()
{
	
}

GO_TestCube::~GO_TestCube()
{
	//Delete every Component
	mComponents.clear();
}