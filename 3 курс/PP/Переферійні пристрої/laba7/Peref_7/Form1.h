#pragma once

bool t = false, ton = false, flag = false, on = false, fd1 = false, fd2 = false, fd3 = false;
char counter1 = 0, counter2 = 0;

namespace Peref_6 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
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
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::PictureBox^  Lpic;
	private: System::Windows::Forms::PictureBox^  clk;


	private: System::Windows::Forms::Panel^  obj;

	private: System::Windows::Forms::PictureBox^  lamp;

	private: System::Windows::Forms::RichTextBox^  obj_txt;
	private: System::Windows::Forms::RichTextBox^  ctrl_txt;
	private: System::Windows::Forms::PictureBox^  ra0;


	private: System::Windows::Forms::Timer^  timer;
	private: System::Windows::Forms::TextBox^  algor;

	private: System::Windows::Forms::RichTextBox^  i_am;
	private: System::Windows::Forms::CheckBox^  d3;
	private: System::Windows::Forms::CheckBox^  d2;
	private: System::Windows::Forms::CheckBox^  d1;
	private: System::Windows::Forms::CheckBox^  ud3;
	private: System::Windows::Forms::CheckBox^  ud2;
	private: System::Windows::Forms::CheckBox^  ud1;
	private: System::Windows::Forms::PictureBox^  ctrl;
	private: System::Windows::Forms::PictureBox^  ra1;
	private: System::Windows::Forms::PictureBox^  ra2;
	private: System::Windows::Forms::PictureBox^  rb0;
	private: System::Windows::Forms::PictureBox^  rb1;
	private: System::Windows::Forms::PictureBox^  rb2;
	private: System::Windows::Forms::PictureBox^  gates12;
	private: System::Windows::Forms::PictureBox^  gate0;
	private: System::Windows::Forms::PictureBox^  clk1up;
	private: System::Windows::Forms::PictureBox^  clk1line;
	private: System::Windows::Forms::PictureBox^  clk1down;
	private: System::Windows::Forms::PictureBox^  clk2down;
	private: System::Windows::Forms::PictureBox^  clk2line;
	private: System::Windows::Forms::PictureBox^  clk2up;
	private: System::Windows::Forms::PictureBox^  out0down;
	private: System::Windows::Forms::PictureBox^  out0line;
	private: System::Windows::Forms::PictureBox^  out0up;
	private: System::Windows::Forms::PictureBox^  out1down;
	private: System::Windows::Forms::PictureBox^  out1line;
	//private: System::Windows::Forms::PictureBox^  pictureBox1;
	private: System::Windows::Forms::PictureBox^  out1up;
	private: System::Windows::Forms::PictureBox^  out2down;
	private: System::Windows::Forms::PictureBox^  out2line;
	private: System::Windows::Forms::PictureBox^  out2up;
	private: System::Windows::Forms::Button^  stop;
	private: System::Windows::Forms::Button^  start;
	private: System::Windows::Forms::TextBox^  ct1;
	private: System::Windows::Forms::TextBox^  ct2;
	private: System::Windows::Forms::Timer^  timer100;






	private: System::ComponentModel::IContainer^  components;




	protected: 

	protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->components = (gcnew System::ComponentModel::Container());
			this->Lpic = (gcnew System::Windows::Forms::PictureBox());
			this->clk = (gcnew System::Windows::Forms::PictureBox());
			this->obj = (gcnew System::Windows::Forms::Panel());
			this->stop = (gcnew System::Windows::Forms::Button());
			this->start = (gcnew System::Windows::Forms::Button());
			this->d3 = (gcnew System::Windows::Forms::CheckBox());
			this->d2 = (gcnew System::Windows::Forms::CheckBox());
			this->d1 = (gcnew System::Windows::Forms::CheckBox());
			this->ud3 = (gcnew System::Windows::Forms::CheckBox());
			this->ud2 = (gcnew System::Windows::Forms::CheckBox());
			this->ud1 = (gcnew System::Windows::Forms::CheckBox());
			this->obj_txt = (gcnew System::Windows::Forms::RichTextBox());
			this->lamp = (gcnew System::Windows::Forms::PictureBox());
			this->ctrl_txt = (gcnew System::Windows::Forms::RichTextBox());
			this->ra0 = (gcnew System::Windows::Forms::PictureBox());
			this->timer = (gcnew System::Windows::Forms::Timer(this->components));
			this->algor = (gcnew System::Windows::Forms::TextBox());
			this->i_am = (gcnew System::Windows::Forms::RichTextBox());
			this->ctrl = (gcnew System::Windows::Forms::PictureBox());
			this->ra1 = (gcnew System::Windows::Forms::PictureBox());
			this->ra2 = (gcnew System::Windows::Forms::PictureBox());
			this->rb0 = (gcnew System::Windows::Forms::PictureBox());
			this->rb1 = (gcnew System::Windows::Forms::PictureBox());
			this->rb2 = (gcnew System::Windows::Forms::PictureBox());
			this->gates12 = (gcnew System::Windows::Forms::PictureBox());
			this->gate0 = (gcnew System::Windows::Forms::PictureBox());
			this->clk1up = (gcnew System::Windows::Forms::PictureBox());
			this->clk1line = (gcnew System::Windows::Forms::PictureBox());
			this->clk1down = (gcnew System::Windows::Forms::PictureBox());
			this->clk2down = (gcnew System::Windows::Forms::PictureBox());
			this->clk2line = (gcnew System::Windows::Forms::PictureBox());
			this->clk2up = (gcnew System::Windows::Forms::PictureBox());
			this->out0down = (gcnew System::Windows::Forms::PictureBox());
			this->out0line = (gcnew System::Windows::Forms::PictureBox());
			this->out0up = (gcnew System::Windows::Forms::PictureBox());
			this->out1down = (gcnew System::Windows::Forms::PictureBox());
			this->out1line = (gcnew System::Windows::Forms::PictureBox());
			this->out1up = (gcnew System::Windows::Forms::PictureBox());
			this->out2down = (gcnew System::Windows::Forms::PictureBox());
			this->out2line = (gcnew System::Windows::Forms::PictureBox());
			this->out2up = (gcnew System::Windows::Forms::PictureBox());
			this->ct1 = (gcnew System::Windows::Forms::TextBox());
			this->ct2 = (gcnew System::Windows::Forms::TextBox());
			this->timer100 = (gcnew System::Windows::Forms::Timer(this->components));
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->Lpic))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk))->BeginInit();
			this->obj->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->lamp))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra0))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ctrl))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra2))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb0))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb2))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->gates12))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->gate0))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1up))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1line))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1down))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2down))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2line))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2up))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0down))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0line))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0up))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1down))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1line))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1up))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2down))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2line))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2up))->BeginInit();
			this->SuspendLayout();
			// 
			// Lpic
			// 
			this->Lpic->ImageLocation = L"pics/Loff.png";
			this->Lpic->Location = System::Drawing::Point(10, 10);
			this->Lpic->Name = L"Lpic";
			this->Lpic->Size = System::Drawing::Size(402, 474);
			this->Lpic->TabIndex = 0;
			this->Lpic->TabStop = false;
			// 
			// clk
			// 
			this->clk->ImageLocation = L"pics/clk0.png";
			this->clk->Location = System::Drawing::Point(412, 438);
			this->clk->Name = L"clk";
			this->clk->Size = System::Drawing::Size(112, 15);
			this->clk->TabIndex = 1;
			this->clk->TabStop = false;
			// 
			// obj
			// 
			this->obj->BackColor = System::Drawing::SystemColors::Window;
			this->obj->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->obj->Controls->Add(this->stop);
			this->obj->Controls->Add(this->start);
			this->obj->Controls->Add(this->d3);
			this->obj->Controls->Add(this->d2);
			this->obj->Controls->Add(this->d1);
			this->obj->Controls->Add(this->ud3);
			this->obj->Controls->Add(this->ud2);
			this->obj->Controls->Add(this->ud1);
			this->obj->Controls->Add(this->obj_txt);
			this->obj->Controls->Add(this->lamp);
			this->obj->Location = System::Drawing::Point(524, 10);
			this->obj->Name = L"obj";
			this->obj->Size = System::Drawing::Size(146, 219);
			this->obj->TabIndex = 4;
			// 
			// stop
			// 
			this->stop->Location = System::Drawing::Point(84, 191);
			this->stop->Name = L"stop";
			this->stop->Size = System::Drawing::Size(57, 23);
			this->stop->TabIndex = 11;
			this->stop->Text = L"STOP";
			this->stop->UseVisualStyleBackColor = true;
			this->stop->Click += gcnew System::EventHandler(this, &Form1::stop_Click);
			// 
			// start
			// 
			this->start->Location = System::Drawing::Point(3, 191);
			this->start->Name = L"start";
			this->start->Size = System::Drawing::Size(57, 23);
			this->start->TabIndex = 10;
			this->start->Text = L"START";
			this->start->UseVisualStyleBackColor = true;
			this->start->Click += gcnew System::EventHandler(this, &Form1::start_Click);
			// 
			// d3
			// 
			this->d3->AutoSize = true;
			this->d3->Location = System::Drawing::Point(5, 100);
			this->d3->Name = L"d3";
			this->d3->Size = System::Drawing::Size(40, 17);
			this->d3->TabIndex = 9;
			this->d3->Text = L"D3";
			this->d3->UseVisualStyleBackColor = true;
			this->d3->CheckedChanged += gcnew System::EventHandler(this, &Form1::d3_CheckedChanged);
			// 
			// d2
			// 
			this->d2->AutoSize = true;
			this->d2->Location = System::Drawing::Point(5, 85);
			this->d2->Name = L"d2";
			this->d2->Size = System::Drawing::Size(40, 17);
			this->d2->TabIndex = 8;
			this->d2->Text = L"D2";
			this->d2->UseVisualStyleBackColor = true;
			this->d2->CheckedChanged += gcnew System::EventHandler(this, &Form1::d2_CheckedChanged);
			// 
			// d1
			// 
			this->d1->AutoSize = true;
			this->d1->Location = System::Drawing::Point(5, 70);
			this->d1->Name = L"d1";
			this->d1->Size = System::Drawing::Size(40, 17);
			this->d1->TabIndex = 1;
			this->d1->Text = L"D1";
			this->d1->UseVisualStyleBackColor = true;
			this->d1->CheckedChanged += gcnew System::EventHandler(this, &Form1::d1_CheckedChanged);
			// 
			// ud3
			// 
			this->ud3->AutoSize = true;
			this->ud3->Location = System::Drawing::Point(5, 55);
			this->ud3->Name = L"ud3";
			this->ud3->Size = System::Drawing::Size(48, 17);
			this->ud3->TabIndex = 6;
			this->ud3->Text = L"UD3";
			this->ud3->UseVisualStyleBackColor = true;
			this->ud3->CheckedChanged += gcnew System::EventHandler(this, &Form1::ud3_CheckedChanged);
			// 
			// ud2
			// 
			this->ud2->AutoSize = true;
			this->ud2->Location = System::Drawing::Point(5, 40);
			this->ud2->Name = L"ud2";
			this->ud2->Size = System::Drawing::Size(48, 17);
			this->ud2->TabIndex = 5;
			this->ud2->Text = L"UD2";
			this->ud2->UseVisualStyleBackColor = true;
			this->ud2->CheckedChanged += gcnew System::EventHandler(this, &Form1::ud2_CheckedChanged);
			// 
			// ud1
			// 
			this->ud1->AutoSize = true;
			this->ud1->Location = System::Drawing::Point(5, 25);
			this->ud1->Name = L"ud1";
			this->ud1->Size = System::Drawing::Size(48, 17);
			this->ud1->TabIndex = 4;
			this->ud1->Text = L"UD1";
			this->ud1->UseVisualStyleBackColor = true;
			this->ud1->CheckedChanged += gcnew System::EventHandler(this, &Form1::ud1_CheckedChanged);
			// 
			// obj_txt
			// 
			this->obj_txt->BorderStyle = System::Windows::Forms::BorderStyle::None;
			this->obj_txt->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->obj_txt->Location = System::Drawing::Point(49, 3);
			this->obj_txt->Name = L"obj_txt";
			this->obj_txt->Size = System::Drawing::Size(50, 25);
			this->obj_txt->TabIndex = 3;
			this->obj_txt->Text = L"OBJ";
			// 
			// lamp
			// 
			this->lamp->ImageLocation = L"pics/lamp_off.png";
			this->lamp->Location = System::Drawing::Point(57, 34);
			this->lamp->Name = L"lamp";
			this->lamp->Size = System::Drawing::Size(27, 27);
			this->lamp->TabIndex = 2;
			this->lamp->TabStop = false;
			// 
			// ctrl_txt
			// 
			this->ctrl_txt->BorderStyle = System::Windows::Forms::BorderStyle::None;
			this->ctrl_txt->Location = System::Drawing::Point(448, 1);
			this->ctrl_txt->Name = L"ctrl_txt";
			this->ctrl_txt->Size = System::Drawing::Size(38, 14);
			this->ctrl_txt->TabIndex = 5;
			this->ctrl_txt->Text = L"Control";
			// 
			// ra0
			// 
			this->ra0->ImageLocation = L"pics/r0.png";
			this->ra0->Location = System::Drawing::Point(412, 35);
			this->ra0->Name = L"ra0";
			this->ra0->Size = System::Drawing::Size(112, 15);
			this->ra0->TabIndex = 6;
			this->ra0->TabStop = false;
			// 
			// timer
			// 
			this->timer->Interval = 1000;
			this->timer->Tick += gcnew System::EventHandler(this, &Form1::timer_Tick);
			// 
			// algor
			// 
			this->algor->AcceptsReturn = true;
			this->algor->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->algor->Location = System::Drawing::Point(524, 232);
			this->algor->Multiline = true;
			this->algor->Name = L"algor";
			this->algor->ScrollBars = System::Windows::Forms::ScrollBars::Both;
			this->algor->Size = System::Drawing::Size(170, 236);
			this->algor->TabIndex = 8;
			this->algor->Text = L"Инициализация PPI (I8255)\r\nИнициализация PT (I8253)\r\nОжидание сигнала Ready\r\n";
			// 
			// i_am
			// 
			this->i_am->BorderStyle = System::Windows::Forms::BorderStyle::None;
			this->i_am->Location = System::Drawing::Point(546, 469);
			this->i_am->Name = L"i_am";
			this->i_am->Size = System::Drawing::Size(122, 19);
			this->i_am->TabIndex = 9;
			this->i_am->Text = L"Author: Anton Samarsky";
			// 
			// ctrl
			// 
			this->ctrl->ImageLocation = L"pics/c0.png";
			this->ctrl->Location = System::Drawing::Point(412, 12);
			this->ctrl->Name = L"ctrl";
			this->ctrl->Size = System::Drawing::Size(112, 15);
			this->ctrl->TabIndex = 10;
			this->ctrl->TabStop = false;
			// 
			// ra1
			// 
			this->ra1->ImageLocation = L"pics/r0.png";
			this->ra1->Location = System::Drawing::Point(412, 51);
			this->ra1->Name = L"ra1";
			this->ra1->Size = System::Drawing::Size(112, 15);
			this->ra1->TabIndex = 11;
			this->ra1->TabStop = false;
			// 
			// ra2
			// 
			this->ra2->ImageLocation = L"pics/r0.png";
			this->ra2->Location = System::Drawing::Point(412, 67);
			this->ra2->Name = L"ra2";
			this->ra2->Size = System::Drawing::Size(112, 15);
			this->ra2->TabIndex = 12;
			this->ra2->TabStop = false;
			// 
			// rb0
			// 
			this->rb0->ImageLocation = L"pics/r0.png";
			this->rb0->Location = System::Drawing::Point(412, 82);
			this->rb0->Name = L"rb0";
			this->rb0->Size = System::Drawing::Size(112, 15);
			this->rb0->TabIndex = 13;
			this->rb0->TabStop = false;
			// 
			// rb1
			// 
			this->rb1->ImageLocation = L"pics/r0.png";
			this->rb1->Location = System::Drawing::Point(412, 97);
			this->rb1->Name = L"rb1";
			this->rb1->Size = System::Drawing::Size(112, 15);
			this->rb1->TabIndex = 14;
			this->rb1->TabStop = false;
			// 
			// rb2
			// 
			this->rb2->ImageLocation = L"pics/r0.png";
			this->rb2->Location = System::Drawing::Point(412, 112);
			this->rb2->Name = L"rb2";
			this->rb2->Size = System::Drawing::Size(112, 15);
			this->rb2->TabIndex = 15;
			this->rb2->TabStop = false;
			// 
			// gates12
			// 
			this->gates12->ImageLocation = L"pics/gates.png";
			this->gates12->Location = System::Drawing::Point(412, 380);
			this->gates12->Name = L"gates12";
			this->gates12->Size = System::Drawing::Size(75, 41);
			this->gates12->TabIndex = 16;
			this->gates12->TabStop = false;
			// 
			// gate0
			// 
			this->gate0->ImageLocation = L"pics/gate0.png";
			this->gate0->Location = System::Drawing::Point(412, 220);
			this->gate0->Name = L"gate0";
			this->gate0->Size = System::Drawing::Size(11, 34);
			this->gate0->TabIndex = 17;
			this->gate0->TabStop = false;
			// 
			// clk1up
			// 
			this->clk1up->ImageLocation = L"pics/clk_1_up0.PNG";
			this->clk1up->Location = System::Drawing::Point(412, 204);
			this->clk1up->Name = L"clk1up";
			this->clk1up->Size = System::Drawing::Size(18, 1);
			this->clk1up->TabIndex = 18;
			this->clk1up->TabStop = false;
			// 
			// clk1line
			// 
			this->clk1line->ImageLocation = L"pics/clk_1_l0.PNG";
			this->clk1line->Location = System::Drawing::Point(429, 205);
			this->clk1line->Name = L"clk1line";
			this->clk1line->Size = System::Drawing::Size(1, 59);
			this->clk1line->TabIndex = 19;
			this->clk1line->TabStop = false;
			// 
			// clk1down
			// 
			this->clk1down->ImageLocation = L"pics/clk_1_d0.PNG";
			this->clk1down->Location = System::Drawing::Point(412, 264);
			this->clk1down->Name = L"clk1down";
			this->clk1down->Size = System::Drawing::Size(18, 7);
			this->clk1down->TabIndex = 20;
			this->clk1down->TabStop = false;
			// 
			// clk2down
			// 
			this->clk2down->ImageLocation = L"pics/clk_2_d0.PNG";
			this->clk2down->Location = System::Drawing::Point(412, 280);
			this->clk2down->Name = L"clk2down";
			this->clk2down->Size = System::Drawing::Size(28, 7);
			this->clk2down->TabIndex = 21;
			this->clk2down->TabStop = false;
			// 
			// clk2line
			// 
			this->clk2line->ImageLocation = L"pics/clk_2_l0.PNG";
			this->clk2line->Location = System::Drawing::Point(439, 187);
			this->clk2line->Name = L"clk2line";
			this->clk2line->Size = System::Drawing::Size(1, 93);
			this->clk2line->TabIndex = 22;
			this->clk2line->TabStop = false;
			// 
			// clk2up
			// 
			this->clk2up->ImageLocation = L"pics/clk_2_up0.PNG";
			this->clk2up->Location = System::Drawing::Point(412, 187);
			this->clk2up->Name = L"clk2up";
			this->clk2up->Size = System::Drawing::Size(27, 1);
			this->clk2up->TabIndex = 23;
			this->clk2up->TabStop = false;
			// 
			// out0down
			// 
			this->out0down->ImageLocation = L"pics/out_0_d0.PNG";
			this->out0down->Location = System::Drawing::Point(412, 306);
			this->out0down->Name = L"out0down";
			this->out0down->Size = System::Drawing::Size(36, 1);
			this->out0down->TabIndex = 24;
			this->out0down->TabStop = false;
			// 
			// out0line
			// 
			this->out0line->ImageLocation = L"pics/out_0_l0.PNG";
			this->out0line->Location = System::Drawing::Point(448, 173);
			this->out0line->Name = L"out0line";
			this->out0line->Size = System::Drawing::Size(1, 134);
			this->out0line->TabIndex = 25;
			this->out0line->TabStop = false;
			// 
			// out0up
			// 
			this->out0up->ImageLocation = L"pics/out_0_up0.PNG";
			this->out0up->Location = System::Drawing::Point(412, 170);
			this->out0up->Name = L"out0up";
			this->out0up->Size = System::Drawing::Size(36, 7);
			this->out0up->TabIndex = 26;
			this->out0up->TabStop = false;
			// 
			// out1down
			// 
			this->out1down->ImageLocation = L"pics/out_1_d0.PNG";
			this->out1down->Location = System::Drawing::Point(412, 320);
			this->out1down->Name = L"out1down";
			this->out1down->Size = System::Drawing::Size(44, 1);
			this->out1down->TabIndex = 27;
			this->out1down->TabStop = false;
			// 
			// out1line
			// 
			this->out1line->ImageLocation = L"pics/out_1_l0.PNG";
			this->out1line->Location = System::Drawing::Point(456, 156);
			this->out1line->Name = L"out1line";
			this->out1line->Size = System::Drawing::Size(1, 165);
			this->out1line->TabIndex = 28;
			this->out1line->TabStop = false;
			// 
			// out1up
			// 
			this->out1up->ImageLocation = L"pics/out_1_up0.PNG";
			this->out1up->Location = System::Drawing::Point(412, 153);
			this->out1up->Name = L"out1up";
			this->out1up->Size = System::Drawing::Size(44, 7);
			this->out1up->TabIndex = 30;
			this->out1up->TabStop = false;
			// 
			// out2down
			// 
			this->out2down->ImageLocation = L"pics/out_2_d0.PNG";
			this->out2down->Location = System::Drawing::Point(412, 334);
			this->out2down->Name = L"out2down";
			this->out2down->Size = System::Drawing::Size(53, 1);
			this->out2down->TabIndex = 32;
			this->out2down->TabStop = false;
			// 
			// out2line
			// 
			this->out2line->ImageLocation = L"pics/out_2_l0.PNG";
			this->out2line->Location = System::Drawing::Point(465, 139);
			this->out2line->Name = L"out2line";
			this->out2line->Size = System::Drawing::Size(1, 196);
			this->out2line->TabIndex = 33;
			this->out2line->TabStop = false;
			// 
			// out2up
			// 
			this->out2up->ImageLocation = L"pics/out_2_up0.PNG";
			this->out2up->Location = System::Drawing::Point(412, 136);
			this->out2up->Name = L"out2up";
			this->out2up->Size = System::Drawing::Size(53, 7);
			this->out2up->TabIndex = 34;
			this->out2up->TabStop = false;
			// 
			// ct1
			// 
			this->ct1->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->ct1->Location = System::Drawing::Point(326, 382);
			this->ct1->Name = L"ct1";
			this->ct1->Size = System::Drawing::Size(42, 20);
			this->ct1->TabIndex = 35;
			// 
			// ct2
			// 
			this->ct2->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->ct2->Location = System::Drawing::Point(326, 404);
			this->ct2->Name = L"ct2";
			this->ct2->Size = System::Drawing::Size(42, 20);
			this->ct2->TabIndex = 36;
			// 
			// timer100
			// 
			this->timer100->Enabled = true;
			this->timer100->Tick += gcnew System::EventHandler(this, &Form1::timer100_Tick);
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::SystemColors::Window;
			this->ClientSize = System::Drawing::Size(700, 500);
			this->Controls->Add(this->ct2);
			this->Controls->Add(this->ct1);
			this->Controls->Add(this->out2up);
			this->Controls->Add(this->out2line);
			this->Controls->Add(this->out2down);
			this->Controls->Add(this->out1up);
			this->Controls->Add(this->out1line);
			this->Controls->Add(this->out1down);
			this->Controls->Add(this->out0up);
			this->Controls->Add(this->out0line);
			this->Controls->Add(this->out0down);
			this->Controls->Add(this->clk2up);
			this->Controls->Add(this->clk2line);
			this->Controls->Add(this->clk2down);
			this->Controls->Add(this->clk1down);
			this->Controls->Add(this->clk1line);
			this->Controls->Add(this->clk1up);
			this->Controls->Add(this->gate0);
			this->Controls->Add(this->gates12);
			this->Controls->Add(this->rb2);
			this->Controls->Add(this->rb1);
			this->Controls->Add(this->rb0);
			this->Controls->Add(this->ra2);
			this->Controls->Add(this->ra1);
			this->Controls->Add(this->ctrl);
			this->Controls->Add(this->i_am);
			this->Controls->Add(this->algor);
			this->Controls->Add(this->ra0);
			this->Controls->Add(this->ctrl_txt);
			this->Controls->Add(this->obj);
			this->Controls->Add(this->clk);
			this->Controls->Add(this->Lpic);
			this->Name = L"Form1";
			this->Text = L"Lab N7";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->Lpic))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk))->EndInit();
			this->obj->ResumeLayout(false);
			this->obj->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->lamp))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra0))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ctrl))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->ra2))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb0))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->rb2))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->gates12))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->gate0))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1up))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1line))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk1down))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2down))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2line))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->clk2up))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0down))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0line))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out0up))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1down))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1line))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out1up))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2down))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2line))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->out2up))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
