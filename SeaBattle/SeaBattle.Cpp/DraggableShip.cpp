#include "stdafx.h"
#include "Ship.h"
#include "DraggableShip.h"

DraggableShip::DraggableShip(int length) : Ship(length)
{
};

DraggableShip^ DraggableShip::From(Ship ^ship)
{
	DraggableShip^ draggableShip = gcnew DraggableShip(ship->Length);
    draggableShip->X = ship->X;
    draggableShip->Y = ship->Y;
    draggableShip->Orientation = ship->Orientation;
    draggableShip->Source = ship;

	return draggableShip;
};