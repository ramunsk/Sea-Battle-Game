#include "stdafx.h"
#include "BoardCellClickEventArgs.h"

BoardCellClickEventArgs::BoardCellClickEventArgs(int x, int y)
{
	_x = x;
	_y = y;
};

int BoardCellClickEventArgs::Y::get()
{
	return _y;
};

int BoardCellClickEventArgs::X::get()
{
	return _x;
};