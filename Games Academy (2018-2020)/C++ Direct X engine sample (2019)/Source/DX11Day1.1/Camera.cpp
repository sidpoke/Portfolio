#include "Camera.h"

Camera::Camera()
{
	mViewMatrix = XMMatrixIdentity();
	mPosition[0] = 0.0f;
	mPosition[1] = 0.0f;
	mPosition[2] = 0.0f;
	mRotation[0] = 0.0f;
	mRotation[1] = 0.0f;
	mRotation[2] = 0.0f;
}

Camera::~Camera(){}

void Camera::Update()
{
	XMFLOAT3 up, position, lookAt;
	XMVECTOR upVector, positionVector, lookAtVector;
	float yaw, pitch, roll;
	XMMATRIX rotationMatrix;

	// Setup the vector that points upwards.
	up.x = 0.0f;
	up.y = 1.0f;
	up.z = 0.0f;

	// Load it into a XMVECTOR structure.
	upVector = XMLoadFloat3(&up);

	// Setup the position of the camera in the world.
	position.x = mPosition[0];
	position.y = mPosition[1];
	position.z = mPosition[2];

	// Load it into a XMVECTOR structure.
	positionVector = XMLoadFloat3(&position);

	// Setup where the camera is looking by default.
	lookAt.x = 0.0f;
	lookAt.y = 0.0f;
	lookAt.z = 1.0f;

	// Load it into a XMVECTOR structure.
	lookAtVector = XMLoadFloat3(&lookAt);

	// Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
	pitch = mRotation[0] * 0.0174532925f;
	yaw = mRotation[1] * 0.0174532925f;
	roll = mRotation[2] * 0.0174532925f;

	// Create the rotation matrix from the yaw, pitch, and roll values.
	rotationMatrix = XMMatrixRotationRollPitchYaw(pitch, yaw, roll);

	// Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
	lookAtVector = XMVector3TransformCoord(lookAtVector, rotationMatrix);
	upVector = XMVector3TransformCoord(upVector, rotationMatrix);

	// Translate the rotated camera position to the location of the viewer.
	lookAtVector = XMVectorAdd(positionVector, lookAtVector);

	// Finally create the view matrix from the three updated vectors.
	mViewMatrix = XMMatrixLookAtLH(positionVector, lookAtVector, upVector);
}