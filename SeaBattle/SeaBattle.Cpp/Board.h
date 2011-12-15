#pragma once
#include "stdafx.h"
#include "Rect.h"
#include "Ship.h"
#include "BoardCell.h"
#include "DraggableShip.h"
#include "BoardCellClickEventArgs.h"

ref class Board : Control
{
    private:
	    static initonly int CellSize = 25;
        static initonly Rect^ BoardRegion = gcnew Rect(0, 0, 10, 10);

		initonly array<BoardCell^, 2>^ _cells;
		initonly List<Ship^>^ _ships;
		DraggableShip^ _draggedShip;
		initonly Random^ _rnd;
		initonly bool _drawShips;
        
        static Label^ CreateHeaderCell(int x, int y, String^ text);
        void CreateHeaders();
        void CreateBoard();
        void RedrawRegion(Rect^ region);
        void DrawShip(Ship^ ship, BoardCellState state);
        void DrawShip(Ship^ ship, BoardCellState state, bool force);

        void OnCellClick(Object^ sender, EventArgs^ e);
		void OnCellMouseDown(Object^ sender, MouseEventArgs^ e);
		void OnCellQueryContinueDrag(Object^ sender, QueryContinueDragEventArgs^ e);
		void OnCellDragEnter(Object^ sender, DragEventArgs^ e);
		void OnCellDragLeave(Object^ sender, EventArgs^ e);
        void OnCellDragDrop(Object^ sender, DragEventArgs^ e);

        Ship^ GetShipAt(int x, int y);
        bool CanPlaceShip(Ship^ ship, int x, int y);
        IList<Ship^>^ GetNewShips();

	public:
		Board();
		Board(bool drawShips);
        property BoardMode Mode;
        void ClearBoard();
		void AddShip(Ship^ ship, int x, int y);
        void ShowShips();
        void AddRandomShips();
        ShotResult OpenentShotAt(int x, int y);

        event EventHandler<BoardCellClickEventArgs^>^ OnClick;
		
	protected:
		virtual void OnParentChanged(EventArgs^ e) override;
};
