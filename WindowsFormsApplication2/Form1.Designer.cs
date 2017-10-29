using System;
using System.Collections;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
namespace RS232_monitor
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_terminal1 = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.comboBox_portspeed1 = new System.Windows.Forms.ComboBox();
            this.comboBox_handshake1 = new System.Windows.Forms.ComboBox();
            this.comboBox_databits1 = new System.Windows.Forms.ComboBox();
            this.comboBox_parity1 = new System.Windows.Forms.ComboBox();
            this.comboBox_stopbits1 = new System.Windows.Forms.ComboBox();
            this.textBox_params = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button_openport = new System.Windows.Forms.Button();
            this.button_closeport = new System.Windows.Forms.Button();
            this.comboBox_portname1 = new System.Windows.Forms.ComboBox();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.label_custom_command = new System.Windows.Forms.Label();
            this.comboBox_portname2 = new System.Windows.Forms.ComboBox();
            this.comboBox_portspeed2 = new System.Windows.Forms.ComboBox();
            this.comboBox_handshake2 = new System.Windows.Forms.ComboBox();
            this.comboBox_databits2 = new System.Windows.Forms.ComboBox();
            this.comboBox_parity2 = new System.Windows.Forms.ComboBox();
            this.comboBox_stopbits2 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.checkBox_sendPort1 = new System.Windows.Forms.CheckBox();
            this.checkBox_sendPort2 = new System.Windows.Forms.CheckBox();
            this.checkBox_cr = new System.Windows.Forms.CheckBox();
            this.checkBox_lf = new System.Windows.Forms.CheckBox();
            this.checkBox_suff = new System.Windows.Forms.CheckBox();
            this.textBox_senddata = new System.Windows.Forms.TextBox();
            this.checkBox_commandhex = new System.Windows.Forms.CheckBox();
            this.checkBox_paramhex = new System.Windows.Forms.CheckBox();
            this.button_clear1 = new System.Windows.Forms.Button();
            this.checkBox_CD1 = new System.Windows.Forms.CheckBox();
            this.checkBox_DSR1 = new System.Windows.Forms.CheckBox();
            this.checkBox_DTR1 = new System.Windows.Forms.CheckBox();
            this.checkBox_RTS1 = new System.Windows.Forms.CheckBox();
            this.checkBox_CTS1 = new System.Windows.Forms.CheckBox();
            this.checkBox_CD2 = new System.Windows.Forms.CheckBox();
            this.checkBox_DSR2 = new System.Windows.Forms.CheckBox();
            this.checkBox_DTR2 = new System.Windows.Forms.CheckBox();
            this.checkBox_RTS2 = new System.Windows.Forms.CheckBox();
            this.checkBox_CTS2 = new System.Windows.Forms.CheckBox();
            this.checkBox_insPin = new System.Windows.Forms.CheckBox();
            this.checkBox_insTime = new System.Windows.Forms.CheckBox();
            this.checkBox_displayPort1hex = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.textBox_suff = new System.Windows.Forms.TextBox();
            this.checkBox_insDir = new System.Windows.Forms.CheckBox();
            this.checkBox_displayPort2hex = new System.Windows.Forms.CheckBox();
            this.label_dispHex = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox_RI1 = new System.Windows.Forms.CheckBox();
            this.checkBox_RI2 = new System.Windows.Forms.CheckBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.checkBox_Mark = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox_portName = new System.Windows.Forms.CheckBox();
            this.textBox_port1Name = new System.Windows.Forms.TextBox();
            this.textBox_port2Name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_onlyData = new System.Windows.Forms.ToolStripMenuItem();
            this.autoscrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosaveTXTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.terminaltxtToolStripMenuItem1 = new System.Windows.Forms.ToolStripTextBox();
            this.autosaveCSVToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.LineBreakToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.LineBreakToolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.limitCSVLinesNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_CSVLinesNumber = new System.Windows.Forms.ToolStripTextBox();
            this.saveParametersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox_suffhex = new System.Windows.Forms.CheckBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox_CD3 = new System.Windows.Forms.CheckBox();
            this.comboBox_portname3 = new System.Windows.Forms.ComboBox();
            this.comboBox_portspeed3 = new System.Windows.Forms.ComboBox();
            this.textBox_port3Name = new System.Windows.Forms.TextBox();
            this.comboBox_handshake3 = new System.Windows.Forms.ComboBox();
            this.comboBox_databits3 = new System.Windows.Forms.ComboBox();
            this.comboBox_parity3 = new System.Windows.Forms.ComboBox();
            this.comboBox_stopbits3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_CTS3 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBox_RTS3 = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.checkBox_RI3 = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBox_DTR3 = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.checkBox_DSR3 = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_CD4 = new System.Windows.Forms.CheckBox();
            this.comboBox_portname4 = new System.Windows.Forms.ComboBox();
            this.comboBox_portspeed4 = new System.Windows.Forms.ComboBox();
            this.textBox_port4Name = new System.Windows.Forms.TextBox();
            this.comboBox_handshake4 = new System.Windows.Forms.ComboBox();
            this.comboBox_databits4 = new System.Windows.Forms.ComboBox();
            this.comboBox_parity4 = new System.Windows.Forms.ComboBox();
            this.comboBox_stopbits4 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.checkBox_CTS4 = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.checkBox_RTS4 = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.checkBox_RI4 = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.checkBox_DTR4 = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.checkBox_DSR4 = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBox_sendPort3 = new System.Windows.Forms.CheckBox();
            this.checkBox_sendPort4 = new System.Windows.Forms.CheckBox();
            this.checkBox_displayPort3hex = new System.Windows.Forms.CheckBox();
            this.checkBox_displayPort4hex = new System.Windows.Forms.CheckBox();
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort4 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_terminal1
            // 
            this.textBox_terminal1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_terminal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_terminal1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_terminal1.HideSelection = false;
            this.textBox_terminal1.Location = new System.Drawing.Point(3, 3);
            this.textBox_terminal1.MaxLength = 20480000;
            this.textBox_terminal1.Multiline = true;
            this.textBox_terminal1.Name = "textBox_terminal1";
            this.textBox_terminal1.ReadOnly = true;
            this.textBox_terminal1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_terminal1.Size = new System.Drawing.Size(686, 108);
            this.textBox_terminal1.TabIndex = 50;
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(12, 103);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(41, 23);
            this.button_send.TabIndex = 24;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // comboBox_portspeed1
            // 
            this.comboBox_portspeed1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portspeed1.FormattingEnabled = true;
            this.comboBox_portspeed1.Items.AddRange(new object[] {
            "250000",
            "230400",
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200",
            "600",
            "300"});
            this.comboBox_portspeed1.Location = new System.Drawing.Point(82, 62);
            this.comboBox_portspeed1.Name = "comboBox_portspeed1";
            this.comboBox_portspeed1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portspeed1.TabIndex = 1;
            // 
            // comboBox_handshake1
            // 
            this.comboBox_handshake1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_handshake1.FormattingEnabled = true;
            this.comboBox_handshake1.Location = new System.Drawing.Point(158, 62);
            this.comboBox_handshake1.Name = "comboBox_handshake1";
            this.comboBox_handshake1.Size = new System.Drawing.Size(141, 21);
            this.comboBox_handshake1.Sorted = true;
            this.comboBox_handshake1.TabIndex = 2;
            // 
            // comboBox_databits1
            // 
            this.comboBox_databits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits1.FormattingEnabled = true;
            this.comboBox_databits1.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_databits1.Location = new System.Drawing.Point(6, 102);
            this.comboBox_databits1.Name = "comboBox_databits1";
            this.comboBox_databits1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_databits1.TabIndex = 3;
            // 
            // comboBox_parity1
            // 
            this.comboBox_parity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_parity1.FormattingEnabled = true;
            this.comboBox_parity1.Location = new System.Drawing.Point(82, 102);
            this.comboBox_parity1.Name = "comboBox_parity1";
            this.comboBox_parity1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_parity1.Sorted = true;
            this.comboBox_parity1.TabIndex = 4;
            // 
            // comboBox_stopbits1
            // 
            this.comboBox_stopbits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits1.FormattingEnabled = true;
            this.comboBox_stopbits1.Location = new System.Drawing.Point(158, 102);
            this.comboBox_stopbits1.Name = "comboBox_stopbits1";
            this.comboBox_stopbits1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_stopbits1.Sorted = true;
            this.comboBox_stopbits1.TabIndex = 5;
            // 
            // textBox_params
            // 
            this.textBox_params.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_params.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_params.Location = new System.Drawing.Point(73, 53);
            this.textBox_params.Name = "textBox_params";
            this.textBox_params.Size = new System.Drawing.Size(383, 20);
            this.textBox_params.TabIndex = 17;
            this.textBox_params.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_params_KeyPress);
            this.textBox_params.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_params.Leave += new System.EventHandler(this.textBox_params_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Flow control";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Parity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Stop bits";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Parameter";
            // 
            // button_openport
            // 
            this.button_openport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_openport.Location = new System.Drawing.Point(642, 494);
            this.button_openport.Name = "button_openport";
            this.button_openport.Size = new System.Drawing.Size(70, 25);
            this.button_openport.TabIndex = 14;
            this.button_openport.Text = "Open ports";
            this.button_openport.UseVisualStyleBackColor = true;
            this.button_openport.Click += new System.EventHandler(this.button_openport_Click);
            // 
            // button_closeport
            // 
            this.button_closeport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_closeport.Enabled = false;
            this.button_closeport.Location = new System.Drawing.Point(642, 525);
            this.button_closeport.Name = "button_closeport";
            this.button_closeport.Size = new System.Drawing.Size(70, 25);
            this.button_closeport.TabIndex = 34;
            this.button_closeport.Text = "Close ports";
            this.button_closeport.UseVisualStyleBackColor = true;
            this.button_closeport.Click += new System.EventHandler(this.button_closeport_Click);
            // 
            // comboBox_portname1
            // 
            this.comboBox_portname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portname1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_portname1.FormattingEnabled = true;
            this.comboBox_portname1.Location = new System.Drawing.Point(6, 62);
            this.comboBox_portname1.Name = "comboBox_portname1";
            this.comboBox_portname1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portname1.Sorted = true;
            this.comboBox_portname1.TabIndex = 0;
            this.comboBox_portname1.SelectedIndexChanged += new System.EventHandler(this.comboBox_portname1_SelectedIndexChanged);
            // 
            // textBox_command
            // 
            this.textBox_command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_command.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_command.Location = new System.Drawing.Point(73, 27);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(383, 20);
            this.textBox_command.TabIndex = 15;
            this.textBox_command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_custom_command_KeyPress);
            this.textBox_command.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_command.Leave += new System.EventHandler(this.textBox_command_Leave);
            // 
            // label_custom_command
            // 
            this.label_custom_command.AutoSize = true;
            this.label_custom_command.Location = new System.Drawing.Point(12, 30);
            this.label_custom_command.Name = "label_custom_command";
            this.label_custom_command.Size = new System.Drawing.Size(54, 13);
            this.label_custom_command.TabIndex = 3;
            this.label_custom_command.Text = "Command";
            // 
            // comboBox_portname2
            // 
            this.comboBox_portname2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portname2.FormattingEnabled = true;
            this.comboBox_portname2.Location = new System.Drawing.Point(6, 65);
            this.comboBox_portname2.Name = "comboBox_portname2";
            this.comboBox_portname2.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portname2.Sorted = true;
            this.comboBox_portname2.TabIndex = 7;
            this.comboBox_portname2.SelectedIndexChanged += new System.EventHandler(this.comboBox_portname2_SelectedIndexChanged);
            // 
            // comboBox_portspeed2
            // 
            this.comboBox_portspeed2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portspeed2.Enabled = false;
            this.comboBox_portspeed2.FormattingEnabled = true;
            this.comboBox_portspeed2.Items.AddRange(new object[] {
            "250000",
            "230400",
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200",
            "600",
            "300"});
            this.comboBox_portspeed2.Location = new System.Drawing.Point(83, 65);
            this.comboBox_portspeed2.Name = "comboBox_portspeed2";
            this.comboBox_portspeed2.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portspeed2.TabIndex = 8;
            // 
            // comboBox_handshake2
            // 
            this.comboBox_handshake2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_handshake2.Enabled = false;
            this.comboBox_handshake2.FormattingEnabled = true;
            this.comboBox_handshake2.Location = new System.Drawing.Point(159, 65);
            this.comboBox_handshake2.Name = "comboBox_handshake2";
            this.comboBox_handshake2.Size = new System.Drawing.Size(140, 21);
            this.comboBox_handshake2.Sorted = true;
            this.comboBox_handshake2.TabIndex = 9;
            // 
            // comboBox_databits2
            // 
            this.comboBox_databits2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits2.Enabled = false;
            this.comboBox_databits2.FormattingEnabled = true;
            this.comboBox_databits2.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_databits2.Location = new System.Drawing.Point(6, 105);
            this.comboBox_databits2.Name = "comboBox_databits2";
            this.comboBox_databits2.Size = new System.Drawing.Size(70, 21);
            this.comboBox_databits2.TabIndex = 10;
            // 
            // comboBox_parity2
            // 
            this.comboBox_parity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_parity2.Enabled = false;
            this.comboBox_parity2.FormattingEnabled = true;
            this.comboBox_parity2.Location = new System.Drawing.Point(82, 105);
            this.comboBox_parity2.Name = "comboBox_parity2";
            this.comboBox_parity2.Size = new System.Drawing.Size(70, 21);
            this.comboBox_parity2.Sorted = true;
            this.comboBox_parity2.TabIndex = 11;
            // 
            // comboBox_stopbits2
            // 
            this.comboBox_stopbits2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits2.Enabled = false;
            this.comboBox_stopbits2.FormattingEnabled = true;
            this.comboBox_stopbits2.Location = new System.Drawing.Point(158, 105);
            this.comboBox_stopbits2.Name = "comboBox_stopbits2";
            this.comboBox_stopbits2.Size = new System.Drawing.Size(70, 21);
            this.comboBox_stopbits2.Sorted = true;
            this.comboBox_stopbits2.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Data bits";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(79, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Port speed";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(79, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Parity";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(155, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Flow control";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(155, 89);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Stop bits";
            // 
            // serialPort2
            // 
            this.serialPort2.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort2_ErrorReceived);
            // 
            // checkBox_sendPort1
            // 
            this.checkBox_sendPort1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_sendPort1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox_sendPort1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_sendPort1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_sendPort1.Location = new System.Drawing.Point(462, 107);
            this.checkBox_sendPort1.Name = "checkBox_sendPort1";
            this.checkBox_sendPort1.Size = new System.Drawing.Size(76, 17);
            this.checkBox_sendPort1.TabIndex = 25;
            this.checkBox_sendPort1.Text = "Port1";
            this.checkBox_sendPort1.UseVisualStyleBackColor = true;
            this.checkBox_sendPort1.CheckedChanged += new System.EventHandler(this.checkBox_send_CheckedChanged);
            // 
            // checkBox_sendPort2
            // 
            this.checkBox_sendPort2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_sendPort2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox_sendPort2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_sendPort2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_sendPort2.Location = new System.Drawing.Point(528, 107);
            this.checkBox_sendPort2.Name = "checkBox_sendPort2";
            this.checkBox_sendPort2.Size = new System.Drawing.Size(76, 17);
            this.checkBox_sendPort2.TabIndex = 26;
            this.checkBox_sendPort2.Text = "Port2";
            this.checkBox_sendPort2.UseVisualStyleBackColor = true;
            this.checkBox_sendPort2.CheckedChanged += new System.EventHandler(this.checkBox_send_CheckedChanged);
            // 
            // checkBox_cr
            // 
            this.checkBox_cr.AutoSize = true;
            this.checkBox_cr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_cr.Location = new System.Drawing.Point(73, 81);
            this.checkBox_cr.Name = "checkBox_cr";
            this.checkBox_cr.Size = new System.Drawing.Size(67, 17);
            this.checkBox_cr.TabIndex = 19;
            this.checkBox_cr.Text = "CR (0D);";
            this.checkBox_cr.UseVisualStyleBackColor = true;
            this.checkBox_cr.CheckedChanged += new System.EventHandler(this.checkBox_cr_CheckedChanged);
            // 
            // checkBox_lf
            // 
            this.checkBox_lf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_lf.AutoSize = true;
            this.checkBox_lf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_lf.Location = new System.Drawing.Point(146, 81);
            this.checkBox_lf.Name = "checkBox_lf";
            this.checkBox_lf.Size = new System.Drawing.Size(63, 17);
            this.checkBox_lf.TabIndex = 20;
            this.checkBox_lf.Text = "LF (0A);";
            this.checkBox_lf.UseVisualStyleBackColor = true;
            this.checkBox_lf.CheckedChanged += new System.EventHandler(this.checkBox_lf_CheckedChanged);
            // 
            // checkBox_suff
            // 
            this.checkBox_suff.AutoSize = true;
            this.checkBox_suff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_suff.Location = new System.Drawing.Point(215, 82);
            this.checkBox_suff.Name = "checkBox_suff";
            this.checkBox_suff.Size = new System.Drawing.Size(15, 14);
            this.checkBox_suff.TabIndex = 21;
            this.checkBox_suff.UseVisualStyleBackColor = true;
            this.checkBox_suff.CheckedChanged += new System.EventHandler(this.checkBox_suff_CheckedChanged);
            // 
            // textBox_senddata
            // 
            this.textBox_senddata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_senddata.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_senddata.Location = new System.Drawing.Point(73, 105);
            this.textBox_senddata.Name = "textBox_senddata";
            this.textBox_senddata.ReadOnly = true;
            this.textBox_senddata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_senddata.Size = new System.Drawing.Size(383, 20);
            this.textBox_senddata.TabIndex = 49;
            // 
            // checkBox_commandhex
            // 
            this.checkBox_commandhex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_commandhex.AutoSize = true;
            this.checkBox_commandhex.Location = new System.Drawing.Point(462, 29);
            this.checkBox_commandhex.Name = "checkBox_commandhex";
            this.checkBox_commandhex.Size = new System.Drawing.Size(43, 17);
            this.checkBox_commandhex.TabIndex = 16;
            this.checkBox_commandhex.Text = "hex";
            this.checkBox_commandhex.UseVisualStyleBackColor = true;
            this.checkBox_commandhex.CheckedChanged += new System.EventHandler(this.checkBox_commandhex_CheckedChanged);
            // 
            // checkBox_paramhex
            // 
            this.checkBox_paramhex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_paramhex.AutoSize = true;
            this.checkBox_paramhex.Location = new System.Drawing.Point(462, 55);
            this.checkBox_paramhex.Name = "checkBox_paramhex";
            this.checkBox_paramhex.Size = new System.Drawing.Size(43, 17);
            this.checkBox_paramhex.TabIndex = 18;
            this.checkBox_paramhex.Text = "hex";
            this.checkBox_paramhex.UseVisualStyleBackColor = true;
            this.checkBox_paramhex.CheckedChanged += new System.EventHandler(this.checkBox_paramhex_CheckedChanged);
            // 
            // button_clear1
            // 
            this.button_clear1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_clear1.Location = new System.Drawing.Point(640, 423);
            this.button_clear1.Name = "button_clear1";
            this.button_clear1.Size = new System.Drawing.Size(70, 24);
            this.button_clear1.TabIndex = 30;
            this.button_clear1.Text = "Clear";
            this.button_clear1.UseVisualStyleBackColor = true;
            this.button_clear1.Click += new System.EventHandler(this.button_clear1_Click);
            // 
            // checkBox_CD1
            // 
            this.checkBox_CD1.AutoSize = true;
            this.checkBox_CD1.Enabled = false;
            this.checkBox_CD1.Location = new System.Drawing.Point(6, 26);
            this.checkBox_CD1.Name = "checkBox_CD1";
            this.checkBox_CD1.Size = new System.Drawing.Size(41, 17);
            this.checkBox_CD1.TabIndex = 17;
            this.checkBox_CD1.Text = "CD";
            this.checkBox_CD1.UseVisualStyleBackColor = true;
            // 
            // checkBox_DSR1
            // 
            this.checkBox_DSR1.AutoSize = true;
            this.checkBox_DSR1.Enabled = false;
            this.checkBox_DSR1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DSR1.Location = new System.Drawing.Point(53, 26);
            this.checkBox_DSR1.Name = "checkBox_DSR1";
            this.checkBox_DSR1.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DSR1.TabIndex = 18;
            this.checkBox_DSR1.Text = "DSR";
            this.checkBox_DSR1.UseVisualStyleBackColor = true;
            // 
            // checkBox_DTR1
            // 
            this.checkBox_DTR1.AutoSize = true;
            this.checkBox_DTR1.Enabled = false;
            this.checkBox_DTR1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DTR1.Location = new System.Drawing.Point(108, 26);
            this.checkBox_DTR1.Name = "checkBox_DTR1";
            this.checkBox_DTR1.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DTR1.TabIndex = 19;
            this.checkBox_DTR1.Text = "DTR";
            this.checkBox_DTR1.UseVisualStyleBackColor = true;
            this.checkBox_DTR1.CheckedChanged += new System.EventHandler(this.checkBox_DTR1_CheckedChanged);
            // 
            // checkBox_RTS1
            // 
            this.checkBox_RTS1.AutoSize = true;
            this.checkBox_RTS1.Enabled = false;
            this.checkBox_RTS1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RTS1.Location = new System.Drawing.Point(163, 26);
            this.checkBox_RTS1.Name = "checkBox_RTS1";
            this.checkBox_RTS1.Size = new System.Drawing.Size(48, 17);
            this.checkBox_RTS1.TabIndex = 20;
            this.checkBox_RTS1.Text = "RTS";
            this.checkBox_RTS1.UseVisualStyleBackColor = true;
            this.checkBox_RTS1.CheckedChanged += new System.EventHandler(this.checkBox_RTS1_CheckedChanged);
            // 
            // checkBox_CTS1
            // 
            this.checkBox_CTS1.AutoSize = true;
            this.checkBox_CTS1.Enabled = false;
            this.checkBox_CTS1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CTS1.Location = new System.Drawing.Point(217, 26);
            this.checkBox_CTS1.Name = "checkBox_CTS1";
            this.checkBox_CTS1.Size = new System.Drawing.Size(47, 17);
            this.checkBox_CTS1.TabIndex = 21;
            this.checkBox_CTS1.Text = "CTS";
            this.checkBox_CTS1.UseVisualStyleBackColor = true;
            // 
            // checkBox_CD2
            // 
            this.checkBox_CD2.AutoSize = true;
            this.checkBox_CD2.Enabled = false;
            this.checkBox_CD2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CD2.Location = new System.Drawing.Point(6, 29);
            this.checkBox_CD2.Name = "checkBox_CD2";
            this.checkBox_CD2.Size = new System.Drawing.Size(41, 17);
            this.checkBox_CD2.TabIndex = 25;
            this.checkBox_CD2.Text = "CD";
            this.checkBox_CD2.UseVisualStyleBackColor = true;
            // 
            // checkBox_DSR2
            // 
            this.checkBox_DSR2.AutoSize = true;
            this.checkBox_DSR2.Enabled = false;
            this.checkBox_DSR2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DSR2.Location = new System.Drawing.Point(53, 29);
            this.checkBox_DSR2.Name = "checkBox_DSR2";
            this.checkBox_DSR2.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DSR2.TabIndex = 26;
            this.checkBox_DSR2.Text = "DSR";
            this.checkBox_DSR2.UseVisualStyleBackColor = true;
            // 
            // checkBox_DTR2
            // 
            this.checkBox_DTR2.AutoSize = true;
            this.checkBox_DTR2.Enabled = false;
            this.checkBox_DTR2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DTR2.Location = new System.Drawing.Point(108, 29);
            this.checkBox_DTR2.Name = "checkBox_DTR2";
            this.checkBox_DTR2.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DTR2.TabIndex = 27;
            this.checkBox_DTR2.Text = "DTR";
            this.checkBox_DTR2.UseVisualStyleBackColor = true;
            this.checkBox_DTR2.CheckedChanged += new System.EventHandler(this.checkBox_DTR2_CheckedChanged);
            // 
            // checkBox_RTS2
            // 
            this.checkBox_RTS2.AutoSize = true;
            this.checkBox_RTS2.Enabled = false;
            this.checkBox_RTS2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RTS2.Location = new System.Drawing.Point(163, 29);
            this.checkBox_RTS2.Name = "checkBox_RTS2";
            this.checkBox_RTS2.Size = new System.Drawing.Size(48, 17);
            this.checkBox_RTS2.TabIndex = 28;
            this.checkBox_RTS2.Text = "RTS";
            this.checkBox_RTS2.UseVisualStyleBackColor = true;
            this.checkBox_RTS2.CheckedChanged += new System.EventHandler(this.checkBox_RTS2_CheckedChanged);
            // 
            // checkBox_CTS2
            // 
            this.checkBox_CTS2.AutoSize = true;
            this.checkBox_CTS2.Enabled = false;
            this.checkBox_CTS2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CTS2.Location = new System.Drawing.Point(217, 29);
            this.checkBox_CTS2.Name = "checkBox_CTS2";
            this.checkBox_CTS2.Size = new System.Drawing.Size(47, 17);
            this.checkBox_CTS2.TabIndex = 29;
            this.checkBox_CTS2.Text = "CTS";
            this.checkBox_CTS2.UseVisualStyleBackColor = true;
            // 
            // checkBox_insPin
            // 
            this.checkBox_insPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_insPin.AutoSize = true;
            this.checkBox_insPin.Checked = true;
            this.checkBox_insPin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_insPin.Location = new System.Drawing.Point(15, 19);
            this.checkBox_insPin.Name = "checkBox_insPin";
            this.checkBox_insPin.Size = new System.Drawing.Size(84, 17);
            this.checkBox_insPin.TabIndex = 0;
            this.checkBox_insPin.Text = "signal status";
            this.checkBox_insPin.UseVisualStyleBackColor = true;
            this.checkBox_insPin.CheckedChanged += new System.EventHandler(this.checkBox_insPin_CheckedChanged);
            // 
            // checkBox_insTime
            // 
            this.checkBox_insTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_insTime.AutoSize = true;
            this.checkBox_insTime.BackColor = System.Drawing.SystemColors.ControlLight;
            this.checkBox_insTime.Checked = true;
            this.checkBox_insTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_insTime.Location = new System.Drawing.Point(129, 19);
            this.checkBox_insTime.Name = "checkBox_insTime";
            this.checkBox_insTime.Size = new System.Drawing.Size(45, 17);
            this.checkBox_insTime.TabIndex = 2;
            this.checkBox_insTime.Text = "time";
            this.checkBox_insTime.UseVisualStyleBackColor = false;
            // 
            // checkBox_displayPort1hex
            // 
            this.checkBox_displayPort1hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_displayPort1hex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_displayPort1hex.Location = new System.Drawing.Point(642, 294);
            this.checkBox_displayPort1hex.Name = "checkBox_displayPort1hex";
            this.checkBox_displayPort1hex.Size = new System.Drawing.Size(76, 17);
            this.checkBox_displayPort1hex.TabIndex = 27;
            this.checkBox_displayPort1hex.Text = "Port 1";
            this.checkBox_displayPort1hex.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox_displayPort1hex.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.FileName = "terminal.txt";
            this.saveFileDialog.Filter = "Text files|*.txt|All files|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // textBox_suff
            // 
            this.textBox_suff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_suff.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_suff.Location = new System.Drawing.Point(236, 80);
            this.textBox_suff.MaxLength = 30;
            this.textBox_suff.Name = "textBox_suff";
            this.textBox_suff.Size = new System.Drawing.Size(220, 20);
            this.textBox_suff.TabIndex = 22;
            this.textBox_suff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_suff_KeyPress);
            this.textBox_suff.Leave += new System.EventHandler(this.textBox_suff_Leave);
            // 
            // checkBox_insDir
            // 
            this.checkBox_insDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_insDir.AutoSize = true;
            this.checkBox_insDir.Checked = true;
            this.checkBox_insDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_insDir.Location = new System.Drawing.Point(129, 42);
            this.checkBox_insDir.Name = "checkBox_insDir";
            this.checkBox_insDir.Size = new System.Drawing.Size(66, 17);
            this.checkBox_insDir.TabIndex = 3;
            this.checkBox_insDir.Text = "direction";
            this.checkBox_insDir.UseVisualStyleBackColor = true;
            // 
            // checkBox_displayPort2hex
            // 
            this.checkBox_displayPort2hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_displayPort2hex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_displayPort2hex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_displayPort2hex.Location = new System.Drawing.Point(642, 317);
            this.checkBox_displayPort2hex.Name = "checkBox_displayPort2hex";
            this.checkBox_displayPort2hex.Size = new System.Drawing.Size(76, 17);
            this.checkBox_displayPort2hex.TabIndex = 28;
            this.checkBox_displayPort2hex.Text = "Port 2";
            this.checkBox_displayPort2hex.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox_displayPort2hex.UseVisualStyleBackColor = true;
            // 
            // label_dispHex
            // 
            this.label_dispHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_dispHex.AutoSize = true;
            this.label_dispHex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_dispHex.Location = new System.Drawing.Point(642, 278);
            this.label_dispHex.Name = "label_dispHex";
            this.label_dispHex.Size = new System.Drawing.Size(76, 13);
            this.label_dispHex.TabIndex = 3;
            this.label_dispHex.Text = "Display hex:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(12, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Line end";
            // 
            // checkBox_RI1
            // 
            this.checkBox_RI1.AutoSize = true;
            this.checkBox_RI1.Enabled = false;
            this.checkBox_RI1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RI1.Location = new System.Drawing.Point(270, 26);
            this.checkBox_RI1.Name = "checkBox_RI1";
            this.checkBox_RI1.Size = new System.Drawing.Size(37, 17);
            this.checkBox_RI1.TabIndex = 22;
            this.checkBox_RI1.Text = "RI";
            this.checkBox_RI1.UseVisualStyleBackColor = true;
            // 
            // checkBox_RI2
            // 
            this.checkBox_RI2.AutoSize = true;
            this.checkBox_RI2.Enabled = false;
            this.checkBox_RI2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RI2.Location = new System.Drawing.Point(270, 29);
            this.checkBox_RI2.Name = "checkBox_RI2";
            this.checkBox_RI2.Size = new System.Drawing.Size(37, 17);
            this.checkBox_RI2.TabIndex = 30;
            this.checkBox_RI2.Text = "RI";
            this.checkBox_RI2.UseVisualStyleBackColor = true;
            // 
            // serialPort1
            // 
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(686, 108);
            this.dataGridView.StandardTab = true;
            this.dataGridView.TabIndex = 51;
            this.dataGridView.VirtualMode = true;
            // 
            // checkBox_Mark
            // 
            this.checkBox_Mark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Mark.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Mark.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBox_Mark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_Mark.Location = new System.Drawing.Point(642, 386);
            this.checkBox_Mark.Name = "checkBox_Mark";
            this.checkBox_Mark.Size = new System.Drawing.Size(70, 23);
            this.checkBox_Mark.TabIndex = 29;
            this.checkBox_Mark.Text = "Mark";
            this.checkBox_Mark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_Mark.UseVisualStyleBackColor = true;
            this.checkBox_Mark.CheckedChanged += new System.EventHandler(this.checkBox_Mark_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 132);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(700, 140);
            this.tabControl1.TabIndex = 32;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.textBox_terminal1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(692, 114);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.dataGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(692, 114);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_portName
            // 
            this.checkBox_portName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_portName.AutoSize = true;
            this.checkBox_portName.Checked = true;
            this.checkBox_portName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_portName.Location = new System.Drawing.Point(15, 42);
            this.checkBox_portName.Name = "checkBox_portName";
            this.checkBox_portName.Size = new System.Drawing.Size(73, 17);
            this.checkBox_portName.TabIndex = 1;
            this.checkBox_portName.Text = "port name";
            this.checkBox_portName.UseVisualStyleBackColor = true;
            this.checkBox_portName.CheckedChanged += new System.EventHandler(this.checkBox_portName_CheckedChanged);
            // 
            // textBox_port1Name
            // 
            this.textBox_port1Name.Location = new System.Drawing.Point(234, 102);
            this.textBox_port1Name.MaxLength = 50;
            this.textBox_port1Name.Name = "textBox_port1Name";
            this.textBox_port1Name.Size = new System.Drawing.Size(65, 20);
            this.textBox_port1Name.TabIndex = 6;
            // 
            // textBox_port2Name
            // 
            this.textBox_port2Name.Location = new System.Drawing.Point(234, 105);
            this.textBox_port2Name.MaxLength = 50;
            this.textBox_port2Name.Name = "textBox_port2Name";
            this.textBox_port2Name.Size = new System.Drawing.Size(65, 20);
            this.textBox_port2Name.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "COM #";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(231, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Port 1 name";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "COM #";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(231, 89);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Port 2 name";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox_insPin);
            this.groupBox1.Controls.Add(this.checkBox_insTime);
            this.groupBox1.Controls.Add(this.checkBox_insDir);
            this.groupBox1.Controls.Add(this.checkBox_portName);
            this.groupBox1.Location = new System.Drawing.Point(511, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 67);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log options";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(724, 24);
            this.menuStrip.TabIndex = 35;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTXTToolStripMenuItem,
            this.saveCSVToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveTXTToolStripMenuItem
            // 
            this.saveTXTToolStripMenuItem.Name = "saveTXTToolStripMenuItem";
            this.saveTXTToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveTXTToolStripMenuItem.Text = "Save .TXT";
            this.saveTXTToolStripMenuItem.Click += new System.EventHandler(this.saveTXTToolStripMenuItem_Click);
            // 
            // saveCSVToolStripMenuItem
            // 
            this.saveCSVToolStripMenuItem.Name = "saveCSVToolStripMenuItem";
            this.saveCSVToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveCSVToolStripMenuItem.Text = "Save .CSV";
            this.saveCSVToolStripMenuItem.Click += new System.EventHandler(this.saveCSVToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToGridToolStripMenuItem,
            this.logToTextToolStripMenuItem,
            this.toolStripMenuItem_onlyData,
            this.autoscrollToolStripMenuItem,
            this.lineWrapToolStripMenuItem,
            this.autosaveTXTToolStripMenuItem1,
            this.autosaveCSVToolStripMenuItem1,
            this.LineBreakToolStripMenuItem1,
            this.limitCSVLinesNumberToolStripMenuItem,
            this.saveParametersToolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // logToGridToolStripMenuItem
            // 
            this.logToGridToolStripMenuItem.Checked = true;
            this.logToGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logToGridToolStripMenuItem.Name = "logToGridToolStripMenuItem";
            this.logToGridToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.logToGridToolStripMenuItem.Text = "Log to table";
            this.logToGridToolStripMenuItem.Click += new System.EventHandler(this.logToGridToolStripMenuItem_Click);
            // 
            // logToTextToolStripMenuItem
            // 
            this.logToTextToolStripMenuItem.Checked = true;
            this.logToTextToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logToTextToolStripMenuItem.Name = "logToTextToolStripMenuItem";
            this.logToTextToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.logToTextToolStripMenuItem.Text = "Log to text";
            this.logToTextToolStripMenuItem.Click += new System.EventHandler(this.logToTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_onlyData
            // 
            this.toolStripMenuItem_onlyData.Name = "toolStripMenuItem_onlyData";
            this.toolStripMenuItem_onlyData.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItem_onlyData.Text = "SimpleData logging";
            this.toolStripMenuItem_onlyData.Click += new System.EventHandler(this.toolStripMenuItem_onlyData_Click);
            // 
            // autoscrollToolStripMenuItem
            // 
            this.autoscrollToolStripMenuItem.Checked = true;
            this.autoscrollToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoscrollToolStripMenuItem.Enabled = false;
            this.autoscrollToolStripMenuItem.Name = "autoscrollToolStripMenuItem";
            this.autoscrollToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.autoscrollToolStripMenuItem.Text = "Autoscroll";
            this.autoscrollToolStripMenuItem.Click += new System.EventHandler(this.autoscrollToolStripMenuItem_Click);
            // 
            // lineWrapToolStripMenuItem
            // 
            this.lineWrapToolStripMenuItem.Checked = true;
            this.lineWrapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lineWrapToolStripMenuItem.Name = "lineWrapToolStripMenuItem";
            this.lineWrapToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.lineWrapToolStripMenuItem.Text = "Line wrap";
            this.lineWrapToolStripMenuItem.Click += new System.EventHandler(this.lineWrapToolStripMenuItem_Click);
            // 
            // autosaveTXTToolStripMenuItem1
            // 
            this.autosaveTXTToolStripMenuItem1.Checked = true;
            this.autosaveTXTToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autosaveTXTToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminaltxtToolStripMenuItem1});
            this.autosaveTXTToolStripMenuItem1.Name = "autosaveTXTToolStripMenuItem1";
            this.autosaveTXTToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.autosaveTXTToolStripMenuItem1.Text = "Autosave .TXT";
            this.autosaveTXTToolStripMenuItem1.Click += new System.EventHandler(this.autosaveTXTToolStripMenuItem1_Click);
            // 
            // terminaltxtToolStripMenuItem1
            // 
            this.terminaltxtToolStripMenuItem1.Name = "terminaltxtToolStripMenuItem1";
            this.terminaltxtToolStripMenuItem1.Size = new System.Drawing.Size(152, 23);
            this.terminaltxtToolStripMenuItem1.Text = "terminal.txt";
            // 
            // autosaveCSVToolStripMenuItem1
            // 
            this.autosaveCSVToolStripMenuItem1.Checked = true;
            this.autosaveCSVToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autosaveCSVToolStripMenuItem1.Name = "autosaveCSVToolStripMenuItem1";
            this.autosaveCSVToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.autosaveCSVToolStripMenuItem1.Text = "Autosave .CSV";
            this.autosaveCSVToolStripMenuItem1.Click += new System.EventHandler(this.autosaveCSVToolStripMenuItem1_Click);
            // 
            // LineBreakToolStripMenuItem1
            // 
            this.LineBreakToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LineBreakToolStripTextBox1});
            this.LineBreakToolStripMenuItem1.Name = "LineBreakToolStripMenuItem1";
            this.LineBreakToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.LineBreakToolStripMenuItem1.Text = "Line break timeout [ms]";
            // 
            // LineBreakToolStripTextBox1
            // 
            this.LineBreakToolStripTextBox1.MaxLength = 5;
            this.LineBreakToolStripTextBox1.Name = "LineBreakToolStripTextBox1";
            this.LineBreakToolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.LineBreakToolStripTextBox1.Text = "1000";
            this.LineBreakToolStripTextBox1.Leave += new System.EventHandler(this.LineBreakToolStripTextBox1_Leave);
            // 
            // limitCSVLinesNumberToolStripMenuItem
            // 
            this.limitCSVLinesNumberToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_CSVLinesNumber});
            this.limitCSVLinesNumberToolStripMenuItem.Name = "limitCSVLinesNumberToolStripMenuItem";
            this.limitCSVLinesNumberToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.limitCSVLinesNumberToolStripMenuItem.Text = "Limit CSV file lines number";
            // 
            // toolStripTextBox_CSVLinesNumber
            // 
            this.toolStripTextBox_CSVLinesNumber.MaxLength = 5;
            this.toolStripTextBox_CSVLinesNumber.Name = "toolStripTextBox_CSVLinesNumber";
            this.toolStripTextBox_CSVLinesNumber.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_CSVLinesNumber.Text = "1024";
            this.toolStripTextBox_CSVLinesNumber.Leave += new System.EventHandler(this.toolStripTextBox_CSVLinesNumber_Leave);
            // 
            // saveParametersToolStripMenuItem1
            // 
            this.saveParametersToolStripMenuItem1.Name = "saveParametersToolStripMenuItem1";
            this.saveParametersToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.saveParametersToolStripMenuItem1.Text = "Save parameters";
            this.saveParametersToolStripMenuItem1.Click += new System.EventHandler(this.saveParametersToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkBox_suffhex
            // 
            this.checkBox_suffhex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_suffhex.AutoSize = true;
            this.checkBox_suffhex.Location = new System.Drawing.Point(462, 81);
            this.checkBox_suffhex.Name = "checkBox_suffhex";
            this.checkBox_suffhex.Size = new System.Drawing.Size(43, 17);
            this.checkBox_suffhex.TabIndex = 23;
            this.checkBox_suffhex.Text = "hex";
            this.checkBox_suffhex.UseVisualStyleBackColor = true;
            this.checkBox_suffhex.CheckedChanged += new System.EventHandler(this.checkBox_suffhex_CheckedChanged);
            // 
            // button_refresh
            // 
            this.button_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_refresh.Location = new System.Drawing.Point(642, 465);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(70, 23);
            this.button_refresh.TabIndex = 33;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.checkBox_CD1);
            this.groupBox2.Controls.Add(this.comboBox_portname1);
            this.groupBox2.Controls.Add(this.comboBox_portspeed1);
            this.groupBox2.Controls.Add(this.textBox_port1Name);
            this.groupBox2.Controls.Add(this.comboBox_handshake1);
            this.groupBox2.Controls.Add(this.comboBox_databits1);
            this.groupBox2.Controls.Add(this.comboBox_parity1);
            this.groupBox2.Controls.Add(this.comboBox_stopbits1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.checkBox_RI1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBox_CTS1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checkBox_DSR1);
            this.groupBox2.Controls.Add(this.checkBox_RTS1);
            this.groupBox2.Controls.Add(this.checkBox_DTR1);
            this.groupBox2.Location = new System.Drawing.Point(12, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 133);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port1";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.checkBox_CD2);
            this.groupBox3.Controls.Add(this.comboBox_portname2);
            this.groupBox3.Controls.Add(this.comboBox_portspeed2);
            this.groupBox3.Controls.Add(this.textBox_port2Name);
            this.groupBox3.Controls.Add(this.comboBox_handshake2);
            this.groupBox3.Controls.Add(this.comboBox_databits2);
            this.groupBox3.Controls.Add(this.comboBox_parity2);
            this.groupBox3.Controls.Add(this.comboBox_stopbits2);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.checkBox_CTS2);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.checkBox_RTS2);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.checkBox_RI2);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.checkBox_DTR2);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.checkBox_DSR2);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(327, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 133);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Port2";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.checkBox_CD3);
            this.groupBox4.Controls.Add(this.comboBox_portname3);
            this.groupBox4.Controls.Add(this.comboBox_portspeed3);
            this.groupBox4.Controls.Add(this.textBox_port3Name);
            this.groupBox4.Controls.Add(this.comboBox_handshake3);
            this.groupBox4.Controls.Add(this.comboBox_databits3);
            this.groupBox4.Controls.Add(this.comboBox_parity3);
            this.groupBox4.Controls.Add(this.comboBox_stopbits3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.checkBox_CTS3);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.checkBox_RTS3);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.checkBox_RI3);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.checkBox_DTR3);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.checkBox_DSR3);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Location = new System.Drawing.Point(12, 417);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(309, 133);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Port3";
            // 
            // checkBox_CD3
            // 
            this.checkBox_CD3.AutoSize = true;
            this.checkBox_CD3.Enabled = false;
            this.checkBox_CD3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CD3.Location = new System.Drawing.Point(6, 29);
            this.checkBox_CD3.Name = "checkBox_CD3";
            this.checkBox_CD3.Size = new System.Drawing.Size(41, 17);
            this.checkBox_CD3.TabIndex = 25;
            this.checkBox_CD3.Text = "CD";
            this.checkBox_CD3.UseVisualStyleBackColor = true;
            // 
            // comboBox_portname3
            // 
            this.comboBox_portname3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portname3.FormattingEnabled = true;
            this.comboBox_portname3.Location = new System.Drawing.Point(6, 65);
            this.comboBox_portname3.Name = "comboBox_portname3";
            this.comboBox_portname3.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portname3.Sorted = true;
            this.comboBox_portname3.TabIndex = 7;
            this.comboBox_portname3.SelectedIndexChanged += new System.EventHandler(this.comboBox_portname3_SelectedIndexChanged);
            // 
            // comboBox_portspeed3
            // 
            this.comboBox_portspeed3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portspeed3.Enabled = false;
            this.comboBox_portspeed3.FormattingEnabled = true;
            this.comboBox_portspeed3.Items.AddRange(new object[] {
            "250000",
            "230400",
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200",
            "600",
            "300"});
            this.comboBox_portspeed3.Location = new System.Drawing.Point(83, 65);
            this.comboBox_portspeed3.Name = "comboBox_portspeed3";
            this.comboBox_portspeed3.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portspeed3.TabIndex = 8;
            // 
            // textBox_port3Name
            // 
            this.textBox_port3Name.Location = new System.Drawing.Point(234, 105);
            this.textBox_port3Name.MaxLength = 50;
            this.textBox_port3Name.Name = "textBox_port3Name";
            this.textBox_port3Name.Size = new System.Drawing.Size(65, 20);
            this.textBox_port3Name.TabIndex = 13;
            // 
            // comboBox_handshake3
            // 
            this.comboBox_handshake3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_handshake3.Enabled = false;
            this.comboBox_handshake3.FormattingEnabled = true;
            this.comboBox_handshake3.Location = new System.Drawing.Point(159, 65);
            this.comboBox_handshake3.Name = "comboBox_handshake3";
            this.comboBox_handshake3.Size = new System.Drawing.Size(140, 21);
            this.comboBox_handshake3.Sorted = true;
            this.comboBox_handshake3.TabIndex = 9;
            // 
            // comboBox_databits3
            // 
            this.comboBox_databits3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits3.Enabled = false;
            this.comboBox_databits3.FormattingEnabled = true;
            this.comboBox_databits3.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_databits3.Location = new System.Drawing.Point(6, 105);
            this.comboBox_databits3.Name = "comboBox_databits3";
            this.comboBox_databits3.Size = new System.Drawing.Size(70, 21);
            this.comboBox_databits3.TabIndex = 10;
            // 
            // comboBox_parity3
            // 
            this.comboBox_parity3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_parity3.Enabled = false;
            this.comboBox_parity3.FormattingEnabled = true;
            this.comboBox_parity3.Location = new System.Drawing.Point(82, 105);
            this.comboBox_parity3.Name = "comboBox_parity3";
            this.comboBox_parity3.Size = new System.Drawing.Size(70, 21);
            this.comboBox_parity3.Sorted = true;
            this.comboBox_parity3.TabIndex = 11;
            // 
            // comboBox_stopbits3
            // 
            this.comboBox_stopbits3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits3.Enabled = false;
            this.comboBox_stopbits3.FormattingEnabled = true;
            this.comboBox_stopbits3.Location = new System.Drawing.Point(158, 105);
            this.comboBox_stopbits3.Name = "comboBox_stopbits3";
            this.comboBox_stopbits3.Size = new System.Drawing.Size(70, 21);
            this.comboBox_stopbits3.Sorted = true;
            this.comboBox_stopbits3.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data bits";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "COM #";
            // 
            // checkBox_CTS3
            // 
            this.checkBox_CTS3.AutoSize = true;
            this.checkBox_CTS3.Enabled = false;
            this.checkBox_CTS3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CTS3.Location = new System.Drawing.Point(217, 29);
            this.checkBox_CTS3.Name = "checkBox_CTS3";
            this.checkBox_CTS3.Size = new System.Drawing.Size(47, 17);
            this.checkBox_CTS3.TabIndex = 29;
            this.checkBox_CTS3.Text = "CTS";
            this.checkBox_CTS3.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(231, 89);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Port 3 name";
            // 
            // checkBox_RTS3
            // 
            this.checkBox_RTS3.AutoSize = true;
            this.checkBox_RTS3.Enabled = false;
            this.checkBox_RTS3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RTS3.Location = new System.Drawing.Point(163, 29);
            this.checkBox_RTS3.Name = "checkBox_RTS3";
            this.checkBox_RTS3.Size = new System.Drawing.Size(48, 17);
            this.checkBox_RTS3.TabIndex = 28;
            this.checkBox_RTS3.Text = "RTS";
            this.checkBox_RTS3.UseVisualStyleBackColor = true;
            this.checkBox_RTS3.CheckedChanged += new System.EventHandler(this.checkBox_RTS3_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(79, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Port speed";
            // 
            // checkBox_RI3
            // 
            this.checkBox_RI3.AutoSize = true;
            this.checkBox_RI3.Enabled = false;
            this.checkBox_RI3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RI3.Location = new System.Drawing.Point(270, 29);
            this.checkBox_RI3.Name = "checkBox_RI3";
            this.checkBox_RI3.Size = new System.Drawing.Size(37, 17);
            this.checkBox_RI3.TabIndex = 30;
            this.checkBox_RI3.Text = "RI";
            this.checkBox_RI3.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(79, 89);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Parity";
            // 
            // checkBox_DTR3
            // 
            this.checkBox_DTR3.AutoSize = true;
            this.checkBox_DTR3.Enabled = false;
            this.checkBox_DTR3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DTR3.Location = new System.Drawing.Point(108, 29);
            this.checkBox_DTR3.Name = "checkBox_DTR3";
            this.checkBox_DTR3.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DTR3.TabIndex = 27;
            this.checkBox_DTR3.Text = "DTR";
            this.checkBox_DTR3.UseVisualStyleBackColor = true;
            this.checkBox_DTR3.CheckedChanged += new System.EventHandler(this.checkBox_DTR3_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(155, 49);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Flow control";
            // 
            // checkBox_DSR3
            // 
            this.checkBox_DSR3.AutoSize = true;
            this.checkBox_DSR3.Enabled = false;
            this.checkBox_DSR3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DSR3.Location = new System.Drawing.Point(53, 29);
            this.checkBox_DSR3.Name = "checkBox_DSR3";
            this.checkBox_DSR3.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DSR3.TabIndex = 26;
            this.checkBox_DSR3.Text = "DSR";
            this.checkBox_DSR3.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(155, 89);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Stop bits";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.checkBox_CD4);
            this.groupBox5.Controls.Add(this.comboBox_portname4);
            this.groupBox5.Controls.Add(this.comboBox_portspeed4);
            this.groupBox5.Controls.Add(this.textBox_port4Name);
            this.groupBox5.Controls.Add(this.comboBox_handshake4);
            this.groupBox5.Controls.Add(this.comboBox_databits4);
            this.groupBox5.Controls.Add(this.comboBox_parity4);
            this.groupBox5.Controls.Add(this.comboBox_stopbits4);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.checkBox_CTS4);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.checkBox_RTS4);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.checkBox_RI4);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.checkBox_DTR4);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.checkBox_DSR4);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Location = new System.Drawing.Point(327, 417);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(309, 133);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Port4";
            // 
            // checkBox_CD4
            // 
            this.checkBox_CD4.AutoSize = true;
            this.checkBox_CD4.Enabled = false;
            this.checkBox_CD4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CD4.Location = new System.Drawing.Point(6, 29);
            this.checkBox_CD4.Name = "checkBox_CD4";
            this.checkBox_CD4.Size = new System.Drawing.Size(41, 17);
            this.checkBox_CD4.TabIndex = 25;
            this.checkBox_CD4.Text = "CD";
            this.checkBox_CD4.UseVisualStyleBackColor = true;
            // 
            // comboBox_portname4
            // 
            this.comboBox_portname4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portname4.FormattingEnabled = true;
            this.comboBox_portname4.Location = new System.Drawing.Point(6, 65);
            this.comboBox_portname4.Name = "comboBox_portname4";
            this.comboBox_portname4.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portname4.Sorted = true;
            this.comboBox_portname4.TabIndex = 7;
            this.comboBox_portname4.SelectedIndexChanged += new System.EventHandler(this.comboBox_portname4_SelectedIndexChanged);
            // 
            // comboBox_portspeed4
            // 
            this.comboBox_portspeed4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portspeed4.Enabled = false;
            this.comboBox_portspeed4.FormattingEnabled = true;
            this.comboBox_portspeed4.Items.AddRange(new object[] {
            "250000",
            "230400",
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200",
            "600",
            "300"});
            this.comboBox_portspeed4.Location = new System.Drawing.Point(83, 65);
            this.comboBox_portspeed4.Name = "comboBox_portspeed4";
            this.comboBox_portspeed4.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portspeed4.TabIndex = 8;
            // 
            // textBox_port4Name
            // 
            this.textBox_port4Name.Location = new System.Drawing.Point(234, 105);
            this.textBox_port4Name.MaxLength = 50;
            this.textBox_port4Name.Name = "textBox_port4Name";
            this.textBox_port4Name.Size = new System.Drawing.Size(65, 20);
            this.textBox_port4Name.TabIndex = 13;
            // 
            // comboBox_handshake4
            // 
            this.comboBox_handshake4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_handshake4.Enabled = false;
            this.comboBox_handshake4.FormattingEnabled = true;
            this.comboBox_handshake4.Location = new System.Drawing.Point(159, 65);
            this.comboBox_handshake4.Name = "comboBox_handshake4";
            this.comboBox_handshake4.Size = new System.Drawing.Size(140, 21);
            this.comboBox_handshake4.Sorted = true;
            this.comboBox_handshake4.TabIndex = 9;
            // 
            // comboBox_databits4
            // 
            this.comboBox_databits4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits4.Enabled = false;
            this.comboBox_databits4.FormattingEnabled = true;
            this.comboBox_databits4.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_databits4.Location = new System.Drawing.Point(6, 105);
            this.comboBox_databits4.Name = "comboBox_databits4";
            this.comboBox_databits4.Size = new System.Drawing.Size(70, 21);
            this.comboBox_databits4.TabIndex = 10;
            // 
            // comboBox_parity4
            // 
            this.comboBox_parity4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_parity4.Enabled = false;
            this.comboBox_parity4.FormattingEnabled = true;
            this.comboBox_parity4.Location = new System.Drawing.Point(82, 105);
            this.comboBox_parity4.Name = "comboBox_parity4";
            this.comboBox_parity4.Size = new System.Drawing.Size(70, 21);
            this.comboBox_parity4.Sorted = true;
            this.comboBox_parity4.TabIndex = 11;
            // 
            // comboBox_stopbits4
            // 
            this.comboBox_stopbits4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits4.Enabled = false;
            this.comboBox_stopbits4.FormattingEnabled = true;
            this.comboBox_stopbits4.Location = new System.Drawing.Point(158, 105);
            this.comboBox_stopbits4.Name = "comboBox_stopbits4";
            this.comboBox_stopbits4.Size = new System.Drawing.Size(70, 21);
            this.comboBox_stopbits4.Sorted = true;
            this.comboBox_stopbits4.TabIndex = 12;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 89);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Data bits";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 49);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "COM #";
            // 
            // checkBox_CTS4
            // 
            this.checkBox_CTS4.AutoSize = true;
            this.checkBox_CTS4.Enabled = false;
            this.checkBox_CTS4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CTS4.Location = new System.Drawing.Point(217, 29);
            this.checkBox_CTS4.Name = "checkBox_CTS4";
            this.checkBox_CTS4.Size = new System.Drawing.Size(47, 17);
            this.checkBox_CTS4.TabIndex = 29;
            this.checkBox_CTS4.Text = "CTS";
            this.checkBox_CTS4.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(231, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 13);
            this.label26.TabIndex = 3;
            this.label26.Text = "Port 4 name";
            // 
            // checkBox_RTS4
            // 
            this.checkBox_RTS4.AutoSize = true;
            this.checkBox_RTS4.Enabled = false;
            this.checkBox_RTS4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RTS4.Location = new System.Drawing.Point(163, 29);
            this.checkBox_RTS4.Name = "checkBox_RTS4";
            this.checkBox_RTS4.Size = new System.Drawing.Size(48, 17);
            this.checkBox_RTS4.TabIndex = 28;
            this.checkBox_RTS4.Text = "RTS";
            this.checkBox_RTS4.UseVisualStyleBackColor = true;
            this.checkBox_RTS4.CheckedChanged += new System.EventHandler(this.checkBox_RTS4_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(79, 49);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(58, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "Port speed";
            // 
            // checkBox_RI4
            // 
            this.checkBox_RI4.AutoSize = true;
            this.checkBox_RI4.Enabled = false;
            this.checkBox_RI4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RI4.Location = new System.Drawing.Point(270, 29);
            this.checkBox_RI4.Name = "checkBox_RI4";
            this.checkBox_RI4.Size = new System.Drawing.Size(37, 17);
            this.checkBox_RI4.TabIndex = 30;
            this.checkBox_RI4.Text = "RI";
            this.checkBox_RI4.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(79, 89);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(33, 13);
            this.label28.TabIndex = 3;
            this.label28.Text = "Parity";
            // 
            // checkBox_DTR4
            // 
            this.checkBox_DTR4.AutoSize = true;
            this.checkBox_DTR4.Enabled = false;
            this.checkBox_DTR4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DTR4.Location = new System.Drawing.Point(108, 29);
            this.checkBox_DTR4.Name = "checkBox_DTR4";
            this.checkBox_DTR4.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DTR4.TabIndex = 27;
            this.checkBox_DTR4.Text = "DTR";
            this.checkBox_DTR4.UseVisualStyleBackColor = true;
            this.checkBox_DTR4.CheckedChanged += new System.EventHandler(this.checkBox_DTR4_CheckedChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(155, 49);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(64, 13);
            this.label29.TabIndex = 3;
            this.label29.Text = "Flow control";
            // 
            // checkBox_DSR4
            // 
            this.checkBox_DSR4.AutoSize = true;
            this.checkBox_DSR4.Enabled = false;
            this.checkBox_DSR4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DSR4.Location = new System.Drawing.Point(53, 29);
            this.checkBox_DSR4.Name = "checkBox_DSR4";
            this.checkBox_DSR4.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DSR4.TabIndex = 26;
            this.checkBox_DSR4.Text = "DSR";
            this.checkBox_DSR4.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(155, 89);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 13);
            this.label30.TabIndex = 3;
            this.label30.Text = "Stop bits";
            // 
            // checkBox_sendPort3
            // 
            this.checkBox_sendPort3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_sendPort3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox_sendPort3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_sendPort3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_sendPort3.Location = new System.Drawing.Point(594, 107);
            this.checkBox_sendPort3.Name = "checkBox_sendPort3";
            this.checkBox_sendPort3.Size = new System.Drawing.Size(76, 17);
            this.checkBox_sendPort3.TabIndex = 25;
            this.checkBox_sendPort3.Text = "Port3";
            this.checkBox_sendPort3.UseVisualStyleBackColor = true;
            this.checkBox_sendPort3.CheckedChanged += new System.EventHandler(this.checkBox_send_CheckedChanged);
            // 
            // checkBox_sendPort4
            // 
            this.checkBox_sendPort4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_sendPort4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox_sendPort4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBox_sendPort4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_sendPort4.Location = new System.Drawing.Point(660, 107);
            this.checkBox_sendPort4.Name = "checkBox_sendPort4";
            this.checkBox_sendPort4.Size = new System.Drawing.Size(76, 17);
            this.checkBox_sendPort4.TabIndex = 26;
            this.checkBox_sendPort4.Text = "Port4";
            this.checkBox_sendPort4.UseVisualStyleBackColor = true;
            this.checkBox_sendPort4.CheckedChanged += new System.EventHandler(this.checkBox_send_CheckedChanged);
            // 
            // checkBox_displayPort3hex
            // 
            this.checkBox_displayPort3hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_displayPort3hex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_displayPort3hex.Location = new System.Drawing.Point(642, 340);
            this.checkBox_displayPort3hex.Name = "checkBox_displayPort3hex";
            this.checkBox_displayPort3hex.Size = new System.Drawing.Size(76, 17);
            this.checkBox_displayPort3hex.TabIndex = 27;
            this.checkBox_displayPort3hex.Text = "Port 3";
            this.checkBox_displayPort3hex.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox_displayPort3hex.UseVisualStyleBackColor = true;
            // 
            // checkBox_displayPort4hex
            // 
            this.checkBox_displayPort4hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_displayPort4hex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_displayPort4hex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_displayPort4hex.Location = new System.Drawing.Point(642, 363);
            this.checkBox_displayPort4hex.Name = "checkBox_displayPort4hex";
            this.checkBox_displayPort4hex.Size = new System.Drawing.Size(76, 17);
            this.checkBox_displayPort4hex.TabIndex = 28;
            this.checkBox_displayPort4hex.Text = "Port 4";
            this.checkBox_displayPort4hex.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox_displayPort4hex.UseVisualStyleBackColor = true;
            // 
            // serialPort3
            // 
            this.serialPort3.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort3_ErrorReceived);
            // 
            // serialPort4
            // 
            this.serialPort4.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort4_ErrorReceived);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 562);
            this.Controls.Add(this.checkBox_sendPort4);
            this.Controls.Add(this.checkBox_sendPort3);
            this.Controls.Add(this.checkBox_sendPort2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.checkBox_suffhex);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBox_Mark);
            this.Controls.Add(this.textBox_suff);
            this.Controls.Add(this.checkBox_displayPort4hex);
            this.Controls.Add(this.checkBox_displayPort2hex);
            this.Controls.Add(this.checkBox_displayPort3hex);
            this.Controls.Add(this.checkBox_displayPort1hex);
            this.Controls.Add(this.button_clear1);
            this.Controls.Add(this.checkBox_lf);
            this.Controls.Add(this.checkBox_suff);
            this.Controls.Add(this.checkBox_cr);
            this.Controls.Add(this.checkBox_paramhex);
            this.Controls.Add(this.checkBox_commandhex);
            this.Controls.Add(this.checkBox_sendPort1);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_dispHex);
            this.Controls.Add(this.label_custom_command);
            this.Controls.Add(this.button_closeport);
            this.Controls.Add(this.button_openport);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_params);
            this.Controls.Add(this.textBox_senddata);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(740, 510);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RS232 monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_portname1;
        private System.Windows.Forms.ComboBox comboBox_portspeed1;
        private System.Windows.Forms.ComboBox comboBox_handshake1;
        private System.Windows.Forms.ComboBox comboBox_databits1;
        private System.Windows.Forms.ComboBox comboBox_parity1;
        private System.Windows.Forms.ComboBox comboBox_stopbits1;
        private System.Windows.Forms.TextBox textBox_params;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_openport;
        private System.Windows.Forms.Button button_closeport;
        private System.Windows.Forms.TextBox textBox_command;
        private System.Windows.Forms.Label label_custom_command;
        private System.Windows.Forms.ComboBox comboBox_portname2;
        private System.Windows.Forms.ComboBox comboBox_portspeed2;
        private System.Windows.Forms.ComboBox comboBox_handshake2;
        private System.Windows.Forms.ComboBox comboBox_databits2;
        private System.Windows.Forms.ComboBox comboBox_parity2;
        private System.Windows.Forms.ComboBox comboBox_stopbits2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBox_sendPort1;
        private System.Windows.Forms.CheckBox checkBox_sendPort2;
        private System.Windows.Forms.CheckBox checkBox_cr;
        private System.Windows.Forms.CheckBox checkBox_lf;
        private System.Windows.Forms.CheckBox checkBox_suff;
        private System.Windows.Forms.TextBox textBox_senddata;
        private System.Windows.Forms.CheckBox checkBox_commandhex;
        private System.Windows.Forms.CheckBox checkBox_paramhex;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_clear1;
        public System.Windows.Forms.TextBox textBox_terminal1;
        public System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.CheckBox checkBox_CD1;
        private System.Windows.Forms.CheckBox checkBox_DSR1;
        private System.Windows.Forms.CheckBox checkBox_DTR1;
        private System.Windows.Forms.CheckBox checkBox_RTS1;
        private System.Windows.Forms.CheckBox checkBox_CTS1;
        private System.Windows.Forms.CheckBox checkBox_CD2;
        private System.Windows.Forms.CheckBox checkBox_DSR2;
        private System.Windows.Forms.CheckBox checkBox_DTR2;
        private System.Windows.Forms.CheckBox checkBox_RTS2;
        private System.Windows.Forms.CheckBox checkBox_CTS2;
        private System.Windows.Forms.CheckBox checkBox_insPin;
        private System.Windows.Forms.CheckBox checkBox_insTime;
        private System.Windows.Forms.CheckBox checkBox_displayPort1hex;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox textBox_suff;
        private System.Windows.Forms.CheckBox checkBox_insDir;
        private System.Windows.Forms.CheckBox checkBox_displayPort2hex;
        private System.Windows.Forms.Label label_dispHex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox_RI1;
        private System.Windows.Forms.CheckBox checkBox_RI2;
        public System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.CheckBox checkBox_Mark;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.DataGridView dataGridView;
        private CheckBox checkBox_portName;
        private TextBox textBox_port1Name;
        private TextBox textBox_port2Name;
        private Label label9;
        private Label label16;
        private Label label17;
        private Label label18;
        private GroupBox groupBox1;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveTXTToolStripMenuItem;
        private ToolStripMenuItem saveCSVToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem logToGridToolStripMenuItem;
        private ToolStripMenuItem autoscrollToolStripMenuItem;
        private ToolStripMenuItem lineWrapToolStripMenuItem;
        private ToolStripMenuItem autosaveTXTToolStripMenuItem1;
        private ToolStripMenuItem autosaveCSVToolStripMenuItem1;
        private ToolStripMenuItem saveParametersToolStripMenuItem1;
        private ToolStripTextBox terminaltxtToolStripMenuItem1;
        private CheckBox checkBox_suffhex;
        private Button button_refresh;
        private ToolStripMenuItem LineBreakToolStripMenuItem1;
        private ToolStripTextBox LineBreakToolStripTextBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private CheckBox checkBox_CD3;
        private ComboBox comboBox_portname3;
        private ComboBox comboBox_portspeed3;
        private TextBox textBox_port3Name;
        private ComboBox comboBox_handshake3;
        private ComboBox comboBox_databits3;
        private ComboBox comboBox_parity3;
        private ComboBox comboBox_stopbits3;
        private Label label1;
        private Label label7;
        private CheckBox checkBox_CTS3;
        private Label label19;
        private CheckBox checkBox_RTS3;
        private Label label20;
        private CheckBox checkBox_RI3;
        private Label label21;
        private CheckBox checkBox_DTR3;
        private Label label22;
        private CheckBox checkBox_DSR3;
        private Label label23;
        private GroupBox groupBox5;
        private CheckBox checkBox_CD4;
        private ComboBox comboBox_portname4;
        private ComboBox comboBox_portspeed4;
        private TextBox textBox_port4Name;
        private ComboBox comboBox_handshake4;
        private ComboBox comboBox_databits4;
        private ComboBox comboBox_parity4;
        private ComboBox comboBox_stopbits4;
        private Label label24;
        private Label label25;
        private CheckBox checkBox_CTS4;
        private Label label26;
        private CheckBox checkBox_RTS4;
        private Label label27;
        private CheckBox checkBox_RI4;
        private Label label28;
        private CheckBox checkBox_DTR4;
        private Label label29;
        private CheckBox checkBox_DSR4;
        private Label label30;
        private CheckBox checkBox_sendPort3;
        private CheckBox checkBox_sendPort4;
        private CheckBox checkBox_displayPort3hex;
        private CheckBox checkBox_displayPort4hex;
        private SerialPort serialPort3;
        private SerialPort serialPort4;
        private ToolStripMenuItem logToTextToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_onlyData;
        private ToolStripMenuItem limitCSVLinesNumberToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox_CSVLinesNumber;
    }
}