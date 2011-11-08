#include "stdafx.h"
#include "PlayerBoard.h"

namespace SeaBattle {
    namespace Cpp {

        PlayerBoard::PlayerBoard(){
            
            this->DefaultCellBackgroundColor = Color::LightBlue;
            this->DefaultCellBorderColor = Color::CornflowerBlue;

            this->ColumnCount = 11;
            this->RowCount = 11;

            this->AddCellStyles();
            this->AddBoardHeaders();

            this->Width = DefaultCellWidth * 11;
            this->Height = DefaultCellHeight * 11;
        }

        void PlayerBoard::AddBoardHeaders(){
            
            for (int i = 1; i < 11; i++)
            {
                Label^ lblLeft = gcnew Label();
                lblLeft->Dock = DockStyle::Fill;
                lblLeft->TextAlign = ContentAlignment::MiddleCenter;
                lblLeft->Text = i.ToString();
                this->Controls->Add(lblLeft, 0, i);

                Label^ lblTop = gcnew Label();
                lblTop->Dock = DockStyle::Fill,
                lblTop->TextAlign = ContentAlignment::MiddleCenter,
                lblTop->Text =  ((System::Char)(64 + i)).ToString();
                Controls->Add(lblTop, i, 0);
            }
        }

        void PlayerBoard::AddCellStyles(){
            for (int i = 0; i < 11; i++)
            {
                this->ColumnStyles->Add(gcnew ColumnStyle(SizeType::Absolute, this->DefaultCellWidth));
                this->RowStyles->Add(gcnew RowStyle(SizeType::Absolute, this->DefaultCellHeight));
            }
        }

        void PlayerBoard::OnCellPaint(TableLayoutCellPaintEventArgs^ e){
            
            TableLayoutPanel::OnCellPaint(e);

            if (e->Row == 0 || e->Column == 0)
                return;

            Rectangle rect = e->CellBounds;

            Pen^ pen = gcnew Pen(this->DefaultCellBorderColor, 0);
            pen->Alignment = Drawing2D::PenAlignment::Center;
            pen->DashStyle = System::Drawing::Drawing2D::DashStyle::Solid;

            Brush^ brush = gcnew SolidBrush(this->DefaultCellBackgroundColor);

            if (e->Row == (this->RowCount - 1))
                rect.Height -= 1;

            if (e->Column == (this->ColumnCount - 1))
                rect.Width -= 1;

            e->Graphics->FillRectangle(brush, rect);
            e->Graphics->DrawRectangle(pen, rect);

            delete brush;
            delete pen;

        }
    };
}