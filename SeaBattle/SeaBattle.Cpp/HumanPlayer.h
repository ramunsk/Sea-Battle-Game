#pragma once
#include "stdafx.h"
#include "Player.h"
#include "Board.h"
#include "BoardCellClickEventArgs.h"

ref class HumanPlayer : Player
{
	private:
		initonly Board^ _board;
        void OnBoardClick(Object^ sender, BoardCellClickEventArgs^ e);

	public:
		HumanPlayer(String^ name, Board^ board);
};
