#pragma comment (lib, "d3d11.lib")
#pragma comment (lib, "d3dcompiler.lib")

#include <Windows.h>
#include <d3d11.h>
#include <memory>

#include "Window.h"
#include "Graphics.h"
#include "S_TestScene.h"

//template<typename T> void SafeRelease(T*& aPointer)
//{
//	if (aPointer != nullptr)
//	{
//		aPointer->Release();
//		aPointer = nullptr;
//	}
//}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR szCmdLine, int iCmdShow)
{
	std::shared_ptr<Window> mWindow(new Window(1024, 768, hInstance)); 	//init a window instance
	std::shared_ptr<Graphics> mGraphics(new Graphics(mWindow));			//init a graphics instance

	if (mGraphics->hasFailed())
		return -1;

	S_TestScene test;

	test.Load();
	test.InitRenderers(mGraphics);

	MSG msg;

	while (true)
	{
		if (PeekMessage(&msg, nullptr, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);

			if (msg.message == WM_QUIT)
				break;
		}

		float color[4] = { 0, 0.3f, 0.5f, 1 };
		mGraphics->devCon->ClearRenderTargetView(mGraphics->rtv, color);
		mGraphics->devCon->ClearDepthStencilView(mGraphics->dsv, D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);
		
		test.InternalUpdate();

		mGraphics->sc->Present(2, 0);
	}

	return msg.wParam;
}