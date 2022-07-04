#include "Mesh.h"

#define TEXCOORD_TOPLEFT 0, 0
#define TEXCOORD_TOPRIGHT 1, 0
#define TEXCOORD_BOTLEFT 0, 1
#define TEXCOORD_BOTRIGHT 1, 1

Mesh::Mesh(std::string meshFile)
{
	vertices = new std::vector<VertexType>();
	indices = new std::vector<uint32_t>();

	std::vector<std::array<float, 3>> vertBuffer;
	std::vector<std::array<float, 2>> uvBuffer;
	std::vector<int> vertIndex;
	std::vector<int> uvIndex;

	std::ifstream meshf(meshFile, std::ios::in);	//open file stream
	if (!meshf)
	{
		ErrorHandler::Error("Meshfile could not be opened.");
	}
	else
	{
		std::string line;
		while (std::getline(meshf, line))
		{
			if (line.substr(0, 2) == "v ")	//Vertices
			{
				std::istringstream v(line.substr(2));

				float x, y, z;
				v >> x;
				v >> y;
				v >> z;
				std::array<float, 3> vert { x, y, z };

				vertBuffer.push_back(vert);	//Die Werte aus dem string stream in die buffer variable packen
			}
			else if (line.substr(0, 2) == "vt") //UVs
			{
				std::istringstream v(line.substr(3));

				float U, V;
				v >> U;
				v >> V;
				std::array<float, 2> tex { -U, -V };

				uvBuffer.push_back(tex);	//Die Werte aus dem string stream in die buffer variable packen
			}
			else if (line.substr(0, 2) == "f ") {
				unsigned int v1, v2, v3; //Die Indizes der Vertices werden hier gespeichert
				unsigned int t1, t2, t3; //Die Indizes der UV Koordinaten werden hier gespeichert
				unsigned int n1, n2, n3; //Die Indizes der vertex Normalen werden hier gespeichert (werden aber nicht verwendet)

				//die zeile in ein char pointer konvertieren
				const char* chh = line.c_str();
				//die zeile nach einem format auslesen und in die temp. variablen packen 
				sscanf_s(chh, "f %i/%i/%i %i/%i/%i %i/%i/%i", &v1, &t1, &n1, &v2, &t2, &n2, &v3, &t3, &n3);

				//Da die indizes ab 1 anfangen müssen wir hier jeweils jeden Wert um 1 senken
				v1--; v2--; v3--; t1--; t2--; t3--;

				//in die jeweiligen buffer variablen packen
				vertIndex.push_back(v1); vertIndex.push_back(v2); vertIndex.push_back(v3);
				uvIndex.push_back(t1);   uvIndex.push_back(t2);   uvIndex.push_back(t3);
			}
		}

		for (int i = 0; i < vertIndex.size(); i++)
		{
			VertexType t = {	vertBuffer.at(vertIndex.at(i)).at(0), 
								vertBuffer.at(vertIndex.at(i)).at(1),
								vertBuffer.at(vertIndex.at(i)).at(2),
								uvIndex.empty() ? 0 : uvBuffer.at(uvIndex.at(i)).at(0), 
								uvIndex.empty() ? 0 : uvBuffer.at(uvIndex.at(i)).at(1)	};

			vertices->push_back(t);
			indices->push_back(i);
		}
	}

}

Mesh::Mesh(MeshPrimitive primitive)
{
	vertices = new std::vector<VertexType>();
	indices = new std::vector<uint32_t>();

	switch (primitive)
	{
	case MeshPrimitive::Plane :
		vertices->push_back(VertexType(+1.0f, -1.0f,  0.0f, TEXCOORD_BOTRIGHT));
		vertices->push_back(VertexType(-1.0f, -1.0f,  0.0f, TEXCOORD_BOTLEFT));
		vertices->push_back(VertexType(+1.0f, +1.0f,  0.0f, TEXCOORD_TOPRIGHT));
		vertices->push_back(VertexType(-1.0f, +1.0f,  0.0f, TEXCOORD_TOPLEFT));

		indices->insert(indices->end(), { 0, 1, 2 }); //Front
		indices->insert(indices->end(), { 1, 2, 3 });
		indices->insert(indices->end(), { 2, 1, 3 }); //Back
		indices->insert(indices->end(), { 0, 2, 1 });
		break;
	case MeshPrimitive::Cube :
		vertices->push_back(VertexType(+1.0f, -1.0f,  0.0f, TEXCOORD_BOTRIGHT));
		vertices->push_back(VertexType(-1.0f, -1.0f,  0.0f, TEXCOORD_BOTLEFT));		
		vertices->push_back(VertexType(+1.0f, +1.0f,  0.0f, TEXCOORD_TOPRIGHT));
		vertices->push_back(VertexType(-1.0f, +1.0f,  0.0f, TEXCOORD_TOPLEFT));

		vertices->push_back(VertexType(+1.0f, -1.0f,  2.0f, TEXCOORD_BOTRIGHT));
		vertices->push_back(VertexType(-1.0f, -1.0f,  2.0f, TEXCOORD_BOTLEFT));
		vertices->push_back(VertexType(+1.0f, +1.0f,  2.0f, TEXCOORD_TOPRIGHT));
		vertices->push_back(VertexType(-1.0f, +1.0f,  2.0f, TEXCOORD_TOPLEFT));

		indices->insert(indices->end(), { 0, 1, 2 }); //Front
		indices->insert(indices->end(), { 2, 1, 3 });

		indices->insert(indices->end(), { 7, 5, 4 }); //Back
		indices->insert(indices->end(), { 4, 6, 7 });

		indices->insert(indices->end(), { 2, 3, 7 }); //Top
		indices->insert(indices->end(), { 2, 7, 6 });

		indices->insert(indices->end(), { 0, 5, 1 }); //Bottom
		indices->insert(indices->end(), { 0, 4, 5 });

		indices->insert(indices->end(), { 0, 2, 4 }); //Right
		indices->insert(indices->end(), { 4, 2, 6 });

		indices->insert(indices->end(), { 5, 7, 3 }); //Left
		indices->insert(indices->end(), { 3, 1, 5 });

		//INCLUDES NO DUPLICATION OF VERTECIES, MEANING THAT THESE VERTECIES ONLY HAVE 2 TEXTURED FACES
		break;
	default:
		break;
	}
}

Mesh::~Mesh()
{
	delete[] vertices;
}