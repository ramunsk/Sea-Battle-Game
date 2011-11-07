#include "stdafx.h"
#include "GameBoard.h"

using namespace SeaBattleCpp;
using namespace System;
using namespace System::Windows::Forms;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	Application::Run(gcnew GameBoard());
	return 0;
}
