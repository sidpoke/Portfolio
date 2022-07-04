#pragma once
class IComponent
{
public:
	virtual void Start() = 0;
	virtual void Update() = 0;
	virtual void End() = 0;
};