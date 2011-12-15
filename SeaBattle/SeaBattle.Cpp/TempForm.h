#pragma once

namespace SeaBattleCpp {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for TempForm
	/// </summary>
	public ref class TempForm : public System::Windows::Forms::Form
	{
	public:
		TempForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~TempForm()
		{
			if (components)
			{
				delete components;
			}
		}
    private: System::Windows::Forms::Label^  label1;
    protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
            this->label1 = (gcnew System::Windows::Forms::Label());
            this->SuspendLayout();
            // 
            // label1
            // 
            this->label1->Dock = System::Windows::Forms::DockStyle::Fill;
            this->label1->Location = System::Drawing::Point(0, 0);
            this->label1->Name = L"label1";
            this->label1->Size = System::Drawing::Size(284, 262);
            this->label1->TabIndex = 0;
            this->label1->Text = L"label1";
            this->label1->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
            this->label1->Click += gcnew System::EventHandler(this, &TempForm::label1_Click);
            // 
            // TempForm
            // 
            this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
            this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
            this->ClientSize = System::Drawing::Size(284, 262);
            this->Controls->Add(this->label1);
            this->Name = L"TempForm";
            this->Text = L"TempForm";
            this->ResumeLayout(false);

        }
#pragma endregion
    private: System::Void label1_Click(System::Object^  sender, System::EventArgs^  e) {
             }
    };
}
