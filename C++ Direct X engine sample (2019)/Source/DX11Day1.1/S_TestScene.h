#pragma once
#include "IScene.h"
#include "GO_TestCube.h"

class S_TestScene : public IScene
{
public:
	void Load();		//Load gameobjects
	void Update();		//Update scene
private:
	float time;
};

