#pragma once
#include "PlayerBoard.h"

namespace SeaBattle {
    namespace Cpp {
	    public ref class GameBoard : public Form
	    {
            private:
                initonly PlayerBoard^ _humanBoard;

                void CreateWindowLayout();

            public:
		        GameBoard();

        };
    }
}

