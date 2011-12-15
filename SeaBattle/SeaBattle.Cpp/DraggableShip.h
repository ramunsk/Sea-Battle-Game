#pragma once
#include "stdafx.h"
#include "Ship.h"

ref class DraggableShip : Ship
{
    private:
        DraggableShip(int length);

    public:
        property Ship^ Source;
        static DraggableShip^ From(Ship^ ship);
        property bool IsOrientationModified;
};
