#pragma once
#include "stdafx.h"

namespace SeaBattle {
    namespace Cpp {

        public ref class PlayerBoard : public TableLayoutPanel {

            private:
                literal int DefaultCellHeight = 30;
                literal int DefaultCellWidth = 30;
                initonly Color DefaultCellBackgroundColor;
                initonly Color DefaultCellBorderColor;

                void AddBoardHeaders();
                void AddCellStyles();

            protected:
                virtual void OnCellPaint(TableLayoutCellPaintEventArgs^ e) override;

            public:
                PlayerBoard();
        };
    }
}