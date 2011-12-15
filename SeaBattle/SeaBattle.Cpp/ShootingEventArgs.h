#pragma once
#include "stdafx.h"
#include "Enums.h"

ref class ShootingEventArgs : EventArgs
{
	private:
		initonly int _x;
		initonly int _y;

	public:
		ShootingEventArgs(int x, int y);
		property int X
		{
			int get();
		}
		property int Y
		{
			int get();
		}
		property ShotResult Result;
};