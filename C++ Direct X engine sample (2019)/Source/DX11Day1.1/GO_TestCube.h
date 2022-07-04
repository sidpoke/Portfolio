#pragma once
#include "IGameObject.h"
#include "C_MeshRenderer.h"

class GO_TestCube : public IGameObject
{
public:
	GO_TestCube();
	~GO_TestCube();

	void Start();
	void Update();
	void End();
};

