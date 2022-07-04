#pragma once
#include <iostream>
#include <typeinfo>  // typeid()
#include <typeindex>
#include <unordered_map>
#include <memory>
#include <DirectXMath.h>

#include "IComponent.h"

template <typename T>
using ComponentMap_t = std::unordered_map<std::type_index, T>;

class IGameObject
{
public:
    virtual void Start() = 0;
    virtual void Update() = 0;
    virtual void End() = 0;

    //void SetPos(float x, float y, float z) { position[0] = x; position[1] = y; position[2] = z; }
    //void SetRot(float x, float y, float z) { rotation[0] = x; rotation[1] = y; rotation[2] = z; }
    //void SetScl(float x, float y, float z) {    scale[0] = x;    scale[1] = y;    scale[2] = z; }

    //XMFLOAT3 GetPos() { return XMFLOAT3(position[0], position[1], position[2]); }
    //XMFLOAT3 GetRot() { return XMFLOAT3(rotation[0], rotation[1], rotation[2]); }
    //XMFLOAT3 GetScl() { return XMFLOAT3(   scale[0],    scale[1],    scale[2]); }

    //______ENTITY COMPONENT SYSTEM FOR GAMEOBJECT______
    template <typename TComponent>
    void addComponent(std::shared_ptr<TComponent> aComponent)   //STORE A COMPONENT POINTER IN MAP
    {
        if (nullptr == aComponent)
        {
            return;
        }

        std::type_index key(typeid(TComponent));
        if (mComponents.end() != mComponents.find(key))
        {
            return;
        }

        mComponents.insert({ key, aComponent });
    }

    template <typename TComponent>
    void removeComponent() //REMOVE A COMPONENT AND ITS POINTER IN MAP
    {
        std::type_index key(typeid(TComponent));
        mComponents.erase(key);
    }

    template <typename TComponent>
    std::shared_ptr<TComponent> const get() const   //RETURN COMPONENT
    {
        std::type_index key(typeid(TComponent));
        if (mComponents.end() == mComponents.find(key))
        {
            return nullptr;
        }

        std::shared_ptr<IComponent> component = mComponents.at(key);

        return std::static_pointer_cast<TComponent>(component);
    }

protected:    
    float position[3];
    float rotation[3];
    float scale[3];

    ComponentMap_t<std::shared_ptr<IComponent>> mComponents;
};