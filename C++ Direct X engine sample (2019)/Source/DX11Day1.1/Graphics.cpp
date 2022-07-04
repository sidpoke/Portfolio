#include "Graphics.h"

Graphics::Graphics(std::shared_ptr<Window> windowHandler) : mWindowHandler(windowHandler)
{
	FillSwapChainDescription();
	CreateSwapChainAndDevice();
	CreateRenderTargetView();
}

void Graphics::FillSwapChainDescription()
{
	scd.BufferCount = 1;
	scd.BufferDesc.Width = 1024;
	scd.BufferDesc.Height = 768;
	scd.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	scd.BufferDesc.RefreshRate.Numerator = 60;
	scd.BufferDesc.RefreshRate.Denominator = 1;
	scd.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	scd.OutputWindow = mWindowHandler->WindowHandle;
	scd.SampleDesc.Count = 1;
	scd.SampleDesc.Quality = 0;
	scd.Windowed = TRUE;
}

void Graphics::CreateSwapChainAndDevice()
{
	D3D_FEATURE_LEVEL FeatureLevelRequested = D3D_FEATURE_LEVEL::D3D_FEATURE_LEVEL_11_0;
	UINT numLevelsRequested = 1;
	D3D_FEATURE_LEVEL FeatureLevelsSupported;

	const D3D_FEATURE_LEVEL lvl[] = { D3D_FEATURE_LEVEL_11_1, D3D_FEATURE_LEVEL_11_0, D3D_FEATURE_LEVEL_10_1, D3D_FEATURE_LEVEL_10_0, D3D_FEATURE_LEVEL_9_3, D3D_FEATURE_LEVEL_9_2, D3D_FEATURE_LEVEL_9_1 };
	UINT createDeviceFlags = 0;

#ifdef _DEBUG 
	createDeviceFlags |= D3D11_CREATE_DEVICE_DEBUG;
#endif

	HRESULT hr = D3D11CreateDeviceAndSwapChain(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, createDeviceFlags, lvl, _countof(lvl),
		D3D11_SDK_VERSION, &scd, &sc, &dev, &FeatureLevelsSupported, &devCon);
	if (ErrorHandler::Failed(hr))
	{
		failed = true;
	}
}

void Graphics::CreateRenderTargetView()
{
	//Create Backbuffer
	if (ErrorHandler::Failed(sc->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer)))
	{
		failed = true;
	}

	//Create Render Target View
	if (ErrorHandler::Failed(dev->CreateRenderTargetView(pBackBuffer, NULL, &rtv)))
	{
		failed = true;
	}

	//Create depth stencil buffer
	pBackBuffer->GetDesc(&dsd);
	dsd.Format = DXGI_FORMAT_D24_UNORM_S8_UINT;
	dsd.Usage = D3D11_USAGE_DEFAULT;
	dsd.BindFlags = D3D11_BIND_DEPTH_STENCIL;

	if (ErrorHandler::Failed(dev->CreateTexture2D(&dsd, NULL, &dsBuffer)))
	{
		failed = true;
	}	
	
	if (ErrorHandler::Failed(dev->CreateDepthStencilView(dsBuffer, NULL, &dsv)))
	{
		failed = true;
	}

	devCon->OMSetRenderTargets(1, &rtv, dsv);

	vp.Width = 1024;
	vp.Height = 768;
	vp.MinDepth = 0.0f;
	vp.MaxDepth = 1.0f;
	vp.TopLeftX = 0;
	vp.TopLeftY = 0;
	devCon->RSSetViewports(1, &vp);
}

Graphics::~Graphics()
{
	if (devCon != nullptr) devCon->Release();
	if (dev != nullptr) dev->Release();
	if (sc != nullptr) sc->Release();
	if (dsv != nullptr) dsv->Release();
	if (rtv != nullptr) rtv->Release();
	if (pBackBuffer != nullptr) pBackBuffer->Release();
	if (dsBuffer != nullptr) dsBuffer->Release();
}