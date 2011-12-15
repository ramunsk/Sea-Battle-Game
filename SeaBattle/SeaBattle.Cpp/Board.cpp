#include "stdafx.h"
#include "Board.h"
#include "Rect.h"

Board::Board()
{
   _drawShips = true;
	_cells = gcnew array<BoardCell^, 2>(10, 10);
	_ships = gcnew List<Ship^>;
	_rnd = gcnew Random(DateTime::Now.Millisecond);
	Mode = BoardMode::Design;
	Margin = Padding.Empty;

	CreateBoard();
}

Board::Board(bool drawShips)
{
	_drawShips = drawShips;
	_cells = gcnew array<BoardCell^, 2>(10, 10);
	_ships = gcnew List<Ship^>;
	_rnd = gcnew Random(DateTime::Now.Millisecond);
	Mode = BoardMode::Design;
	Margin = Padding.Empty;

	CreateBoard();
}

Label^ Board::CreateHeaderCell(int x, int y, String^ text)
{
	Label^ cell = gcnew Label;
    
    cell->AutoSize = false;
    cell->BackColor = Color::Transparent;
    cell->TextAlign = ContentAlignment::MiddleCenter;
    cell->Text = text;
    cell->Location = Point(x, y);
    cell->Width = CellSize;
    cell->Height = CellSize;

	return cell;
};

void Board::CreateHeaders()
{
	for (int i = 0; i < BoardRegion->Width; i++)
	{
		int offset = CellSize * i + CellSize;
		Label^ columnHeader = CreateHeaderCell(offset, 0, (i + 1).ToString());
		Label^ rowHeader = CreateHeaderCell(0, offset, (i + 1).ToString());

		Controls->Add(columnHeader);
		Controls->Add(rowHeader);
	}
};

void Board::CreateBoard()
{
	SuspendLayout();

    CreateHeaders();

	IList<Point>^ points = BoardRegion->GetPoints();

	for each (Point point in points)
	{
		BoardCell^ cell = gcnew BoardCell(point.X, point.Y);
        cell->Top = point.X * CellSize + CellSize;
        cell->Left = point.Y * CellSize + CellSize;
        cell->Width = CellSize;
        cell->Height = CellSize;
        cell->State = BoardCellState::Normal;
		
		cell->MouseDown += gcnew MouseEventHandler(this, &Board::OnCellMouseDown);
		cell->DragEnter += gcnew DragEventHandler(this, &Board::OnCellDragEnter);
		cell->DragLeave += gcnew EventHandler(this, &Board::OnCellDragLeave);
		cell->DragDrop += gcnew DragEventHandler(this, &Board::OnCellDragDrop);
		cell->QueryContinueDrag += gcnew QueryContinueDragEventHandler(this, &Board::OnCellQueryContinueDrag);
		cell->Click += gcnew EventHandler(this, &Board::OnCellClick);

		_cells[point.X, point.Y] = cell;
        
        Controls->Add(cell);
	}

    delete points;

    Drawing::Size boardSize = Drawing::Size(CellSize * BoardRegion->Width + CellSize, CellSize * BoardRegion->Height + CellSize);
	MinimumSize = boardSize;
	MaximumSize = boardSize;

	ResumeLayout();
};

void Board::RedrawRegion(Rect^ region)
{
	SuspendLayout();

	IList<Point>^ points = region->GetPoints();
	for each (Point point in points)
	{
		if (!BoardRegion->Contains(point))
		{
			continue;
		}

		Ship^ ship = GetShipAt(point.X, point.Y);
		_cells[point.X, point.Y]->State = ship == nullptr ? BoardCellState::Normal : BoardCellState::Ship;
	}

    delete points;

	ResumeLayout();
};

void Board::DrawShip(Ship^ ship, BoardCellState state)
{
	DrawShip(ship, state, false);
};

void Board::DrawShip(Ship^ ship, BoardCellState state, bool force)
{
	if (!_drawShips && !force)
		return;

	IList<Point>^ points = ship->GetShipRegion()->GetPoints();

	for each (Point point in points)
	{
        System::Diagnostics::Debug::WriteLine("B");
		if (BoardRegion->Contains(point))
		{
			_cells[point.X, point.Y]->State = state;
		}
	}

    delete points;
};

void Board::OnCellClick(Object^ sender, EventArgs^ e)
{
	if (Mode != BoardMode::Game)
		return;

    BoardCell^ cell = safe_cast<BoardCell^>(sender);
	BoardCellClickEventArgs^ eventArgs = gcnew BoardCellClickEventArgs(cell->X, cell->Y);
	OnClick(this, eventArgs);
};

void Board::OnCellMouseDown(Object^ sender, MouseEventArgs^ e)
{
	if (Mode == BoardMode::Game || !_drawShips)
		return;

	BoardCell^ cell = safe_cast<BoardCell^>(sender);
	Ship^ ship = GetShipAt(cell->X, cell->Y);

	if (ship == nullptr)
	{
		return;
	}
	_draggedShip = DraggableShip::From(ship);
	cell->DoDragDrop(ship, DragDropEffects::Copy | DragDropEffects::Move);
};

void Board::OnCellQueryContinueDrag(Object^ sender, QueryContinueDragEventArgs^ e)
{
	// check Ctrl key state
	bool shouldRotate = ((e->KeyState & 8) == 8);
	bool isRotated = _draggedShip->IsOrientationModified;

	if ((shouldRotate && isRotated) || (!shouldRotate && !isRotated))
		return;

	Rect^ rect = _draggedShip->GetShipRegion();
	RedrawRegion(rect);

    delete rect;

	_draggedShip->Rotate();
	_draggedShip->IsOrientationModified = !isRotated;

	BoardCellState state = CanPlaceShip(_draggedShip, _draggedShip->X, _draggedShip->Y) ? BoardCellState::ShipDrag : BoardCellState::ShipDragInvalid;
	DrawShip(_draggedShip, state);
};