/*
private: System::Void start_Click(System::Object^  sender, System::EventArgs^  e) {
//			this->r_pic->ImageLocation = L"pics/r_init2.png";
			this->clk->ImageLocation = L"pics/clk1.png";
			//this->ready->ImageLocation = L"pics/ready_on.png";
			this->Lpic->ImageLocation = L"pics/Lon.png";
			this->ctrl->ImageLocation = L"pics/c1.png";
			this->lamp->ImageLocation = L"pics/lamp_on.png";
			flag = true;
			this->timer->Enabled = false;
			this->timer->Enabled = true;
			this->algor->AppendText("Запись Gate0\r\n");
			this->algor->AppendText("Запись Gate1\r\n");
		 }
private: System::Void stop_Click(System::Object^  sender, System::EventArgs^  e) {
			//this->ready->ImageLocation = L"pics/ready_off.png";
			flag = false;
			this->algor->AppendText("Запись 0 в Gate0\r\n");
			this->algor->AppendText("Запись 0 в Gate1\r\n");
		 }*/
private: System::Void timer_Tick(System::Object^  sender, System::EventArgs^  e) {
	String^ s;
	if (flag){
		if (!on){
			this->out0down->ImageLocation = L"pics/out_0_d1.PNG";
			this->out0line->ImageLocation = L"pics/out_0_l1.PNG";
			this->out0up->ImageLocation = L"pics/out_0_up1.PNG";
			this->clk1down->ImageLocation = L"pics/clk_1_d0.PNG";
			this->clk1line->ImageLocation = L"pics/clk_1_l0.PNG";
			this->clk1up->ImageLocation = L"pics/clk_1_up0.PNG";
			this->clk2down->ImageLocation = L"pics/clk_2_d0.PNG";
			this->clk2line->ImageLocation = L"pics/clk_2_l0.PNG";
			this->clk2up->ImageLocation = L"pics/clk_2_up0.PNG";
			if (fd1 || (fd2 && fd3)){
				this->algor->AppendText("Проверка наличия сигнала от D1\r\n");
				this->algor->AppendText("Проверка наличия сигнала от D2 и D3\r\n");
				if ((fd1 && fd2) || (fd2 && fd3) || (fd1 && fd3)){
					this->algor->AppendText("Выделения кода UD1, UD2, UD3\r\n");
					ton = true;
					this->ctrl->ImageLocation = L"pics/c1.png";
					this->lamp->ImageLocation = L"pics/lamp_on.png";
					this->algor->AppendText("Включение линии Control\r\n");
					this->algor->AppendText("Перезапуск счетчика 0\r\n");
					this->algor->AppendText("Ожидание out счетчика 0\r\n");
					on = true;
					t = true;
					this->out0down->ImageLocation = L"pics/out_0_d0.PNG";
					this->out0line->ImageLocation = L"pics/out_0_l0.PNG";
					this->out0up->ImageLocation = L"pics/out_0_up0.PNG";
				} //really works!
				else {
					this->algor->AppendText("Выделения кода UD1, UD2, UD3\r\n");
					ton = false;
					counter2--;
					this->algor->AppendText("Декрементация счетчика 1\r\n");
					s = gcnew String("");
					s = s->Concat(counter2);
					ct2->Clear();
					ct2->AppendText(s);
					delete s;
					this->algor->AppendText("Проверка сигнала out счетчика 2\r\n");
					this->clk2down->ImageLocation = L"pics/clk_2_d1.PNG";
					this->clk2line->ImageLocation = L"pics/clk_2_l1.PNG";
					this->clk2up->ImageLocation = L"pics/clk_2_up1.PNG";
					if (!counter2){
						//end
						this->algor->AppendText("Счетчик 2 = 0, переход на конец\r\nEnd\r\n");
						flag = false;
						this->out2down->ImageLocation = L"pics/out_2_d1.PNG";
						this->out2line->ImageLocation = L"pics/out_2_l1.PNG";
						this->out2up->ImageLocation = L"pics/out_2_up1.PNG";
					} //if counter2 == 0
				} //else counter2
			 } //if work
			 else {
				ton = false;
				counter1--;
				s = gcnew String("");
				s = s->Concat(counter1);
				ct1->Clear();
				ct1->AppendText(s);
				delete s;
				this->algor->AppendText("Декрементация счетчика 1\r\n");
				this->clk1down->ImageLocation = L"pics/clk_1_d1.PNG";
				this->clk1line->ImageLocation = L"pics/clk_1_l1.PNG";
				this->clk1up->ImageLocation = L"pics/clk_1_up1.PNG";
				this->algor->AppendText("Проверка сигнала out счетчика 1\r\n");
				if (!counter1){
					//end
					this->algor->AppendText("Счетчик 1 = 0, переход на конец\r\nEnd\r\n");
					flag = false;
					this->out1down->ImageLocation = L"pics/out_1_d1.PNG";
					this->out1line->ImageLocation = L"pics/out_1_l1.PNG";
					this->out1up->ImageLocation = L"pics/out_1_up1.PNG";
				} //if counter1 == 0
			 } //else (not work)
		} //if !on
		else {
			this->ctrl->ImageLocation = L"pics/c0.png";
			this->lamp->ImageLocation = L"pics/lamp_off.png";
			on = false;
			t = true;
			this->algor->AppendText("Выключение линии Control\r\n");
			this->algor->AppendText("Перезапуск счетчика 0\r\n");
			this->algor->AppendText("Ожидание out счетчика 0\r\n");
		} //else !on
	} //if flag
		else {
			this->clk1down->ImageLocation = L"pics/clk_1_d0.PNG";
			this->clk1line->ImageLocation = L"pics/clk_1_l0.PNG";
			this->clk1up->ImageLocation = L"pics/clk_1_up0.PNG";
			this->clk2down->ImageLocation = L"pics/clk_2_d0.PNG";
			this->clk2line->ImageLocation = L"pics/clk_2_l0.PNG";
			this->clk2up->ImageLocation = L"pics/clk_2_up0.PNG";
			this->Lpic->ImageLocation = L"pics/Loff.png";
			this->clk->ImageLocation = L"pics/clk0.png";
			this->ctrl->ImageLocation = L"pics/c0.png";
			this->lamp->ImageLocation = L"pics/lamp_off.png";
			this->out0down->ImageLocation = L"pics/out_0_d0.PNG";
			this->out0line->ImageLocation = L"pics/out_0_l0.PNG";
			this->out0up->ImageLocation = L"pics/out_0_up0.PNG";
			this->gate0->ImageLocation = L"pics/gate0.png";
			t = false;
		} //else flag
	/*if(flag){
		if (on){
			this->algor->AppendText("Проверка сигнала Ready\r\n");
			this->Lpic->ImageLocation = L"pics/Lon.png";
			this->ctrl->ImageLocation = L"pics/control_on.png";
			this->lamp->ImageLocation = L"pics/lamp_on.png";
			this->r_pic->ImageLocation = L"pics/r_out2.png";
			on = false;
			//this->algor->AppendText("Включение линии Control\r\n");
			this->algor->AppendText("Перезапуск счетчика 0\r\n");
			this->algor->AppendText("Ожидание Out0\r\n");
		} //if on
		else {
			this->Lpic->ImageLocation = L"pics/Lon.png";
			this->ctrl->ImageLocation = L"pics/control_off.png";
			this->lamp->ImageLocation = L"pics/lamp_off.png";
			this->r_pic->ImageLocation = L"pics/r_out1.png";
			on = true;
			//this->algor->AppendText("Выключение линии Control\r\n");
			this->algor->AppendText("Перезапуск счетчика 1\r\n");
			this->algor->AppendText("Ожидание Out1\r\n");
		} //else on
	} //if flag
	else {
		//this->algor->AppendText("Проверка сигнала Ready\r\n");
		this->clk->ImageLocation = L"pics/clk0.png";
		this->Lpic->ImageLocation = L"pics/Loff.png";
		this->ctrl->ImageLocation = L"pics/control_off.png";
		this->lamp->ImageLocation = L"pics/lamp_off.png";
		this->r_pic->ImageLocation = L"pics/r_init0.png";
		on = false;
	} //else flag*/
		 }
