#pragma once
#pragma once
#include <Windows.h>
#include <iostream>

class Window
{
public:
	// TODO : Wir nutzen Constructors damit Rule of 5!
	Window(uint16_t pResolutionX, uint16_t pResolutionY, HINSTANCE pInstance);
	~Window();

	HWND WindowHandle = nullptr;

	static LRESULT CALLBACK WinInputHandler( // CALLBACK da Windows das dingen aufrufen muss
		HWND pWindowHandle, // handle zu dem Fenster das aktiv war beim Input
		UINT pMessageType, // typ der Nachricht
		WPARAM pWParam, // abhänging vom Nachrichtentyp andere Infos
		LPARAM pLParam) // abhänging vom Nachrichtentyp andere Infos
	{
		if (pMessageType == WM_CLOSE)
			PostQuitMessage(0); // wenn das Fenster geschlossen wird soll auch die Message WM_QUIT gesendet werden

		return DefWindowProc(pWindowHandle, pMessageType, pWParam, pLParam); // Infos durchschleifen an Default Handler von Windows
	}
};