#pragma once
#include <memory>
#include <vector>
#include <DirectXMath.h>
#include "IGameObject.h"
#include "Graphics.h"
#include "C_MeshRenderer.h"
#include "Camera.h"

/*
	Grundaufbau aller Szenen mit Pflicht-Funktionen
*/

class IScene
{
public:

	IScene()
	{
		mainCamera = std::make_shared<Camera>();

		float fov = XM_PI / 4.0f;
		float screenAspect = 1024.0f / 768.0f;
		float screenNear = 0.01f;
		float screenDepth = 1000.0f;
		mProjectionMatrix = XMMatrixPerspectiveFovLH(fov, screenAspect, screenNear, screenDepth);
	}

	virtual void Load() = 0;		//Load gameobjects
	virtual void Update() = 0;		//Update scene

	void InternalUpdate()
	{
	//Internal Update Function, used to 
		for (auto& g : gameObjects)
		{
			g->Update();

			if (g->get<C_MeshRenderer>() != nullptr)
			{
				g->get<C_MeshRenderer>()->UpdatePositionToShader(mWorldMatrix, mainCamera->GetViewMatrix(), mProjectionMatrix);
			}
		}

		mainCamera->Update();
		Update();
	}

	void UnLoad() //Unload gameobjects
	{
		while (gameObjects.size() > 0)
		{
			gameObjects.pop_back();
		}
	}		

	bool isLoaded() { return loaded; }		//return scene loaded status

	void InitRenderers(std::shared_ptr<Graphics> graphics)
	{
		for (auto &g : gameObjects)
		{
			if (g->get<C_MeshRenderer>() != nullptr)
			{
				g->get<C_MeshRenderer>()->Init(graphics);
			}
		}
	}

	void AddGameObject(std::shared_ptr<IGameObject> go)
	{
		gameObjects.push_back(go);
	}

	void RemoveGameObject(int index)
	{
		gameObjects.erase(gameObjects.begin() + index);
	}

protected:
	bool loaded = false;
	std::shared_ptr<Camera> mainCamera;
	std::vector<std::shared_ptr<IGameObject>> gameObjects;	//vector that holds game objects
	XMMATRIX mWorldMatrix = XMMatrixIdentity();
	XMMATRIX mProjectionMatrix;
};