#pragma once
#include <fstream>
#include <iostream>
#include <vector>
#include <string>
#include <sstream>
#include <array>

#include "ErrorHandler.h"

enum class MeshPrimitive
{
	Plane,
	Cube
};

struct VertexType //Vertex info struct
{
	float x, y, z, u, v;

	VertexType(float x, float y, float z, float u, float v)
	{
		this->x = x;
		this->y = y;
		this->z = z;
		this->u = u;
		this->v = v;
	}
};

class Mesh
{
public:
	Mesh(std::string meshFile);
	Mesh(MeshPrimitive primitive);

	~Mesh();

	float* Mesh::getVertices()
	{
		return reinterpret_cast<float*>(vertices->data());
	}
	unsigned int getVertexCount()
	{
		return vertices->size() * 5;
	}
	uint32_t* getIndices()
	{
		return indices->data();
	}
	unsigned int getIndexCount()
	{
		return indices->size();
	}
private:
	std::vector<VertexType>* vertices;
	std::vector<uint32_t>* indices;

	float vertColor[4]{ 1, 0, 0, 1 };
};

