#pragma once
#include "stdafx.h"
#include "Player.h"
#include "Board.h"
#include "ScoreBoard.h"


ref class GameController
{
	private:
		initonly Player^ _player1;
		initonly Player^ _player2;
		initonly Board^ _board1;
		initonly Board^ _board2;
		initonly ScoreBoard^ _scoreBoard;
   		void OnPlayerShotShot(Object^ sender, ShootingEventArgs^ e);
		void OnPlayerShooting(Object^ sender, ShootingEventArgs^ e);

	public:
		GameController(Player^ player1, Player^ player2, Board^ board1, Board^ board2, ScoreBoard^ scoreBoard);

	public:
		void NewGame();
		void StartGame();
};
