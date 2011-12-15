#pragma once
#include "stdafx.h"

ref class BoardCellClickEventArgs : EventArgs
{
	private:
		initonly int _x;
		initonly int _y;

	public:
		BoardCellClickEventArgs(int x, int y);
		property int Y
		{
			int get();
		}
		property int X
		{
			int get();
		}

};