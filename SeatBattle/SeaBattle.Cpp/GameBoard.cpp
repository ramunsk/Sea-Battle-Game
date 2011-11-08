#include "stdafx.h"
#include "GameBoard.h"

namespace SeaBattleCpp {
    GameBoard::GameBoard(){
        this->CELL_BORDER_COLOR = SD::Color::CornflowerBlue;
        this->CELL_BG_COLOR = SD::Color::LightBlue;


        this->CreateLayout();

    }

    void GameBoard::CreateLayout(){
       
        this->_tlpHuman = (gcnew WF::TableLayoutPanel());


        this->SuspendLayout();
        this->CreateWindowLayout();
        this->CreateHumanBoard();
        this->ResumeLayout();
    
    }

    void GameBoard::CreateHumanBoard(){
        
        WF::TableLayoutPanel^ pnl = this->_tlpHuman;

        pnl->ColumnCount = 11;
        pnl->RowCount = 11;

        for(int i = 0; i < 11; i++)
        {
            pnl->ColumnStyles->Add(gcnew WF::ColumnStyle(WF::SizeType::Absolute, CELL_WIDTH));
            pnl->RowStyles->Add(gcnew WF::RowStyle(WF::SizeType::Absolute, CELL_HEIGHT));
        }

        pnl->CellPaint += gcnew WF::TableLayoutCellPaintEventHandler(this, &GameBoard::OnBoardCellPaint);
        pnl->Width = CELL_WIDTH * 11;
        pnl->Height = CELL_HEIGHT * 11;


        this->AddGridLabels(pnl);

        this->Controls->Add(this->_tlpHuman);
        
    }

    void GameBoard::CreateWindowLayout(){
        
        this->AutoScaleDimensions = SD::SizeF(8, 19);
        this->AutoScaleMode = WF::AutoScaleMode::Font;
        this->ClientSize = SD::Size(800, 500);
        this->Font = gcnew SD::Font("Calibri", 10, SD::FontStyle::Regular, SD::GraphicsUnit::Point, 186);
        this->Margin = WF::Padding(4, 4, 4, 4);
        //this.Name = "GameBoard";
        //this.Text = "GameBoard";

    }

    void GameBoard::OnBoardCellPaint(System::Object^ sender, WF::TableLayoutCellPaintEventArgs^ e){
    
        if (e->Row == 0 || e->Column == 0)
            return;

        SD::Rectangle rect = e->CellBounds;
        WF::TableLayoutPanel^ pnl = (WF::TableLayoutPanel^)sender;

        SD::Pen^ pen = gcnew SD::Pen(this->CELL_BORDER_COLOR, 0);
        SD::SolidBrush^ brush = gcnew SD::SolidBrush(this->CELL_BG_COLOR);


        pen->Alignment = SD::Drawing2D::PenAlignment::Center;
        pen->DashStyle = SD::Drawing2D::DashStyle::Solid;

        if (e->Row == (pnl->RowCount - 1))
            rect.Height -= 1;

        if (e->Column == (pnl->ColumnCount - 1))
            rect.Width -= 1;

        e->Graphics->FillRectangle(brush, rect);
        e->Graphics->DrawRectangle(pen, rect);
        

        delete pen;
    }

    void GameBoard::AddGridLabels(WF::TableLayoutPanel^ panel){
    
        for(int i = 1; i < 11; i++)
        {
            WF::Label^ lblLeft = (gcnew WF::Label());
            lblLeft->Dock = System::Windows::Forms::DockStyle::Fill;
            lblLeft->TextAlign = SD::ContentAlignment::MiddleCenter;
            lblLeft->Text = i.ToString();
            panel->Controls->Add(lblLeft, 0, i);

            WF::Label^ lblTop = (gcnew WF::Label());
            lblTop->Dock = System::Windows::Forms::DockStyle::Fill;
            lblTop->TextAlign = SD::ContentAlignment::MiddleCenter;
            lblTop->Text = System::String::Format(L"{0}", ((System::Char)(64+i)));
            panel->Controls->Add(lblTop, i, 0);
        }
    }
}