#pragma once
#include "Window.h"
Window::Window(uint16_t pResolutionX, uint16_t pResolutionY, HINSTANCE pInstance)
{
	const auto mClassName = "MeinFenster";

	WNDCLASSEX mWindowClass = { 0 };
	mWindowClass.cbSize = sizeof(WNDCLASSEX); // grösse des structs
	mWindowClass.style = CS_OWNDC; // Art des Fensters
	mWindowClass.lpfnWndProc = WinInputHandler; // wer kümmert sich um Events aus Windows?
	mWindowClass.hInstance = pInstance; // zu wem gehört das fenster
	mWindowClass.lpszClassName = mClassName;
	mWindowClass.lpszMenuName = nullptr;


	if (!RegisterClassEx(&mWindowClass)) // Registriert die Klasse damit sie erstellt werden kann.
		throw "Klassenregistrierung kaputt :(";

	WindowHandle = CreateWindowExA(
		0, // zusätzliche Styles des fensters. z.B. Scrollbars, Ramenoptionen, ...
		mClassName, //Name der Klasse die erzeugt werden soll
		"Titelleistentext xD", // Text in der Titelleiste
		WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX, // standard styles wie MinimierButton, Rahmen zum vergrössern, Titelleiste, ...
		100, 100, pResolutionX, pResolutionY, // position und grösse des Fensters  X,Y,W,H
		nullptr, // pointer zum parent, obsolete
		nullptr, // pointer zum Menü-Strip
		pInstance, // pointer zur eigenen Inztanz
		nullptr); // zusätzlicher Speicher der irgendwie verwendet werden kann.

	if (WindowHandle == nullptr)
		throw "Window creation failed :(";

	ShowWindow(WindowHandle, SW_SHOW);
	UpdateWindow(WindowHandle);
}

Window::~Window()
{
	DestroyWindow(WindowHandle);
}
