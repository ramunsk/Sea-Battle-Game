#include "stdafx.h"
#include "MainForm.h"

MainForm::MainForm()
{
	SuspendLayout();

	_humanBoard = gcnew Board();
	_computerBoard = gcnew Board(false);

	_humanPlayer = gcnew HumanPlayer("Þaidëjas", _computerBoard);
	_computerPlayer = gcnew ComputerPlayer("Kompiuteris");


	_scoreboard = gcnew ScoreBoard(_humanPlayer, _computerPlayer, 10, 100);
	_controller = gcnew GameController(_humanPlayer, _computerPlayer, _humanBoard, _computerBoard, _scoreboard);

	_shuffleButton = CreateButton(ShuffleCharacter.ToString(), ButtonBackColor);
	_newGameButton = CreateButton(NewGameCharacter.ToString(), ButtonBackColor);
	_startGameButton = CreateButton(StartGameCharacter.ToString(), ButtonBackColor);

	SetupWindow();
	LayoutControls();

	_scoreboard->GameEnded += gcnew EventHandler(this, &MainForm::OnGameEnded);

	_shuffleButton->Click += gcnew System::EventHandler(this, &MainForm::OnShuffleButtonClick);
	_startGameButton->Click += gcnew System::EventHandler(this, &MainForm::OnStartGameButtonClick);
	_newGameButton->Click += gcnew System::EventHandler(this, &MainForm::OnNewGameButtonClick);

	ResumeLayout();

	StartNewGame();
};

void MainForm::OnNewGameButtonClick(Object^ sender, EventArgs^ e)
{
	StartNewGame();
};

void MainForm::StartNewGame()
{
	_shuffleButton->Visible = true;
	_startGameButton->Visible = true;
	_newGameButton->Visible = false;
	_controller->NewGame();
};

void MainForm::OnStartGameButtonClick(Object^ sender, EventArgs^ e)
{
	_shuffleButton->Visible = false;
	_newGameButton->Visible = false;
	_startGameButton->Visible = false;
	_controller->StartGame();
};

void MainForm::OnShuffleButtonClick(Object^ sender, EventArgs^ e)
{
	_humanBoard->AddRandomShips();
};

void MainForm::OnGameEnded(Object^ sender, EventArgs^ e)
{
	_shuffleButton->Visible = false;
	_startGameButton->Visible = false;
	_newGameButton->Visible = true;
	_computerBoard->ShowShips();
};

void MainForm::SetupWindow()
{
	AutoScaleDimensions = SizeF(8, 19);
	AutoScaleMode = Windows::Forms::AutoScaleMode::Font;
	Font = gcnew Drawing::Font("Calibri", 10, FontStyle::Regular, GraphicsUnit::Point, 186);
	Margin = Windows::Forms::Padding::Empty;
	Text = "SeaBattle.C++";
	BackColor = Color::FromArgb(235, 235, 235);
	FormBorderStyle = Windows::Forms::FormBorderStyle::FixedSingle;
	StartPosition = FormStartPosition::CenterScreen;
	MaximizeBox = false;
};

Button^ MainForm::CreateButton(String^ text, Color backColor)
{
	Button^ button = gcnew Button;
    button->FlatStyle = FlatStyle::Flat;
    button->ForeColor = Color::White;
    button->BackColor = backColor;
    button->UseVisualStyleBackColor = false;
    button->Size = Drawing::Size(40, 40);
    button->Text = text;
    button->Font = gcnew Drawing::Font("Webdings", 24, FontStyle::Regular, GraphicsUnit::Point);
    button->TextAlign = ContentAlignment::TopCenter;
	button->FlatAppearance->BorderSize = 0;

	return button;
};

void MainForm::LayoutControls()
{
	_humanBoard->Location = Point(0, 0);
	_computerBoard->Location = Point(_humanBoard->Right, 0);
	_scoreboard->Location = Point(25, _humanBoard->Bottom);
	_scoreboard->Width = _computerBoard->Right - 25;
	_newGameButton->Location = Point(_computerBoard->Right - _newGameButton->Width, _scoreboard->Bottom);
	_startGameButton->Location = _newGameButton->Location;
	_shuffleButton->Location = Point(_newGameButton->Location.X - _shuffleButton->Width - 25, _newGameButton->Location.Y);

	Controls->AddRange(gcnew array<Control^> {_humanBoard, _computerBoard, _scoreboard, _newGameButton, _startGameButton, _shuffleButton});

	ClientSize = Drawing::Size(_computerBoard->Right + 25, _startGameButton->Bottom + 25);
}