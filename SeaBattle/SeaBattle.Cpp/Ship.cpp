#include "stdafx.h"
#include "Ship.h"
#include "Rect.h"
#include "Enums.h"

Ship::Ship(int length)
{
    Ship(length, ShipOrientation::Horizontal);
};

Ship::Ship(int length, ShipOrientation orientation)
{
    Length = length;
    Orientation = orientation;
};

bool Ship::IsDrowned::get()
{
	return HitCount == Length;
};

bool Ship::IsLocatedAt(int x, int y)
{
	Rect^ rect = GetShipRegion();
    return (x >= rect->X 
            && 
            x <= rect->Right 
            && 
            y >= rect->Y 
            && 
            y <= rect->Bottom);
};

Rect^ Ship::GetShipRegion()
{
	int width = Orientation == ShipOrientation::Horizontal ? Length : 1;
	int height = Orientation == ShipOrientation::Vertical ? Length : 1;

	return gcnew Rect(X, Y, width, height);
};

bool Ship::IsInRegion(Rect^ rect)
{
	Rect^ r = GetShipRegion();
	return (rect->IntersectsWith(r));
}

void Ship::MoveTo(int x, int y)
{
	X = x;
	Y = y;
}

void Ship::Rotate()
{
	Orientation = Orientation == ShipOrientation::Horizontal ? ShipOrientation::Vertical : ShipOrientation::Horizontal;
}