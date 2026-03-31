namespace Scientific_Calulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_round_right_bracket = new System.Windows.Forms.Button();
            this.btn_round_left_bracket = new System.Windows.Forms.Button();
            this.btnPi = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btn_point = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnTan = new System.Windows.Forms.Button();
            this.btnCos = new System.Windows.Forms.Button();
            this.btnSin = new System.Windows.Forms.Button();
            this.btnlog = new System.Windows.Forms.Button();
            this.btnPower2 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btnSqrt = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPowerXY = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDisplay
            // 
            this.txtDisplay.BackColor = System.Drawing.Color.White;
            this.txtDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay.Location = new System.Drawing.Point(16, 45);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(318, 86);
            this.txtDisplay.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.btnPowerXY);
            this.panel1.Controls.Add(this.btn_round_right_bracket);
            this.panel1.Controls.Add(this.btn_round_left_bracket);
            this.panel1.Controls.Add(this.btnPi);
            this.panel1.Controls.Add(this.btnEqual);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btn_point);
            this.panel1.Controls.Add(this.btnMultiply);
            this.panel1.Controls.Add(this.btnSubtract);
            this.panel1.Controls.Add(this.btnPlus);
            this.panel1.Controls.Add(this.btnDivide);
            this.panel1.Controls.Add(this.btnTan);
            this.panel1.Controls.Add(this.btnCos);
            this.panel1.Controls.Add(this.btnSin);
            this.panel1.Controls.Add(this.btnlog);
            this.panel1.Controls.Add(this.btnPower2);
            this.panel1.Controls.Add(this.btn9);
            this.panel1.Controls.Add(this.btn8);
            this.panel1.Controls.Add(this.btn7);
            this.panel1.Controls.Add(this.btn6);
            this.panel1.Controls.Add(this.btn5);
            this.panel1.Controls.Add(this.btn4);
            this.panel1.Controls.Add(this.btn3);
            this.panel1.Controls.Add(this.btn2);
            this.panel1.Controls.Add(this.btn1);
            this.panel1.Controls.Add(this.btnSqrt);
            this.panel1.Controls.Add(this.btn0);
            this.panel1.Controls.Add(this.txtDisplay);
            this.panel1.Location = new System.Drawing.Point(215, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 427);
            this.panel1.TabIndex = 1;
            // 
            // btn_round_right_bracket
            // 
            this.btn_round_right_bracket.AutoSize = true;
            this.btn_round_right_bracket.BackColor = System.Drawing.Color.DarkCyan;
            this.btn_round_right_bracket.FlatAppearance.BorderSize = 0;
            this.btn_round_right_bracket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_round_right_bracket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_round_right_bracket.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_round_right_bracket.ForeColor = System.Drawing.Color.White;
            this.btn_round_right_bracket.Location = new System.Drawing.Point(178, 160);
            this.btn_round_right_bracket.Name = "btn_round_right_bracket";
            this.btn_round_right_bracket.Size = new System.Drawing.Size(75, 31);
            this.btn_round_right_bracket.TabIndex = 32;
            this.btn_round_right_bracket.Text = ")";
            this.btn_round_right_bracket.UseVisualStyleBackColor = false;
            this.btn_round_right_bracket.Click += new System.EventHandler(this.btn_round_right_bracket_Click);
            // 
            // btn_round_left_bracket
            // 
            this.btn_round_left_bracket.AutoSize = true;
            this.btn_round_left_bracket.BackColor = System.Drawing.Color.DarkCyan;
            this.btn_round_left_bracket.FlatAppearance.BorderSize = 0;
            this.btn_round_left_bracket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_round_left_bracket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_round_left_bracket.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_round_left_bracket.ForeColor = System.Drawing.Color.White;
            this.btn_round_left_bracket.Location = new System.Drawing.Point(97, 160);
            this.btn_round_left_bracket.Name = "btn_round_left_bracket";
            this.btn_round_left_bracket.Size = new System.Drawing.Size(75, 31);
            this.btn_round_left_bracket.TabIndex = 29;
            this.btn_round_left_bracket.Text = "(";
            this.btn_round_left_bracket.UseVisualStyleBackColor = false;
            this.btn_round_left_bracket.Click += new System.EventHandler(this.btn_round_left_bracket_Click);
            // 
            // btnPi
            // 
            this.btnPi.AutoSize = true;
            this.btnPi.BackColor = System.Drawing.Color.DarkCyan;
            this.btnPi.FlatAppearance.BorderSize = 0;
            this.btnPi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPi.ForeColor = System.Drawing.Color.White;
            this.btnPi.Location = new System.Drawing.Point(259, 382);
            this.btnPi.Name = "btnPi";
            this.btnPi.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPi.Size = new System.Drawing.Size(75, 31);
            this.btnPi.TabIndex = 27;
            this.btnPi.Text = "pi";
            this.btnPi.UseVisualStyleBackColor = false;
            this.btnPi.Click += new System.EventHandler(this.btnPi_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.AutoSize = true;
            this.btnEqual.BackColor = System.Drawing.Color.DarkCyan;
            this.btnEqual.FlatAppearance.BorderSize = 0;
            this.btnEqual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnEqual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEqual.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEqual.ForeColor = System.Drawing.Color.White;
            this.btnEqual.Location = new System.Drawing.Point(178, 382);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(75, 31);
            this.btnEqual.TabIndex = 26;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = false;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.BackColor = System.Drawing.Color.Orange;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(259, 160);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 31);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.OrangeRed;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(16, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "D";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btn_point
            // 
            this.btn_point.AutoSize = true;
            this.btn_point.BackColor = System.Drawing.Color.DarkCyan;
            this.btn_point.FlatAppearance.BorderSize = 0;
            this.btn_point.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_point.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_point.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_point.ForeColor = System.Drawing.Color.White;
            this.btn_point.Location = new System.Drawing.Point(97, 382);
            this.btn_point.Name = "btn_point";
            this.btn_point.Size = new System.Drawing.Size(75, 31);
            this.btn_point.TabIndex = 24;
            this.btn_point.Text = ".";
            this.btn_point.UseVisualStyleBackColor = false;
            this.btn_point.Click += new System.EventHandler(this.btn_point_Click);
            // 
            // btnMultiply
            // 
            this.btnMultiply.AutoSize = true;
            this.btnMultiply.BackColor = System.Drawing.Color.DarkCyan;
            this.btnMultiply.FlatAppearance.BorderSize = 0;
            this.btnMultiply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnMultiply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultiply.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMultiply.ForeColor = System.Drawing.Color.White;
            this.btnMultiply.Location = new System.Drawing.Point(259, 271);
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size(75, 31);
            this.btnMultiply.TabIndex = 23;
            this.btnMultiply.Text = "*";
            this.btnMultiply.UseVisualStyleBackColor = false;
            this.btnMultiply.Click += new System.EventHandler(this.btnMultiply_Click);
            // 
            // btnSubtract
            // 
            this.btnSubtract.AutoSize = true;
            this.btnSubtract.BackColor = System.Drawing.Color.DarkCyan;
            this.btnSubtract.FlatAppearance.BorderSize = 0;
            this.btnSubtract.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnSubtract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubtract.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubtract.ForeColor = System.Drawing.Color.White;
            this.btnSubtract.Location = new System.Drawing.Point(259, 308);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(75, 31);
            this.btnSubtract.TabIndex = 22;
            this.btnSubtract.Text = "-";
            this.btnSubtract.UseVisualStyleBackColor = false;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.AutoSize = true;
            this.btnPlus.BackColor = System.Drawing.Color.DarkCyan;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.ForeColor = System.Drawing.Color.White;
            this.btnPlus.Location = new System.Drawing.Point(259, 345);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(75, 31);
            this.btnPlus.TabIndex = 21;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.AutoSize = true;
            this.btnDivide.BackColor = System.Drawing.Color.DarkCyan;
            this.btnDivide.FlatAppearance.BorderSize = 0;
            this.btnDivide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnDivide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDivide.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDivide.ForeColor = System.Drawing.Color.White;
            this.btnDivide.Location = new System.Drawing.Point(259, 234);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(75, 31);
            this.btnDivide.TabIndex = 20;
            this.btnDivide.Text = "/";
            this.btnDivide.UseVisualStyleBackColor = false;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnTan
            // 
            this.btnTan.AutoSize = true;
            this.btnTan.BackColor = System.Drawing.Color.DarkCyan;
            this.btnTan.FlatAppearance.BorderSize = 0;
            this.btnTan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnTan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTan.ForeColor = System.Drawing.Color.White;
            this.btnTan.Location = new System.Drawing.Point(178, 197);
            this.btnTan.Name = "btnTan";
            this.btnTan.Size = new System.Drawing.Size(75, 31);
            this.btnTan.TabIndex = 19;
            this.btnTan.Text = "tan";
            this.btnTan.UseVisualStyleBackColor = false;
            this.btnTan.Click += new System.EventHandler(this.btnTan_Click);
            // 
            // btnCos
            // 
            this.btnCos.AutoSize = true;
            this.btnCos.BackColor = System.Drawing.Color.DarkCyan;
            this.btnCos.FlatAppearance.BorderSize = 0;
            this.btnCos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnCos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCos.ForeColor = System.Drawing.Color.White;
            this.btnCos.Location = new System.Drawing.Point(97, 197);
            this.btnCos.Name = "btnCos";
            this.btnCos.Size = new System.Drawing.Size(75, 31);
            this.btnCos.TabIndex = 18;
            this.btnCos.Text = "cos";
            this.btnCos.UseVisualStyleBackColor = false;
            this.btnCos.Click += new System.EventHandler(this.btnCos_Click);
            // 
            // btnSin
            // 
            this.btnSin.AutoSize = true;
            this.btnSin.BackColor = System.Drawing.Color.DarkCyan;
            this.btnSin.FlatAppearance.BorderSize = 0;
            this.btnSin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnSin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSin.ForeColor = System.Drawing.Color.White;
            this.btnSin.Location = new System.Drawing.Point(16, 197);
            this.btnSin.Name = "btnSin";
            this.btnSin.Size = new System.Drawing.Size(75, 31);
            this.btnSin.TabIndex = 17;
            this.btnSin.Text = "sin";
            this.btnSin.UseVisualStyleBackColor = false;
            this.btnSin.Click += new System.EventHandler(this.btnSin_Click);
            // 
            // btnlog
            // 
            this.btnlog.AutoSize = true;
            this.btnlog.BackColor = System.Drawing.Color.DarkCyan;
            this.btnlog.FlatAppearance.BorderSize = 0;
            this.btnlog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlog.ForeColor = System.Drawing.Color.White;
            this.btnlog.Location = new System.Drawing.Point(178, 234);
            this.btnlog.Name = "btnlog";
            this.btnlog.Size = new System.Drawing.Size(75, 31);
            this.btnlog.TabIndex = 16;
            this.btnlog.Text = "log";
            this.btnlog.UseVisualStyleBackColor = false;
            this.btnlog.Click += new System.EventHandler(this.btnlog_Click);
            // 
            // btnPower2
            // 
            this.btnPower2.AutoSize = true;
            this.btnPower2.BackColor = System.Drawing.Color.DarkCyan;
            this.btnPower2.FlatAppearance.BorderSize = 0;
            this.btnPower2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPower2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPower2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPower2.ForeColor = System.Drawing.Color.White;
            this.btnPower2.Location = new System.Drawing.Point(97, 234);
            this.btnPower2.Name = "btnPower2";
            this.btnPower2.Size = new System.Drawing.Size(75, 31);
            this.btnPower2.TabIndex = 14;
            this.btnPower2.Text = "^2";
            this.btnPower2.UseVisualStyleBackColor = false;
            this.btnPower2.Click += new System.EventHandler(this.btnPower2_Click);
            // 
            // btn9
            // 
            this.btn9.AutoSize = true;
            this.btn9.BackColor = System.Drawing.Color.DarkCyan;
            this.btn9.FlatAppearance.BorderSize = 0;
            this.btn9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.ForeColor = System.Drawing.Color.White;
            this.btn9.Location = new System.Drawing.Point(178, 271);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(75, 31);
            this.btn9.TabIndex = 13;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btn8
            // 
            this.btn8.AutoSize = true;
            this.btn8.BackColor = System.Drawing.Color.DarkCyan;
            this.btn8.FlatAppearance.BorderSize = 0;
            this.btn8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.ForeColor = System.Drawing.Color.White;
            this.btn8.Location = new System.Drawing.Point(97, 271);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(75, 31);
            this.btn8.TabIndex = 12;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn7
            // 
            this.btn7.AutoSize = true;
            this.btn7.BackColor = System.Drawing.Color.DarkCyan;
            this.btn7.FlatAppearance.BorderSize = 0;
            this.btn7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.ForeColor = System.Drawing.Color.White;
            this.btn7.Location = new System.Drawing.Point(16, 271);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(75, 31);
            this.btn7.TabIndex = 11;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn6
            // 
            this.btn6.AutoSize = true;
            this.btn6.BackColor = System.Drawing.Color.DarkCyan;
            this.btn6.FlatAppearance.BorderSize = 0;
            this.btn6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.ForeColor = System.Drawing.Color.White;
            this.btn6.Location = new System.Drawing.Point(178, 308);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(75, 31);
            this.btn6.TabIndex = 10;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn5
            // 
            this.btn5.AutoSize = true;
            this.btn5.BackColor = System.Drawing.Color.DarkCyan;
            this.btn5.FlatAppearance.BorderSize = 0;
            this.btn5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.ForeColor = System.Drawing.Color.White;
            this.btn5.Location = new System.Drawing.Point(97, 308);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(75, 31);
            this.btn5.TabIndex = 9;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn4
            // 
            this.btn4.AutoSize = true;
            this.btn4.BackColor = System.Drawing.Color.DarkCyan;
            this.btn4.FlatAppearance.BorderSize = 0;
            this.btn4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.ForeColor = System.Drawing.Color.White;
            this.btn4.Location = new System.Drawing.Point(16, 308);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(75, 31);
            this.btn4.TabIndex = 8;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn3
            // 
            this.btn3.AutoSize = true;
            this.btn3.BackColor = System.Drawing.Color.DarkCyan;
            this.btn3.FlatAppearance.BorderSize = 0;
            this.btn3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.ForeColor = System.Drawing.Color.White;
            this.btn3.Location = new System.Drawing.Point(178, 345);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 31);
            this.btn3.TabIndex = 7;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn2
            // 
            this.btn2.AutoSize = true;
            this.btn2.BackColor = System.Drawing.Color.DarkCyan;
            this.btn2.FlatAppearance.BorderSize = 0;
            this.btn2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.ForeColor = System.Drawing.Color.White;
            this.btn2.Location = new System.Drawing.Point(97, 345);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 31);
            this.btn2.TabIndex = 6;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn1
            // 
            this.btn1.AutoSize = true;
            this.btn1.BackColor = System.Drawing.Color.DarkCyan;
            this.btn1.FlatAppearance.BorderSize = 0;
            this.btn1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.ForeColor = System.Drawing.Color.White;
            this.btn1.Location = new System.Drawing.Point(16, 345);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 31);
            this.btn1.TabIndex = 5;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btnSqrt
            // 
            this.btnSqrt.AutoSize = true;
            this.btnSqrt.BackColor = System.Drawing.Color.DarkCyan;
            this.btnSqrt.FlatAppearance.BorderSize = 0;
            this.btnSqrt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnSqrt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSqrt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSqrt.ForeColor = System.Drawing.Color.White;
            this.btnSqrt.Location = new System.Drawing.Point(259, 197);
            this.btnSqrt.Name = "btnSqrt";
            this.btnSqrt.Size = new System.Drawing.Size(75, 31);
            this.btnSqrt.TabIndex = 4;
            this.btnSqrt.Text = "sqrt";
            this.btnSqrt.UseVisualStyleBackColor = false;
            this.btnSqrt.Click += new System.EventHandler(this.btnSqrt_Click);
            // 
            // btn0
            // 
            this.btn0.AutoSize = true;
            this.btn0.BackColor = System.Drawing.Color.DarkCyan;
            this.btn0.FlatAppearance.BorderSize = 0;
            this.btn0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn0.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.ForeColor = System.Drawing.Color.White;
            this.btn0.Location = new System.Drawing.Point(16, 382);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(75, 31);
            this.btn0.TabIndex = 1;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = false;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(242, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scientific Calculator";
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.OrangeRed;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(12, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 31);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPowerXY
            // 
            this.btnPowerXY.AutoSize = true;
            this.btnPowerXY.BackColor = System.Drawing.Color.DarkCyan;
            this.btnPowerXY.FlatAppearance.BorderSize = 0;
            this.btnPowerXY.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPowerXY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPowerXY.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPowerXY.ForeColor = System.Drawing.Color.White;
            this.btnPowerXY.Location = new System.Drawing.Point(16, 234);
            this.btnPowerXY.Name = "btnPowerXY";
            this.btnPowerXY.Size = new System.Drawing.Size(75, 31);
            this.btnPowerXY.TabIndex = 33;
            this.btnPowerXY.Text = "x^y";
            this.btnPowerXY.UseVisualStyleBackColor = false;
          //  this.btnPowerXY.Click += new System.EventHandler(this.btnPowerXY_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 603);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSqrt;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn_point;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btnTan;
        private System.Windows.Forms.Button btnCos;
        private System.Windows.Forms.Button btnSin;
        private System.Windows.Forms.Button btnlog;
        private System.Windows.Forms.Button btnPower2;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnPi;
        private System.Windows.Forms.Button btn_round_right_bracket;
        private System.Windows.Forms.Button btn_round_left_bracket;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPowerXY;
    }
}