private: System::Void ud1_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 //fd1 = ! fd1;
			 this->d1->Checked = this->ud1->Checked;
			 if (fd1){
				 this->ra0->ImageLocation = L"pics/r1.png";
				 this->rb0->ImageLocation = L"pics/r1.png";
			 } //if
			 else {
				 this->ra0->ImageLocation = L"pics/r0.png";
				 this->rb0->ImageLocation = L"pics/r0.png";
			 } //else
		 }
private: System::Void ud2_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 //fd2 = ! fd2;
			 this->d2->Checked = this->ud2->Checked;
			 if (fd2){
				 this->ra1->ImageLocation = L"pics/r1.png";
				 this->rb1->ImageLocation = L"pics/r1.png";
			 } //if
			 else {
				 this->ra1->ImageLocation = L"pics/r0.png";
				 this->rb1->ImageLocation = L"pics/r0.png";
			 } //else
		 }
private: System::Void ud3_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 //fd3 = ! fd3;
			 this->d3->Checked = this->ud3->Checked;
			 if (fd3){
				 this->ra2->ImageLocation = L"pics/r1.png";
				 this->rb2->ImageLocation = L"pics/r1.png";
			 } //if
			 else {
				 this->ra2->ImageLocation = L"pics/r0.png";
				 this->rb2->ImageLocation = L"pics/r0.png";
			 } //else
		 }
