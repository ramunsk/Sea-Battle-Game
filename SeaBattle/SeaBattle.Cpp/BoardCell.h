#pragma once
#include "stdafx.h"
#include "Enums.h"

ref class BoardCell : Label
{
    private:
        static initonly Color DefaultBorderColor = Color::FromArgb(214,214,214);
        static initonly Color DefaultBackgroundColor = Color::FromArgb(222,222,222);
        static initonly Color DragOverBackgroundColor = Color::FromArgb(255,174,0);
        static initonly Color DragOverInvalidBackgroundColor = Color::FromArgb(222,0,0);
        static initonly Color ShipColor = Color::FromArgb(65,133,243);
        static initonly Color ShipDrownedColor = Color::FromArgb(222, 0, 0);

		static initonly char^ ShipHitChar = (char)(0x72);
		static initonly char^ MissedHitChar = (char)(0x3D);

    private:
		void OnCellStateChenged();
        BoardCellState _state;

    protected:
		virtual void OnPaint(PaintEventArgs ^e) override;

	public:
		BoardCell(int x, int y);
   		property int X;
		property int Y;

		property BoardCellState State
		{
			BoardCellState get();
			void set(BoardCellState value);
		};
};
