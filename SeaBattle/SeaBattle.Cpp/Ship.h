#pragma once
#include "stdafx.h"
#include "Enums.h"
#include "Rect.h"

ref class Ship
{
    public:
        Ship(int length);
        Ship(int length, ShipOrientation orientation);
        property int X;
        property int Y;
        property int Length;
        property ShipOrientation Orientation;
        property int HitCount;
        property bool IsDrowned
        {
	        bool get();
        }

        bool IsLocatedAt(int x, int y);
        Rect^ GetShipRegion();
        bool IsInRegion(Rect^ rect);
        void MoveTo(int x, int y);
        void Rotate();
};