private: System::Void d1_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 fd1 = ! fd1;
			 this->ud1->Checked = this->d1->Checked;
		 }
private: System::Void d2_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 fd2 = ! fd2;
			 this->ud2->Checked = this->d2->Checked;
		 }
private: System::Void d3_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 fd3 = ! fd3;
			 this->ud3->Checked = this->d3->Checked;
		 }
private: System::Void start_Click(System::Object^  sender, System::EventArgs^  e) {
			flag = true;
			counter1 = counter2 = 10;
			this->algor->AppendText("Запись Gate0\r\n");
			this->timer->Enabled = false;
			this->timer->Enabled = true;
			this->gate0->ImageLocation = L"pics/gate1.png";
			this->Lpic->ImageLocation = L"pics/Lon.png";
			this->clk->ImageLocation = L"pics/clk1.png";
			this->out1down->ImageLocation = L"pics/out_1_d0.PNG";
			this->out1line->ImageLocation = L"pics/out_1_l0.PNG";
			this->out1up->ImageLocation = L"pics/out_1_up0.PNG";
			this->out2down->ImageLocation = L"pics/out_2_d0.PNG";
			this->out2line->ImageLocation = L"pics/out_2_l0.PNG";
			this->out2up->ImageLocation = L"pics/out_2_up0.PNG";
			this->ct1->Clear();
			this->ct2->Clear();
			this->ct1->AppendText("10");
			this->ct2->AppendText("10");
			t = false;
			on = false;
			ton = false;
		 }
