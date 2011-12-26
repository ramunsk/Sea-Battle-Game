#pragma once
#include "stdafx.h"
#include "Player.h"
#include "Pair.h"

ref class ScoreBoard : TableLayoutPanel
{
	private:
		initonly Player^ _player1;
		initonly Player^ _player2;
		initonly int _shipsPerGame;
		initonly int _shotsPerGame;
		initonly Label^ _scoreLabel;

		literal String^ PlayerStatsTemplate = "Liko laivø: {0}, Liko ðûviø: {1}";
		literal String^ ScoreTemplate = "{0} : {1}";

		static initonly Color ActivePlayerColor = Color::FromArgb(255,174,0);
		static initonly Color InactivePlayerColor = Color::FromArgb(128, 128, 128);
		static initonly Color PlayerStatsColor = Color::FromArgb(128, 128, 128);
		static initonly Color WinnerColor = Color::FromArgb(32, 167, 8);
		static initonly Color LooserColor = Color::FromArgb(222, 0, 0);
		static initonly Color ScoreColor = Color::Black;

		initonly Pair<Label^, Label^>^ _playerNames;
		initonly Pair<Label^, Label^>^ _playerStats;

		Point _score;
		Point _shipsLeft;
		Point _shotsLeft;

        void InitPlayerStats();
		static Label^ CreateLabel(String^ text, Color color);
		void OnPlayerMadeShot(Object^ sender, ShootingEventArgs^ e);
		void RefreshPlayerStats();
		void OnPlayerTurnChanged(Object^ sender, EventArgs^ e);
   		void TrackResult();
		void RefreshScore();
		void OnGameEnded();
  		void AddLayoutColumns();
		void AddLayoutRows();

    public:
		ScoreBoard(Player^ player1, Player^ player2, int shipsPerGame, int shotsPerGame);
		bool GameHasEnded();
		event EventHandler^ GameEnded;
		void NewGame();


    protected:
		 virtual void InitLayout() override;
};