#pragma once

namespace WF = System::Windows::Forms;
namespace SD = System::Drawing;

namespace SeaBattleCpp {
	public ref class GameBoard : public WF::Form
	{
	    public:
		    GameBoard();

        private:
            literal int CELL_HEIGHT = 25;
            literal int CELL_WIDTH = 25;
            initonly SD::Color CELL_BORDER_COLOR;
            initonly SD::Color CELL_BG_COLOR;

            WF::TableLayoutPanel^ _tlpHuman;


            void CreateLayout();
            void CreateHumanBoard();
            void OnBoardCellPaint(System::Object^ sender, WF::TableLayoutCellPaintEventArgs^ e);

    };
}