private: System::Void stop_Click(System::Object^  sender, System::EventArgs^  e) {
			flag = false;
			ton = false;
			this->algor->AppendText("End\r\n");
			this->out1down->ImageLocation = L"pics/out_1_d0.PNG";
			this->out1line->ImageLocation = L"pics/out_1_l0.PNG";
			this->out1up->ImageLocation = L"pics/out_1_up0.PNG";
			this->out2down->ImageLocation = L"pics/out_2_d0.PNG";
			this->out2line->ImageLocation = L"pics/out_2_l0.PNG";
			this->out2up->ImageLocation = L"pics/out_2_up0.PNG";
			//this->gate0->ImageLocation = L"pics/gate0.png";
		 }
private: System::Void timer100_Tick(System::Object^  sender, System::EventArgs^  e) {
	if (ton){
			 if (t){
				t = false;
				this->out0down->ImageLocation = L"pics/out_0_d1.PNG";
				this->out0line->ImageLocation = L"pics/out_0_l1.PNG";
				this->out0up->ImageLocation = L"pics/out_0_up1.PNG";
			 } //if t
			 else {
				this->out0down->ImageLocation = L"pics/out_0_d0.PNG";
				this->out0line->ImageLocation = L"pics/out_0_l0.PNG";
				this->out0up->ImageLocation = L"pics/out_0_up0.PNG";
			 }
	} //ton
	else {
		if (flag){
		this->out0down->ImageLocation = L"pics/out_0_d1.PNG";
		this->out0line->ImageLocation = L"pics/out_0_l1.PNG";
		this->out0up->ImageLocation = L"pics/out_0_up1.PNG";
		} //flag
	}
		 }
};
}

