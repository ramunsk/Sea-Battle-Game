#include "stdafx.h"
#include "BoardCell.h"

BoardCell::BoardCell(int x, int y)
{
	X = x;
	Y = y;
	AutoSize = false;
	TextAlign = ContentAlignment::MiddleCenter;
	BackColor = Color::LightBlue;
	Font = gcnew System::Drawing::Font("Webdings", 10);
	AllowDrop = true;
}

void BoardCell::OnCellStateChenged()
{
	SuspendLayout();
	switch (_state)
	{
		case BoardCellState::Normal:
			Text = String::Empty;
			BackColor = DefaultBackgroundColor;
			break;
		case BoardCellState::MissedShot:
			Text = MissedHitChar->ToString();
			BackColor = DefaultBackgroundColor;
			break;
		case BoardCellState::Ship:
			Text = String::Empty;
			BackColor = ShipColor;
			break;
		case BoardCellState::ShotShip:
			Text = ShipHitChar->ToString();
			BackColor = ShipColor;
			break;
		case BoardCellState::ShipDrag:
			BackColor = DragOverBackgroundColor;
			Text = String::Empty;
			break;
		case BoardCellState::ShipDragInvalid:
			BackColor = DragOverInvalidBackgroundColor;
			Text = String::Empty;
			break;
		case BoardCellState::ShowDrowned:
			BackColor = ShipDrownedColor;
			Text = ShipHitChar->ToString();
			break;
	}
	Invalidate();
	ResumeLayout();
}

void BoardCell::OnPaint(PaintEventArgs ^e)
{
	Label::OnPaint(e);

	Pen^ pen = gcnew Pen(DefaultBorderColor);
	pen->Alignment = PenAlignment::Inset;
	pen->DashStyle = DashStyle::Solid;

	Rectangle rect = ClientRectangle;
	rect.Height -= 1;
	rect.Width -= 1;

	e->Graphics->DrawRectangle(pen, rect);

    delete pen;
}

BoardCellState BoardCell::State::get()
{
	return _state;
}

void BoardCell::State::set(BoardCellState value)
{
	_state = value;
	OnCellStateChenged();
}