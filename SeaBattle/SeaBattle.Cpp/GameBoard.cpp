#include "stdafx.h"
#include "GameBoard.h"
#include "PlayerBoard.h"



namespace SeaBattle {
    namespace Cpp{
    
        GameBoard::GameBoard(){
            
            this->SuspendLayout();

            this->_humanBoard = gcnew PlayerBoard();
            this->Controls->Add(this->_humanBoard);

            this->ResumeLayout();
        }

        void GameBoard::CreateWindowLayout(){
            
            this->AutoScaleDimensions = SizeF(8, 19);
            this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
            this->ClientSize = System::Drawing::Size(800, 500);
            this->Font = (gcnew System::Drawing::Font("Calibri", 10, FontStyle::Regular, GraphicsUnit::Point, 186));
            this->Margin = System::Windows::Forms::Padding(4, 4, 4, 4);
            this->Text = L"SeaBattle++";       
        }
    }
}