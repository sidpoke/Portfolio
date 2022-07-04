#include "S_TestScene.h"

void S_TestScene::Load()
{
	AddGameObject(std::make_shared<GO_TestCube>());
	mainCamera->SetPos(0.0f, 0.0f, -5.0f);
	mainCamera->SetRot(0.0f, 5.0f, 0.0f);
}

void S_TestScene::Update()
{
	float distance = 40.0f;
	time += 0.02f;
	mainCamera->SetPos(sin(2 * time) * distance, sin(2 * time) * distance, -cos(2 * time) * distance);
	mainCamera->SetRot(sin(2 * time) * 40, time * -(360 / XM_PI), 0);

	if (time >= 10000.0f)
	{
		time = 0;
	}
}