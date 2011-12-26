#include "stdafx.h"
#include "ScoreBoard.h"

ScoreBoard::ScoreBoard(Player^ player1, Player^ player2, int shipsPerGame, int shotsPerGame)
{
	SuspendLayout();
	_player1 = player1;
	_player2 = player2;
	_shipsPerGame = shipsPerGame;
	_shotsPerGame = shotsPerGame;

	_player1->MyTurn += gcnew EventHandler(this, &ScoreBoard::OnPlayerTurnChanged);
	_player2->MyTurn += gcnew EventHandler(this, &ScoreBoard::OnPlayerTurnChanged);

	_player1->Shot += gcnew EventHandler<ShootingEventArgs^>(this, &ScoreBoard::OnPlayerMadeShot);
	_player2->Shot += gcnew EventHandler<ShootingEventArgs^>(this, &ScoreBoard::OnPlayerMadeShot);

	Label^ firstPlayerNameLabel = CreateLabel(_player1->Name, InactivePlayerColor);
	Label^ secondPlayerNameLabel = CreateLabel(_player2->Name, InactivePlayerColor);
	_playerNames = gcnew Pair<Label^, Label^>(firstPlayerNameLabel, secondPlayerNameLabel);

	Label^ firstPlayerStatsLabel = CreateLabel(String::Empty, PlayerStatsColor);
	Label^ secondPlayerStatsLabel = CreateLabel(String::Empty, PlayerStatsColor);
	_playerStats = gcnew Pair<Label^, Label^>(firstPlayerStatsLabel, secondPlayerStatsLabel);

	_scoreLabel = CreateLabel("", ScoreColor);

	RefreshScore();
	InitPlayerStats();

	ResumeLayout();
};

void ScoreBoard::InitPlayerStats()
{
	_shipsLeft = Point(_shipsPerGame, _shipsPerGame);
	_shotsLeft = Point(_shotsPerGame, _shotsPerGame);
	RefreshPlayerStats();
}

Label^ ScoreBoard::CreateLabel(String^ text, Color color)
{
    Label^ label = gcnew Label;
    label->AutoSize = true;
    label->Text = text;
    label->Dock = DockStyle::Fill;
    label->Margin = Windows::Forms::Padding::Empty;
    label->Padding = Windows::Forms::Padding::Empty;
    label->ForeColor = color;
    label->TextAlign = ContentAlignment::TopLeft;

    return label;
};

void ScoreBoard::OnPlayerMadeShot(Object^ sender, ShootingEventArgs^ e)
{
	if (sender == _player1)
	{
		_shotsLeft.X--;
		if (e->Result == ShotResult::ShipDrowned)
			_shipsLeft.Y--;
	}
	else
	{
		_shotsLeft.Y--;
		if (e->Result == ShotResult::ShipDrowned)
			_shipsLeft.X--;
	}

	TrackResult();
	RefreshPlayerStats();
};

void ScoreBoard::RefreshPlayerStats()
{
	_playerStats->First->Text = String::Format(PlayerStatsTemplate, _shipsLeft.X, _shotsLeft.X);
	_playerStats->Second->Text = String::Format(PlayerStatsTemplate, _shipsLeft.Y, _shotsLeft.Y);
};

void ScoreBoard::OnPlayerTurnChanged(Object^ sender, EventArgs^ e)
{
	Color color1 = sender == _player1 ? ActivePlayerColor : InactivePlayerColor;
	Color color2 = sender == _player2 ? ActivePlayerColor : InactivePlayerColor;

	_playerNames->First->ForeColor = color1;
	_playerNames->Second->ForeColor = color2;
};

void ScoreBoard::TrackResult()
{
	if (!GameHasEnded())
		return;

	Color color1;
	Color color2;

	if (_shipsLeft.X == 0)
	{
		_score.Y++;
		color1 = LooserColor;
		color2 = WinnerColor;
	}
	else
	{
		_score.X++;
		color1 = WinnerColor;
		color2 = LooserColor;
	}

	_playerNames->First->ForeColor = color1;
	_playerNames->Second->ForeColor = color2;

	OnGameEnded();

	GameEnded(this, EventArgs::Empty);
};

void ScoreBoard::RefreshScore()
{
	_scoreLabel->Text = String::Format(ScoreTemplate, _score.X, _score.Y);
};

void ScoreBoard::OnGameEnded()
{
	RefreshScore();
};

void ScoreBoard::AddLayoutColumns()
{

	ColumnCount = 3;
	ColumnStyles->Add(gcnew ColumnStyle(SizeType::Percent, 40));
	ColumnStyles->Add(gcnew ColumnStyle(SizeType::Percent, 20));
	ColumnStyles->Add(gcnew ColumnStyle(SizeType::Percent, 40));
}

void ScoreBoard::AddLayoutRows()
{
	RowCount = 2;
	RowStyles->Add(gcnew RowStyle(SizeType::AutoSize, 0));
	RowStyles->Add(gcnew RowStyle(SizeType::AutoSize, 0));
}

bool ScoreBoard::GameHasEnded()
{
	return _shipsLeft.X == 0 || _shipsLeft.Y == 0;
};

void ScoreBoard::NewGame()
{
	InitPlayerStats();
	RefreshPlayerStats();
	_playerNames->First->ForeColor = InactivePlayerColor;
	_playerNames->Second->ForeColor = InactivePlayerColor;
};


void ScoreBoard::InitLayout()
{
   TableLayoutPanel::InitLayout();
   Padding = Windows::Forms::Padding::Empty;
   Margin = Windows::Forms::Padding::Empty;
   Font = Parent->Font;

   AddLayoutColumns();
   AddLayoutRows();

   _playerNames->First->Font = gcnew Drawing::Font(Font->FontFamily,24);
   Controls->Add(_playerNames->First, 0, 0);

   _playerNames->Second->Font = gcnew Drawing::Font(Font->FontFamily, 24);
   _playerNames->Second->TextAlign = ContentAlignment::TopRight;
   Controls->Add(_playerNames->Second, 2, 0);

   _scoreLabel->Font = gcnew Drawing::Font(Font->FontFamily, 30, FontStyle::Bold);
   _scoreLabel->TextAlign = ContentAlignment::TopCenter;
   Controls->Add(_scoreLabel, 1, 0);

   _playerStats->First->Font = Font;
   Controls->Add(_playerStats->First, 0, 1);

   _playerStats->Second->Font = Font;
   _playerStats->Second->TextAlign = ContentAlignment::TopRight;
   Controls->Add(_playerStats->Second, 2, 1);

   Height = _playerNames->First->Height + _playerStats->First->Height;
};

