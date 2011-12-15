#include "stdafx.h"
#include "ShootingEventArgs.h"

ShootingEventArgs::ShootingEventArgs(int x, int y)
{
	_x = x;
	_y = y;
}

int ShootingEventArgs::X::get()
{
	return _x;
}

int ShootingEventArgs::Y::get()
{
	return _y;
}