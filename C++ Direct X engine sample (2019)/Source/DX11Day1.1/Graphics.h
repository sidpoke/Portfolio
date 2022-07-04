#pragma once
#include <d3d11.h>
#include <memory>

#include "ErrorHandler.h"
#include "Window.h"

class Graphics
{
public:
	Graphics(std::shared_ptr<Window> windowHandler);
	~Graphics();
	bool hasFailed() { return failed; }
	void CreateSwapChainAndDevice();
	void CreateRenderTargetView();

	DXGI_SWAP_CHAIN_DESC scd{};			//Swap Chain Description
	IDXGISwapChain* sc = 0;				//Swap Chain
	ID3D11Device* dev = 0;				//Device
	ID3D11DeviceContext* devCon = 0;	//Device Context

	ID3D11Texture2D* pBackBuffer;		//Back Buffer

	ID3D11RenderTargetView* rtv;		//Render Target View
	ID3D11DepthStencilView* dsv;		//Depth Stencil View

	D3D11_TEXTURE2D_DESC dsd{};			//Depth Stencil Description
	ID3D11Texture2D* dsBuffer;			//Depth Stencil Buffer

	D3D11_VIEWPORT vp{};				//View Port

private:
	void FillSwapChainDescription();
	std::shared_ptr<Window> mWindowHandler;
	bool failed = false;
};