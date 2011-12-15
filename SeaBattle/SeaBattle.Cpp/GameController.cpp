#include "stdafx.h"
#include "MainForm.h"
#include "GameController.h"
#include "ShootingEventArgs.h"

GameController::GameController(Player^ player1, Player^ player2, Board^ board1, Board^ board2, ScoreBoard^ scoreBoard)
{
	_player1 = player1;
	_player2 = player2;
	_board1 = board1;
	_board2 = board2;
	_scoreBoard = scoreBoard;

	_player1->Shooting += gcnew EventHandler<ShootingEventArgs^>(this, &GameController::OnPlayerShooting);
	_player2->Shooting += gcnew EventHandler<ShootingEventArgs^>(this, &GameController::OnPlayerShooting);

	_player1->Shot += gcnew EventHandler<ShootingEventArgs^>(this, &GameController::OnPlayerShotShot);;
	_player2->Shot += gcnew EventHandler<ShootingEventArgs^>(this, &GameController::OnPlayerShotShot);;

};

void GameController::OnPlayerShotShot(Object^ sender, ShootingEventArgs^ e)
{
	if (_scoreBoard->GameHasEnded())
		return;

	Player^ shooter = safe_cast<Player^>(sender);
	Player^ openent = shooter == _player1 ? _player2 : _player1;

	if (e->Result != ShotResult::Missed)
	{
		shooter->Shoot();
	}
	else
	{
		openent->Shoot();
	}
};

void GameController::OnPlayerShooting(Object^ sender, ShootingEventArgs^ e)
{
	Player^ shooter = safe_cast<Player^>(sender);
	Board^ oponentBoard;
	
    if (shooter == _player1)
	{
		oponentBoard = _board2;
	}
	else
	{
		oponentBoard = _board1;
	}

	ShotResult shotResult = oponentBoard->OpenentShotAt(e->X, e->Y);
	e->Result = shotResult;
};

void GameController::NewGame()
{
	_board1->Mode = BoardMode::Design;
	_board2->Mode = BoardMode::Design;
	_board1->AddRandomShips();
	_board2->AddRandomShips();
	_player1->Reset();
	_player2->Reset();
	_scoreBoard->NewGame();
}

void GameController::StartGame()
{
	int playerIndex = (gcnew Random(DateTime::Now.Millisecond))->Next(1, 3);
	Player^ player = playerIndex == 1 ? _player1 : _player2;

	_board1->Mode = BoardMode::Game;
	_board2->Mode = BoardMode::Game;

	_scoreBoard->NewGame();
	player->Shoot();
}