void Board::OnCellDragEnter(Object^ sender, DragEventArgs^ e)
{
   
	if (e->Data->GetDataPresent(Ship::typeid))
	{
		BoardCell^ cell = safe_cast<BoardCell^>(sender);
		_draggedShip->MoveTo(cell->X, cell->Y);

		bool canPlaceShip = CanPlaceShip(_draggedShip, cell->X, cell->Y);
		BoardCellState state = canPlaceShip ? BoardCellState::ShipDrag : BoardCellState::ShipDragInvalid;

		DrawShip(_draggedShip, state);

		e->Effect = canPlaceShip ? DragDropEffects::Move : DragDropEffects::None;
	}
	else
	{
		e->Effect = DragDropEffects::None;
	}
};

void Board::OnCellDragLeave(Object^ sender, EventArgs^ e)
{
	Rect^ rect = _draggedShip->GetShipRegion();
	RedrawRegion(rect);
    delete rect;
};

void Board::OnCellDragDrop(Object^ sender, DragEventArgs^ e)
{
   BoardCell^ cell = safe_cast<BoardCell^>(sender);
   if (e->Data->GetDataPresent(Ship::typeid))
   {
	   if (!CanPlaceShip(_draggedShip, cell->X, cell->Y))
		   return;

	   Ship^ ship = _draggedShip->Source;
	   _ships->Remove(ship);

	   Rect^ rect = ship->GetShipRegion();
	   RedrawRegion(rect);
       delete rect;

	   ship->Orientation = _draggedShip->Orientation;

	   AddShip(ship, cell->X, cell->Y);
	   delete _draggedShip;
   }
   else
   {
	   e->Effect = DragDropEffects::None;
   }
};

Ship^ Board::GetShipAt(int x, int y)
{
    for each(Ship^ ship in _ships)
    {
        if (ship->IsLocatedAt(x, y))
        {
            return ship;
        }
    }

    return nullptr;
};

bool Board::CanPlaceShip(Ship^ ship, int x, int y)
{
	Rect^ shipRegion = ship->GetShipRegion();

	shipRegion->MoveTo(x, y);

	if (!BoardRegion->Contains(shipRegion))
    {
        delete shipRegion;
        return false;
    }
	shipRegion->Inflate(1, 1);

	for each (Ship^ s in _ships)
	{
		if (dynamic_cast<DraggableShip^>(ship) != nullptr && s == (safe_cast<DraggableShip^>(ship))->Source)
		{
            delete shipRegion;
			continue;
		}

		if (s->GetShipRegion()->IntersectsWith(shipRegion))
		{
            delete shipRegion;
			return false;
		}
	}

    delete shipRegion;
	return true;
};

IList<Ship^>^ Board::GetNewShips()
{
	List<Ship^>^ ships = gcnew List<Ship^>;
    ships->Add(gcnew Ship(4, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(3, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(3, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(2, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(2, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(2, safe_cast<ShipOrientation>(_rnd->Next(2))));
    ships->Add(gcnew Ship(1, ShipOrientation::Horizontal));
    ships->Add(gcnew Ship(1, ShipOrientation::Horizontal));
    ships->Add(gcnew Ship(1, ShipOrientation::Horizontal));
    ships->Add(gcnew Ship(1, ShipOrientation::Horizontal));

	return ships;
};

void Board::ClearBoard()
{
	SuspendLayout();

	_ships->Clear();

	IList<Point>^ points = BoardRegion->GetPoints();
	for each (Point point in points)
	{
		_cells[point.X, point.Y]->State = BoardCellState::Normal;
	}

    delete points;

	ResumeLayout();
};

void Board::AddShip(Ship^ ship, int x, int y)
{
	ship->MoveTo(x, y);

	_ships->Add(ship);
	DrawShip(ship, BoardCellState::Ship);
};

void Board::ShowShips()
{
	for each (Ship^ ship in _ships)
	{
		IList<Point>^ shipPoints = ship->GetShipRegion()->GetPoints();

		for each (Point point in shipPoints)
		{
			BoardCell^ cell = _cells[point.X, point.Y];
			if (cell->State != BoardCellState::Normal)
				continue;

			cell->State = BoardCellState::Ship;
		}

        delete shipPoints;
	}
};

void Board::AddRandomShips()
{
	SuspendLayout();

	ClearBoard();

	IList<Ship^>^ ships = GetNewShips();

	for each (Ship^ ship in ships)
	{
		bool shipAdded = false;

		while (!shipAdded)
		{
			int x = _rnd->Next(10);
			int y = _rnd->Next(10);

			if (!CanPlaceShip(ship, x, y))
				continue;

			AddShip(ship, x, y);
			shipAdded = true;
		}
	}

	ResumeLayout();
};

ShotResult Board::OpenentShotAt(int x, int y)
{
	Ship^ ship = GetShipAt(x, y);

	if (ship == nullptr)
	{
		_cells[x, y]->State = BoardCellState::MissedShot;
		return ShotResult::Missed;
	}
	_cells[x, y]->State = BoardCellState::ShotShip;

	ship->HitCount++;

	if (ship->IsDrowned)
		DrawShip(ship, BoardCellState::ShowDrowned, true);

	return ship->IsDrowned ? ShotResult::ShipDrowned : ShotResult::ShipHit;
}

void Board::OnParentChanged(EventArgs^ e)
{
	Control::OnParentChanged(e);
	Font = Parent->Font;
}






















