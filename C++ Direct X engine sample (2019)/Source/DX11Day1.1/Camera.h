#pragma once
#include <DirectXMath.h>
using namespace DirectX;

class Camera
{
public:
    Camera();
    ~Camera();

    void Update();

    void SetPos(float x, float y, float z) { mPosition[0] = x; mPosition[1] = y; mPosition[2] = z; }
    void SetRot(float x, float y, float z) { mRotation[0] = x; mRotation[1] = y; mRotation[2] = z; }

    XMFLOAT3 GetPos() { return XMFLOAT3(mPosition[0], mPosition[1], mPosition[2]); }
    XMFLOAT3 GetRot() { return XMFLOAT3(mRotation[0], mRotation[1], mRotation[2]); }
    //void GetViewMatrix(XMMATRIX& viewMatrix) { viewMatrix = mViewMatrix; }
    XMMATRIX GetViewMatrix() { return mViewMatrix; }

private:
    XMMATRIX mViewMatrix;

    float mPosition[3]{};
    float mRotation[3]{};
};