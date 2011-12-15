#include "stdafx.h"
#include "Player.h"
#include "HumanPlayer.h"
#include "Board.h"

HumanPlayer::HumanPlayer(String^ name, Board^ board) : Player(name)
{
	_board = board;
	_board->OnClick += gcnew EventHandler<BoardCellClickEventArgs^>(this,&HumanPlayer::OnBoardClick);
};

void HumanPlayer::OnBoardClick(Object^ sender, BoardCellClickEventArgs^ e)
{
	if (PastShots->ContainsKey(Point(e->X, e->Y)))
		return;

	ShotTargetChosen(e->X, e->Y);
};