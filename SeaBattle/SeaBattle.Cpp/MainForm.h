#pragma once
#include "stdafx.h"
#include "Player.h"
#include "Board.h"
#include "ScoreBoard.h"
#include "HumanPlayer.h"
#include "ComputerPlayer.h"
#include "GameController.h"

private ref class MainForm : Form
{
	private:
		initonly Player^ _humanPlayer;
		initonly Player^ _computerPlayer;

		initonly Board^ _humanBoard;
		initonly Board^ _computerBoard;

		initonly GameController^ _controller;

		initonly ScoreBoard^ _scoreboard;

		initonly Button^ _shuffleButton;
		initonly Button^ _startGameButton;
		initonly Button^ _newGameButton;

		static initonly Color ButtonBackColor = Color::FromArgb(65, 133, 243);
		literal Char ShuffleCharacter = (Char)(0x60);
		literal Char StartGameCharacter = (Char)(0x55);
		literal Char NewGameCharacter = (Char)(0x6C);

        void OnNewGameButtonClick(Object^ sender, System::EventArgs^ e);
		void StartNewGame();
		void OnStartGameButtonClick(Object^ sender, System::EventArgs^ e);
		void OnShuffleButtonClick(Object^ sender, System::EventArgs^ e);
		void OnGameEnded(Object^ sender, System::EventArgs^ e);

        void SetupWindow();
		static Button^ CreateButton(String^ text, Color backColor);
		void LayoutControls();

	public:
		MainForm();
};
