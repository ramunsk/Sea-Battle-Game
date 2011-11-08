#include "stdafx.h"
#include "GameBoard.h"
#include "PlayerBoard.h"

namespace SD = System::Drawing;
namespace WF = System::Windows::Forms;

namespace SeaBattle {
    namespace Cpp{
    
        GameBoard::GameBoard(){
            
            this->SuspendLayout();

            this->CreateWindowLayout();

            this->_humanBoard = gcnew PlayerBoard();
            this->Controls->Add(this->_humanBoard);

            this->ResumeLayout();
        }

        void GameBoard::CreateWindowLayout(){
            
            this->AutoScaleDimensions = SizeF(8, 19);
            this->AutoScaleMode = WF::AutoScaleMode::Font;
            this->ClientSize = System::Drawing::Size(800, 500);
            this->Font = (gcnew SD::Font("Calibri", 10, FontStyle::Regular, GraphicsUnit::Point, 186));
            this->Margin = WF::Padding(4, 4, 4, 4);
            this->Text = "SeaBattle++";       
        }
    }
}