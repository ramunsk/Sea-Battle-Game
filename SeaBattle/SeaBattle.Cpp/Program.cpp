#include "stdafx.h"
#include "MainForm.h"


using namespace System;




[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	Application::Run(gcnew MainForm());
	return 0;
};

