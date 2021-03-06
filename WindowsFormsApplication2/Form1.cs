﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using RS232_monitor.Properties;

namespace RS232_monitor
{
    public partial class FormMain : Form
    {
        /*Codepages list https://msdn.microsoft.com/en-us/library/system.text.encoding(v=vs.110).aspx
        const int inputCodePage = RS232_monitor.Properties.Settings.Default.CodePage;*/

        private bool o_cd1, o_dsr1, o_dtr1, o_rts1, o_cts1;
        private bool o_cd2, o_dsr2, o_dtr2, o_rts2, o_cts2;
        private bool o_cd3, o_dsr3, o_dtr3, o_rts3, o_cts3;
        private bool o_cd4, o_dsr4, o_dtr4, o_rts4, o_cts4;
        private DataTable CSVdataTable = new DataTable("Logs");
        private string portname1, portname2, portname3, portname4;
        private int txtOutState;
        private long oldTicks = DateTime.Now.Ticks, limitTick;
        private int CSVLineNumberLimit;
        private string CSVFileName = "";
        private int CSVLineCount;
        private int LogLinesLimit = 100;

        private const byte Port1DataIn = 11;
        private const byte Port1DataOut = 12;
        private const byte Port1SignalIn = 13;
        private const byte Port1SignalOut = 14;
        private const byte Port1Error = 15;

        private const byte Port2DataIn = 21;
        private const byte Port2DataOut = 22;
        private const byte Port2SignalIn = 23;
        private const byte Port2SignalOut = 24;
        private const byte Port2Error = 25;

        private const byte Port3DataIn = 31;
        private const byte Port3DataOut = 32;
        private const byte Port3SignalIn = 33;
        private const byte Port3SignalOut = 34;
        private const byte Port3Error = 35;

        private const byte Port4DataIn = 41;
        private const byte Port4DataOut = 42;
        private const byte Port4SignalIn = 43;
        private const byte Port4SignalOut = 44;
        private const byte Port4Error = 45;

        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripTextBox_CSVLinesNumber.LostFocus += ToolStripTextBox_CSVLinesNumber_Leave;
            LineBreakToolStripTextBox1.LostFocus += LineBreakToolStripTextBox1_Leave;

            dataGridView.DataSource = CSVdataTable;
            //create columns
            var colDate = new DataColumn("Date", typeof(string));
            var colTime = new DataColumn("Time", typeof(string));
            var colMilis = new DataColumn("Milis", typeof(string));
            var colPort = new DataColumn("Port", typeof(string));
            var colDir = new DataColumn("Dir", typeof(string));
            var colData = new DataColumn("Data", typeof(string));
            var colSig = new DataColumn("Signal", typeof(string));
            var colMark = new DataColumn("Mark", typeof(bool));
            //add columns to the table
            CSVdataTable.Columns.AddRange(new[]
                {colDate, colTime, colMilis, colPort, colDir, colData, colSig, colMark});

            var column = dataGridView.Columns[0];
            //column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 70;
            column.Width = 70;

            column = dataGridView.Columns[1];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 55;
            column.Width = 55;

            column = dataGridView.Columns[2];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            column = dataGridView.Columns[3];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 40;

            column = dataGridView.Columns[4];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            column = dataGridView.Columns[5];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 200;
            column.Width = 250;

            column = dataGridView.Columns[6];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 60;
            column.Width = 60;

            column = dataGridView.Columns[7];
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            //load settings
            textBox_command.Text = Settings.Default.DefaultCommand;
            checkBox_commandhex.Checked = Settings.Default.DefaultCommandHex;
            textBox_params.Text = Settings.Default.DefaultParameter;
            checkBox_paramhex.Checked = Settings.Default.DefaultParamHex;
            checkBox_cr.Checked = Settings.Default.addCR;
            checkBox_lf.Checked = Settings.Default.addLF;
            checkBox_suff.Checked = Settings.Default.addSuff;
            textBox_suff.Text = Settings.Default.SuffText;
            checkBox_suffhex.Checked = Settings.Default.DefaultSuffHex;
            checkBox_insPin.Checked = Settings.Default.LogSignal;
            checkBox_insTime.Checked = Settings.Default.LogTime;
            checkBox_insDir.Checked = Settings.Default.LogDir;
            checkBox_portName.Checked = Settings.Default.LogPortName;
            checkBox_displayPort1hex.Checked = Settings.Default.HexPort1;
            checkBox_displayPort2hex.Checked = Settings.Default.HexPort2;
            checkBox_displayPort3hex.Checked = Settings.Default.HexPort3;
            checkBox_displayPort4hex.Checked = Settings.Default.HexPort4;
            textBox_port1Name.Text = Settings.Default.Port1Name;
            textBox_port2Name.Text = Settings.Default.Port2Name;
            textBox_port3Name.Text = Settings.Default.Port3Name;
            textBox_port4Name.Text = Settings.Default.Port4Name;
            logToGridToolStripMenuItem.Checked = Settings.Default.LogGrid;
            autoscrollToolStripMenuItem.Checked = Settings.Default.AutoScroll;
            lineWrapToolStripMenuItem.Checked = Settings.Default.LineWrap;
            autosaveTXTToolStripMenuItem1.Checked = Settings.Default.AutoLogTXT;
            terminaltxtToolStripMenuItem1.Text = Settings.Default.TXTlogFile;
            autosaveCSVToolStripMenuItem1.Checked = Settings.Default.AutoLogCSV;
            LineBreakToolStripTextBox1.Text = Settings.Default.LineBreakTimeout.ToString();
            limitTick = Settings.Default.LineBreakTimeout * 10000;
            CSVLineNumberLimit = Settings.Default.CSVLineNumber;
            toolStripTextBox_CSVLinesNumber.Text = CSVLineNumberLimit.ToString();
            LogLinesLimit = Settings.Default.LogLinesLimit;

            terminaltxtToolStripMenuItem1.Enabled = autosaveTXTToolStripMenuItem1.Checked;

            //set the codepage to COM-port
            serialPort1.Encoding = Encoding.GetEncoding(Settings.Default.CodePage);
            serialPort2.Encoding = Encoding.GetEncoding(Settings.Default.CodePage);
            serialPort3.Encoding = Encoding.GetEncoding(Settings.Default.CodePage);
            serialPort4.Encoding = Encoding.GetEncoding(Settings.Default.CodePage);
            SerialPopulate();
        }

        private void ToolStripTextBox_CSVLinesNumber_LostFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_openport_Click(object sender, EventArgs e)
        {
            checkBox_DTR1.Checked = false;
            checkBox_DTR2.Checked = false;
            checkBox_DTR3.Checked = false;
            checkBox_DTR4.Checked = false;
            checkBox_RTS1.Checked = false;
            checkBox_RTS2.Checked = false;
            checkBox_RTS3.Checked = false;
            checkBox_RTS4.Checked = false;
            CSVFileName = DateTime.Today.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString() + "_" +
                          DateTime.Now.Millisecond.ToString("D3") + ".csv";
            CSVFileName = CSVFileName.Replace(':', '-').Replace('\\', '-').Replace('/', '-');
            CSVLineCount = 0;
            if (comboBox_portname1.SelectedIndex != 0)
            {
                comboBox_portname1.Enabled = false;
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;

                comboBox_portname2.Enabled = false;
                comboBox_portspeed2.Enabled = false;
                comboBox_handshake2.Enabled = false;
                comboBox_databits2.Enabled = false;
                comboBox_parity2.Enabled = false;
                comboBox_stopbits2.Enabled = false;

                comboBox_portname3.Enabled = false;
                comboBox_portspeed3.Enabled = false;
                comboBox_handshake3.Enabled = false;
                comboBox_databits3.Enabled = false;
                comboBox_parity3.Enabled = false;
                comboBox_stopbits3.Enabled = false;

                comboBox_portname4.Enabled = false;
                comboBox_portspeed4.Enabled = false;
                comboBox_handshake4.Enabled = false;
                comboBox_databits4.Enabled = false;
                comboBox_parity4.Enabled = false;
                comboBox_stopbits4.Enabled = false;

                serialPort1.PortName = comboBox_portname1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox_portspeed1.Text);
                serialPort1.DataBits = Convert.ToUInt16(comboBox_databits1.Text);
                serialPort1.Handshake = (Handshake) Enum.Parse(typeof(Handshake), comboBox_handshake1.Text);
                serialPort1.Parity = (Parity) Enum.Parse(typeof(Parity), comboBox_parity1.Text);
                serialPort1.StopBits = (StopBits) Enum.Parse(typeof(StopBits), comboBox_stopbits1.Text);
                serialPort1.ReadTimeout = Settings.Default.ReceiveTimeOut;
                serialPort1.WriteTimeout = Settings.Default.SendTimeOut;
                serialPort1.ReadBufferSize = 8192;
                try
                {
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port " + serialPort1.PortName + ": " + ex.Message);
                    comboBox_portname1.Enabled = true;
                    comboBox_portspeed1.Enabled = true;
                    comboBox_handshake1.Enabled = true;
                    comboBox_databits1.Enabled = true;
                    comboBox_parity1.Enabled = true;
                    comboBox_stopbits1.Enabled = true;

                    comboBox_portname2.Enabled = true;
                    comboBox_portspeed2.Enabled = true;
                    comboBox_handshake2.Enabled = true;
                    comboBox_databits2.Enabled = true;
                    comboBox_parity2.Enabled = true;
                    comboBox_stopbits2.Enabled = true;

                    comboBox_portname3.Enabled = true;
                    comboBox_portspeed3.Enabled = true;
                    comboBox_handshake3.Enabled = true;
                    comboBox_databits3.Enabled = true;
                    comboBox_parity3.Enabled = true;
                    comboBox_stopbits3.Enabled = true;

                    comboBox_portname4.Enabled = true;
                    comboBox_portspeed4.Enabled = true;
                    comboBox_handshake4.Enabled = true;
                    comboBox_databits4.Enabled = true;
                    comboBox_parity4.Enabled = true;
                    comboBox_stopbits4.Enabled = true;

                    return;
                }

                if (checkBox_insPin.Checked) serialPort1.PinChanged += SerialPort1_PinChanged;
                serialPort1.DataReceived += SerialPort1_DataReceived;
                button_refresh.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                o_cd1 = serialPort1.CDHolding;
                checkBox_CD1.Checked = o_cd1;
                o_dsr1 = serialPort1.DsrHolding;
                checkBox_DSR1.Checked = o_dsr1;
                o_dtr1 = serialPort1.DtrEnable;
                checkBox_DTR1.Checked = o_dtr1;
                o_cts1 = serialPort1.CtsHolding;
                checkBox_CTS1.Checked = o_cts1;
                checkBox_DTR1.Enabled = true;

                if (serialPort1.Handshake == Handshake.RequestToSend ||
                    serialPort1.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS1.Enabled = false;
                }
                else
                {
                    o_rts1 = serialPort1.RtsEnable;
                    checkBox_RTS1.Checked = o_rts1;
                    checkBox_RTS1.Enabled = true;
                }
            }

            if (comboBox_portname2.SelectedIndex != 0)
            {
                comboBox_portname1.Enabled = false;
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;

                comboBox_portname2.Enabled = false;
                comboBox_portspeed2.Enabled = false;
                comboBox_handshake2.Enabled = false;
                comboBox_databits2.Enabled = false;
                comboBox_parity2.Enabled = false;
                comboBox_stopbits2.Enabled = false;

                comboBox_portname3.Enabled = false;
                comboBox_portspeed3.Enabled = false;
                comboBox_handshake3.Enabled = false;
                comboBox_databits3.Enabled = false;
                comboBox_parity3.Enabled = false;
                comboBox_stopbits3.Enabled = false;

                comboBox_portname4.Enabled = false;
                comboBox_portspeed4.Enabled = false;
                comboBox_handshake4.Enabled = false;
                comboBox_databits4.Enabled = false;
                comboBox_parity4.Enabled = false;
                comboBox_stopbits4.Enabled = false;

                serialPort2.PortName = comboBox_portname2.Text;
                serialPort2.BaudRate = Convert.ToInt32(comboBox_portspeed2.Text);
                serialPort2.DataBits = Convert.ToUInt16(serialPort2.DataBits);
                serialPort2.Handshake = (Handshake) Enum.Parse(typeof(Handshake), comboBox_handshake2.Text);
                serialPort2.Parity = (Parity) Enum.Parse(typeof(Parity), comboBox_parity2.Text);
                serialPort2.StopBits = (StopBits) Enum.Parse(typeof(StopBits), comboBox_stopbits2.Text);
                serialPort2.ReadTimeout = Settings.Default.ReceiveTimeOut;
                serialPort2.WriteTimeout = Settings.Default.SendTimeOut;
                serialPort2.ReadBufferSize = 8192;
                try
                {
                    serialPort2.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port " + serialPort2.PortName + ": " + ex.Message);
                    comboBox_portname1.Enabled = true;
                    comboBox_portspeed1.Enabled = true;
                    comboBox_handshake1.Enabled = true;
                    comboBox_databits1.Enabled = true;
                    comboBox_parity1.Enabled = true;
                    comboBox_stopbits1.Enabled = true;

                    comboBox_portname2.Enabled = true;
                    comboBox_portspeed2.Enabled = true;
                    comboBox_handshake2.Enabled = true;
                    comboBox_databits2.Enabled = true;
                    comboBox_parity2.Enabled = true;
                    comboBox_stopbits2.Enabled = true;

                    comboBox_portname3.Enabled = true;
                    comboBox_portspeed3.Enabled = true;
                    comboBox_handshake3.Enabled = true;
                    comboBox_databits3.Enabled = true;
                    comboBox_parity3.Enabled = true;
                    comboBox_stopbits3.Enabled = true;

                    comboBox_portname4.Enabled = true;
                    comboBox_portspeed4.Enabled = true;
                    comboBox_handshake4.Enabled = true;
                    comboBox_databits4.Enabled = true;
                    comboBox_parity4.Enabled = true;
                    comboBox_stopbits4.Enabled = true;
                    return;
                }

                if (checkBox_insPin.Checked) serialPort2.PinChanged += SerialPort2_PinChanged;
                serialPort2.DataReceived += SerialPort2_DataReceived;
                button_refresh.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                o_cd2 = serialPort2.CDHolding;
                checkBox_CD2.Checked = o_cd2;
                o_dsr2 = serialPort2.DsrHolding;
                checkBox_DSR2.Checked = o_dsr2;
                o_dtr2 = serialPort2.DtrEnable;
                checkBox_DTR2.Checked = o_dtr2;
                o_cts2 = serialPort2.CtsHolding;
                checkBox_CTS2.Checked = o_cts2;
                checkBox_DTR2.Enabled = true;
                if (serialPort2.Handshake == Handshake.RequestToSend ||
                    serialPort2.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS2.Enabled = false;
                }
                else
                {
                    o_rts2 = serialPort2.RtsEnable;
                    checkBox_RTS2.Checked = o_rts2;
                    checkBox_RTS2.Enabled = true;
                }
            }

            if (comboBox_portname3.SelectedIndex != 0)
            {
                comboBox_portname1.Enabled = false;
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;

                comboBox_portname2.Enabled = false;
                comboBox_portspeed2.Enabled = false;
                comboBox_handshake2.Enabled = false;
                comboBox_databits2.Enabled = false;
                comboBox_parity2.Enabled = false;
                comboBox_stopbits2.Enabled = false;

                comboBox_portname3.Enabled = false;
                comboBox_portspeed3.Enabled = false;
                comboBox_handshake3.Enabled = false;
                comboBox_databits3.Enabled = false;
                comboBox_parity3.Enabled = false;
                comboBox_stopbits3.Enabled = false;

                comboBox_portname4.Enabled = false;
                comboBox_portspeed4.Enabled = false;
                comboBox_handshake4.Enabled = false;
                comboBox_databits4.Enabled = false;
                comboBox_parity4.Enabled = false;
                comboBox_stopbits4.Enabled = false;

                serialPort3.PortName = comboBox_portname3.Text;
                serialPort3.BaudRate = Convert.ToInt32(comboBox_portspeed3.Text);
                serialPort3.DataBits = Convert.ToUInt16(serialPort3.DataBits);
                serialPort3.Handshake = (Handshake) Enum.Parse(typeof(Handshake), comboBox_handshake3.Text);
                serialPort3.Parity = (Parity) Enum.Parse(typeof(Parity), comboBox_parity3.Text);
                serialPort3.StopBits = (StopBits) Enum.Parse(typeof(StopBits), comboBox_stopbits3.Text);
                serialPort3.ReadTimeout = Settings.Default.ReceiveTimeOut;
                serialPort3.WriteTimeout = Settings.Default.SendTimeOut;
                serialPort3.ReadBufferSize = 8192;
                try
                {
                    serialPort3.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port " + serialPort3.PortName + ": " + ex.Message);
                    comboBox_portname1.Enabled = true;
                    comboBox_portspeed1.Enabled = true;
                    comboBox_handshake1.Enabled = true;
                    comboBox_databits1.Enabled = true;
                    comboBox_parity1.Enabled = true;
                    comboBox_stopbits1.Enabled = true;

                    comboBox_portname2.Enabled = true;
                    comboBox_portspeed2.Enabled = true;
                    comboBox_handshake2.Enabled = true;
                    comboBox_databits2.Enabled = true;
                    comboBox_parity2.Enabled = true;
                    comboBox_stopbits2.Enabled = true;

                    comboBox_portname3.Enabled = true;
                    comboBox_portspeed3.Enabled = true;
                    comboBox_handshake3.Enabled = true;
                    comboBox_databits3.Enabled = true;
                    comboBox_parity3.Enabled = true;
                    comboBox_stopbits3.Enabled = true;

                    comboBox_portname4.Enabled = true;
                    comboBox_portspeed4.Enabled = true;
                    comboBox_handshake4.Enabled = true;
                    comboBox_databits4.Enabled = true;
                    comboBox_parity4.Enabled = true;
                    comboBox_stopbits4.Enabled = true;
                    return;
                }

                if (checkBox_insPin.Checked) serialPort3.PinChanged += SerialPort3_PinChanged;
                serialPort3.DataReceived += SerialPort3_DataReceived;
                button_refresh.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                o_cd3 = serialPort3.CDHolding;
                checkBox_CD3.Checked = o_cd3;
                o_dsr3 = serialPort3.DsrHolding;
                checkBox_DSR3.Checked = o_dsr3;
                o_dtr3 = serialPort3.DtrEnable;
                checkBox_DTR3.Checked = o_dtr3;
                o_cts3 = serialPort3.CtsHolding;
                checkBox_CTS3.Checked = o_cts3;
                checkBox_DTR3.Enabled = true;
                if (serialPort3.Handshake == Handshake.RequestToSend ||
                    serialPort3.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS3.Enabled = false;
                }
                else
                {
                    o_rts3 = serialPort3.RtsEnable;
                    checkBox_RTS3.Checked = o_rts3;
                    checkBox_RTS3.Enabled = true;
                }
            }

            if (comboBox_portname4.SelectedIndex != 0)
            {
                comboBox_portname1.Enabled = false;
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;

                comboBox_portname2.Enabled = false;
                comboBox_portspeed2.Enabled = false;
                comboBox_handshake2.Enabled = false;
                comboBox_databits2.Enabled = false;
                comboBox_parity2.Enabled = false;
                comboBox_stopbits2.Enabled = false;

                comboBox_portname3.Enabled = false;
                comboBox_portspeed3.Enabled = false;
                comboBox_handshake3.Enabled = false;
                comboBox_databits3.Enabled = false;
                comboBox_parity3.Enabled = false;
                comboBox_stopbits3.Enabled = false;

                comboBox_portname4.Enabled = false;
                comboBox_portspeed4.Enabled = false;
                comboBox_handshake4.Enabled = false;
                comboBox_databits4.Enabled = false;
                comboBox_parity4.Enabled = false;
                comboBox_stopbits4.Enabled = false;

                serialPort4.PortName = comboBox_portname4.Text;
                serialPort4.BaudRate = Convert.ToInt32(comboBox_portspeed4.Text);
                serialPort4.DataBits = Convert.ToUInt16(serialPort4.DataBits);
                serialPort4.Handshake = (Handshake) Enum.Parse(typeof(Handshake), comboBox_handshake4.Text);
                serialPort4.Parity = (Parity) Enum.Parse(typeof(Parity), comboBox_parity4.Text);
                serialPort4.StopBits = (StopBits) Enum.Parse(typeof(StopBits), comboBox_stopbits4.Text);
                serialPort4.ReadTimeout = Settings.Default.ReceiveTimeOut;
                serialPort4.WriteTimeout = Settings.Default.SendTimeOut;
                serialPort4.ReadBufferSize = 8192;
                try
                {
                    serialPort4.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port " + serialPort4.PortName + ": " + ex.Message);
                    comboBox_portname1.Enabled = true;
                    comboBox_portspeed1.Enabled = true;
                    comboBox_handshake1.Enabled = true;
                    comboBox_databits1.Enabled = true;
                    comboBox_parity1.Enabled = true;
                    comboBox_stopbits1.Enabled = true;

                    comboBox_portname2.Enabled = true;
                    comboBox_portspeed2.Enabled = true;
                    comboBox_handshake2.Enabled = true;
                    comboBox_databits2.Enabled = true;
                    comboBox_parity2.Enabled = true;
                    comboBox_stopbits2.Enabled = true;

                    comboBox_portname3.Enabled = true;
                    comboBox_portspeed3.Enabled = true;
                    comboBox_handshake3.Enabled = true;
                    comboBox_databits3.Enabled = true;
                    comboBox_parity3.Enabled = true;
                    comboBox_stopbits3.Enabled = true;

                    comboBox_portname4.Enabled = true;
                    comboBox_portspeed4.Enabled = true;
                    comboBox_handshake4.Enabled = true;
                    comboBox_databits4.Enabled = true;
                    comboBox_parity4.Enabled = true;
                    comboBox_stopbits4.Enabled = true;
                    return;
                }

                if (checkBox_insPin.Checked) serialPort4.PinChanged += SerialPort4_PinChanged;
                serialPort4.DataReceived += SerialPort4_DataReceived;
                button_refresh.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                o_cd4 = serialPort4.CDHolding;
                checkBox_CD4.Checked = o_cd4;
                o_dsr4 = serialPort4.DsrHolding;
                checkBox_DSR4.Checked = o_dsr4;
                o_dtr4 = serialPort4.DtrEnable;
                checkBox_DTR4.Checked = o_dtr4;
                o_cts4 = serialPort4.CtsHolding;
                checkBox_CTS4.Checked = o_cts4;
                checkBox_DTR4.Enabled = true;
                if (serialPort4.Handshake == Handshake.RequestToSend ||
                    serialPort4.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS4.Enabled = false;
                }
                else
                {
                    o_rts4 = serialPort4.RtsEnable;
                    checkBox_RTS4.Checked = o_rts4;
                    checkBox_RTS4.Enabled = true;
                }
            }

            if (checkBox_sendPort1.Checked == false && checkBox_sendPort2.Checked == false &&
                checkBox_sendPort3.Checked == false && checkBox_sendPort4.Checked == false) button_send.Enabled = false;
            else if (serialPort1.IsOpen || serialPort2.IsOpen || serialPort3.IsOpen || serialPort4.IsOpen)
                button_send.Enabled = true;
            CheckBox_portName_CheckedChanged(this, EventArgs.Empty);
        }

        private void Button_closeport_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort1.PortName + ": " + ex.Message);
            }

            try
            {
                serialPort2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort2.PortName + ": " + ex.Message);
            }

            try
            {
                serialPort3.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort3.PortName + ": " + ex.Message);
            }

            try
            {
                serialPort4.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort4.PortName + ": " + ex.Message);
            }

            serialPort1.DataReceived -= SerialPort1_DataReceived;
            serialPort1.PinChanged -= SerialPort1_PinChanged;
            serialPort2.DataReceived -= SerialPort2_DataReceived;
            serialPort2.PinChanged -= SerialPort2_PinChanged;
            serialPort3.DataReceived -= SerialPort3_DataReceived;
            serialPort3.PinChanged -= SerialPort3_PinChanged;
            serialPort4.DataReceived -= SerialPort4_DataReceived;
            serialPort4.PinChanged -= SerialPort4_PinChanged;

            comboBox_portname1.Enabled = true;
            comboBox_portspeed1.Enabled = true;
            comboBox_handshake1.Enabled = true;
            comboBox_databits1.Enabled = true;
            comboBox_parity1.Enabled = true;
            comboBox_stopbits1.Enabled = true;

            comboBox_portname2.Enabled = true;
            comboBox_portspeed2.Enabled = true;
            comboBox_handshake2.Enabled = true;
            comboBox_databits2.Enabled = true;
            comboBox_parity2.Enabled = true;
            comboBox_stopbits2.Enabled = true;

            comboBox_portname3.Enabled = true;
            comboBox_portspeed3.Enabled = true;
            comboBox_handshake3.Enabled = true;
            comboBox_databits3.Enabled = true;
            comboBox_parity3.Enabled = true;
            comboBox_stopbits3.Enabled = true;

            comboBox_portname4.Enabled = true;
            comboBox_portspeed4.Enabled = true;
            comboBox_handshake4.Enabled = true;
            comboBox_databits4.Enabled = true;
            comboBox_parity4.Enabled = true;
            comboBox_stopbits4.Enabled = true;

            button_send.Enabled = false;
            button_refresh.Enabled = true;
            button_openport.Enabled = true;
            button_closeport.Enabled = false;

            checkBox_DTR1.Enabled = false;
            checkBox_RTS1.Enabled = false;

            checkBox_DTR2.Enabled = false;
            checkBox_RTS2.Enabled = false;

            checkBox_DTR3.Enabled = false;
            checkBox_RTS3.Enabled = false;

            checkBox_DTR4.Enabled = false;
            checkBox_RTS4.Enabled = false;
        }

        private void Button_send_Click(object sender, EventArgs e)
        {
            if (textBox_senddata.Text != "")
            {
                var outStr = "";
                if (checkBox_sendPort1.Checked && serialPort1.IsOpen)
                {
                    DataRow dataRowTX1 = null;
                    //create new row in datatable
                    dataRowTX1 = CSVdataTable.NewRow();
                    if (checkBox_insTime.Checked)
                    {
                        dataRowTX1["Date"] = DateTime.Today.ToShortDateString();
                        dataRowTX1["Time"] = DateTime.Now.ToLongTimeString();
                        dataRowTX1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    }

                    if (checkBox_insDir.Checked)
                    {
                        dataRowTX1["Port"] = portname1;
                        dataRowTX1["Dir"] = "TX";
                    }

                    dataRowTX1["Mark"] = checkBox_Mark.Checked;
                    try
                    {
                        serialPort1.Write(Accessory.ConvertHexToByteArray(textBox_senddata.Text), 0,
                            textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                        dataRowTX1["Signal"] = "Error sending to port " + serialPort1.PortName + ": " + ex.Message;
                    }

                    dataRowTX1["Data"] = textBox_senddata.Text;
                    if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowTX1);
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX1["Date"] + " " + dataRowTX1["Time"] + "." + dataRowTX1["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname1 + ">> ";
                    if (checkBox_displayPort1hex.Checked) outStr += textBox_senddata.Text;
                    else outStr += Accessory.ConvertHexToString(textBox_senddata.Text);
                    if (outStr != "")
                    {
                        CollectBuffer(outStr, Port1DataOut,
                            dataRowTX1["Date"] + " " + dataRowTX1["Time"] + "." + dataRowTX1["Milis"]);
                        if (autosaveCSVToolStripMenuItem1.Checked && dataRowTX1["Data"].ToString() != "")
                            CSVcollectBuffer(dataRowTX1["Date"] + "," + dataRowTX1["Time"] + "," + dataRowTX1["Milis"] +
                                             "," + dataRowTX1["Port"] + "," + dataRowTX1["Dir"] + "," +
                                             dataRowTX1["Data"] + "," + dataRowTX1["Signal"] + "," +
                                             dataRowTX1["Mark"] + "\r\n");
                    }
                }

                if (checkBox_sendPort2.Checked && serialPort2.IsOpen)
                {
                    DataRow dataRowTX2 = null;
                    //создаём новую строку
                    dataRowTX2 = CSVdataTable.NewRow();
                    if (checkBox_insTime.Checked)
                    {
                        dataRowTX2["Date"] = DateTime.Today.ToShortDateString();
                        dataRowTX2["Time"] = DateTime.Now.ToLongTimeString();
                        dataRowTX2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    }

                    if (checkBox_insDir.Checked)
                    {
                        dataRowTX2["Port"] = portname2;
                        dataRowTX2["Dir"] = "TX";
                    }

                    dataRowTX2["Mark"] = checkBox_Mark.Checked;
                    try
                    {
                        serialPort2.Write(Accessory.ConvertHexToByteArray(textBox_senddata.Text), 0,
                            textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX2["Signal"] = "Error sending to port " + serialPort2.PortName + ": " + ex.Message;
                    }

                    dataRowTX2["Data"] = textBox_senddata.Text;
                    if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowTX2);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort2hex.Checked) outStr += textBox_senddata.Text;
                    else outStr += Accessory.ConvertHexToString(textBox_senddata.Text);
                    if (outStr != "")
                    {
                        CollectBuffer(outStr, Port2DataOut,
                            dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"]);
                        if (autosaveCSVToolStripMenuItem1.Checked)
                            CSVcollectBuffer(dataRowTX2["Date"] + "," + dataRowTX2["Time"] + "," + dataRowTX2["Milis"] +
                                             "," + dataRowTX2["Port"] + "," + dataRowTX2["Dir"] + "," +
                                             dataRowTX2["Data"] + "," + dataRowTX2["Signal"] + "," +
                                             dataRowTX2["Mark"] + "\r\n");
                    }
                }

                if (checkBox_sendPort3.Checked && serialPort3.IsOpen)
                {
                    DataRow dataRowTX3 = null;
                    //создаём новую строку
                    dataRowTX3 = CSVdataTable.NewRow();
                    if (checkBox_insTime.Checked)
                    {
                        dataRowTX3["Date"] = DateTime.Today.ToShortDateString();
                        dataRowTX3["Time"] = DateTime.Now.ToLongTimeString();
                        dataRowTX3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    }

                    if (checkBox_insDir.Checked)
                    {
                        dataRowTX3["Port"] = portname3;
                        dataRowTX3["Dir"] = "TX";
                    }

                    dataRowTX3["Mark"] = checkBox_Mark.Checked;
                    try
                    {
                        serialPort3.Write(Accessory.ConvertHexToByteArray(textBox_senddata.Text), 0,
                            textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX3["Signal"] = "Error sending to port " + serialPort3.PortName + ": " + ex.Message;
                    }

                    dataRowTX3["Data"] = textBox_senddata.Text;
                    if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowTX3);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort3hex.Checked) outStr += textBox_senddata.Text;
                    else outStr += Accessory.ConvertHexToString(textBox_senddata.Text);
                    if (outStr != "")
                    {
                        CollectBuffer(outStr, Port3DataOut,
                            dataRowTX3["Date"] + " " + dataRowTX3["Time"] + "." + dataRowTX3["Milis"]);
                        if (autosaveCSVToolStripMenuItem1.Checked)
                            CSVcollectBuffer(dataRowTX3["Date"] + "," + dataRowTX3["Time"] + "," + dataRowTX3["Milis"] +
                                             "," + dataRowTX3["Port"] + "," + dataRowTX3["Dir"] + "," +
                                             dataRowTX3["Data"] + "," + dataRowTX3["Signal"] + "," +
                                             dataRowTX3["Mark"] + "\r\n");
                    }
                }

                if (checkBox_sendPort4.Checked && serialPort4.IsOpen)
                {
                    DataRow dataRowTX4 = null;
                    //создаём новую строку
                    dataRowTX4 = CSVdataTable.NewRow();
                    if (checkBox_insTime.Checked)
                    {
                        dataRowTX4["Date"] = DateTime.Today.ToShortDateString();
                        dataRowTX4["Time"] = DateTime.Now.ToLongTimeString();
                        dataRowTX4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    }

                    if (checkBox_insDir.Checked)
                    {
                        dataRowTX4["Port"] = portname4;
                        dataRowTX4["Dir"] = "TX";
                    }

                    dataRowTX4["Mark"] = checkBox_Mark.Checked;
                    try
                    {
                        serialPort4.Write(Accessory.ConvertHexToByteArray(textBox_senddata.Text), 0,
                            textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX4["Signal"] = "Error sending to port " + serialPort4.PortName + ": " + ex.Message;
                    }

                    dataRowTX4["Data"] = textBox_senddata.Text;
                    if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowTX4);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort4hex.Checked) outStr += textBox_senddata.Text;
                    else outStr += Accessory.ConvertHexToString(textBox_senddata.Text);
                    if (outStr != "")
                    {
                        CollectBuffer(outStr, Port4DataOut,
                            dataRowTX4["Date"] + " " + dataRowTX4["Time"] + "." + dataRowTX4["Milis"]);
                        if (autosaveCSVToolStripMenuItem1.Checked)
                            CSVcollectBuffer(dataRowTX4["Date"] + "," + dataRowTX4["Time"] + "," + dataRowTX4["Milis"] +
                                             "," + dataRowTX4["Port"] + "," + dataRowTX4["Dir"] + "," +
                                             dataRowTX4["Data"] + "," + dataRowTX4["Signal"] + "," +
                                             dataRowTX4["Mark"] + "\r\n");
                    }
                }
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rx1 = new List<byte>();
            var dataRowRX1 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowRX1["Date"] = DateTime.Today.ToShortDateString();
                dataRowRX1["Time"] = DateTime.Now.ToLongTimeString();
                dataRowRX1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowRX1["Port"] = portname1;
                dataRowRX1["Dir"] = "RX";
            }

            dataRowRX1["Mark"] = checkBox_Mark.Checked;
            try
            {
                while (serialPort1.BytesToRead > 0) rx1.Add((byte) serialPort1.ReadByte());
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error reading port " + serialPort1.PortName + ": " + ex.Message);
                dataRowRX1["Signal"] = "Error reading port " + serialPort1.PortName + ": " + ex.Message;
            }

            dataRowRX1["Data"] = Accessory.ConvertByteArrayToHex(rx1.ToArray());
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowRX1);
            var outStr1 = "";
            //if (checkBox_insTime.Checked == true) outStr1 += dataRowRX1["Date"] + " " + dataRowRX1["Time"] + "." + dataRowRX1["Milis"] + " ";
            //if (checkBox_insDir.Checked == true) outStr1 += portname1 + "<< ";
            if (checkBox_displayPort1hex.Checked) outStr1 += dataRowRX1["Data"];
            else outStr1 += Encoding.GetEncoding(Settings.Default.CodePage).GetString(rx1.ToArray(), 0, rx1.Count);
            CollectBuffer(outStr1, Port1DataIn,
                dataRowRX1["Date"] + " " + dataRowRX1["Time"] + "." + dataRowRX1["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowRX1["Date"] + "," + dataRowRX1["Time"] + "," + dataRowRX1["Milis"] + "," +
                                 dataRowRX1["Port"] + "," + dataRowRX1["Dir"] + "," + dataRowRX1["Data"] + "," +
                                 dataRowRX1["Signal"] + "," + dataRowRX1["Mark"] + "\r\n");
        }

        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rx2 = new List<byte>();
            var dataRowRX2 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowRX2["Date"] = DateTime.Today.ToShortDateString();
                dataRowRX2["Time"] = DateTime.Now.ToLongTimeString();
                dataRowRX2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowRX2["Port"] = portname2;
                dataRowRX2["Dir"] = "RX";
            }

            dataRowRX2["Mark"] = checkBox_Mark.Checked;
            try
            {
                while (serialPort2.BytesToRead > 0) rx2.Add((byte) serialPort2.ReadByte());
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                dataRowRX2["Signal"] = "Error reading port " + serialPort2.PortName + ": " + ex.Message;
            }

            dataRowRX2["Data"] = Accessory.ConvertByteArrayToHex(rx2.ToArray());
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowRX2);
            var outStr2 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //if (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort2hex.Checked) outStr2 += dataRowRX2["Data"];
            else outStr2 += Encoding.GetEncoding(Settings.Default.CodePage).GetString(rx2.ToArray(), 0, rx2.Count);
            CollectBuffer(outStr2, Port2DataIn,
                dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowRX2["Date"] + "," + dataRowRX2["Time"] + "," + dataRowRX2["Milis"] + "," +
                                 dataRowRX2["Port"] + "," + dataRowRX2["Dir"] + "," + dataRowRX2["Data"] + "," +
                                 dataRowRX2["Signal"] + "," + dataRowRX2["Mark"] + "\r\n");
        }

        private void SerialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rx3 = new List<byte>();
            var dataRowRX3 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowRX3["Date"] = DateTime.Today.ToShortDateString();
                dataRowRX3["Time"] = DateTime.Now.ToLongTimeString();
                dataRowRX3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowRX3["Port"] = portname3;
                dataRowRX3["Dir"] = "RX";
            }

            dataRowRX3["Mark"] = checkBox_Mark.Checked;
            try
            {
                while (serialPort3.BytesToRead > 0) rx3.Add((byte) serialPort3.ReadByte());
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                dataRowRX3["Signal"] = "Error reading port " + serialPort3.PortName + ": " + ex.Message;
            }

            dataRowRX3["Data"] = Accessory.ConvertByteArrayToHex(rx3.ToArray());
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowRX3);
            var outStr3 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //kif (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort3hex.Checked) outStr3 += dataRowRX3["Data"];
            else outStr3 += Encoding.GetEncoding(Settings.Default.CodePage).GetString(rx3.ToArray(), 0, rx3.Count);
            CollectBuffer(outStr3, Port3DataIn,
                dataRowRX3["Date"] + " " + dataRowRX3["Time"] + "." + dataRowRX3["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowRX3["Date"] + "," + dataRowRX3["Time"] + "," + dataRowRX3["Milis"] + "," +
                                 dataRowRX3["Port"] + "," + dataRowRX3["Dir"] + "," + dataRowRX3["Data"] + "," +
                                 dataRowRX3["Signal"] + "," + dataRowRX3["Mark"] + "\r\n");
        }

        private void SerialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rx4 = new List<byte>();
            var dataRowRX4 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowRX4["Date"] = DateTime.Today.ToShortDateString();
                dataRowRX4["Time"] = DateTime.Now.ToLongTimeString();
                dataRowRX4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowRX4["Port"] = portname4;
                dataRowRX4["Dir"] = "RX";
            }

            dataRowRX4["Mark"] = checkBox_Mark.Checked;
            try
            {
                while (serialPort4.BytesToRead > 0) rx4.Add((byte) serialPort4.ReadByte());
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                dataRowRX4["Signal"] = "Error reading port " + serialPort4.PortName + ": " + ex.Message;
            }

            dataRowRX4["Data"] = Accessory.ConvertByteArrayToHex(rx4.ToArray());
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowRX4);
            var outStr4 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //kif (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort4hex.Checked) outStr4 += dataRowRX4["Data"];
            else outStr4 += Encoding.GetEncoding(Settings.Default.CodePage).GetString(rx4.ToArray(), 0, rx4.Count);
            CollectBuffer(outStr4, Port4DataIn,
                dataRowRX4["Date"] + " " + dataRowRX4["Time"] + "." + dataRowRX4["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowRX4["Date"] + "," + dataRowRX4["Time"] + "," + dataRowRX4["Milis"] + "," +
                                 dataRowRX4["Port"] + "," + dataRowRX4["Dir"] + "," + dataRowRX4["Data"] + "," +
                                 dataRowRX4["Signal"] + "," + dataRowRX4["Mark"] + "\r\n");
        }

        private void SerialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD1(serialPort1.CDHolding);
            SetPinDSR1(serialPort1.DsrHolding);
            SetPinCTS1(serialPort1.CtsHolding);
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN1["Port"] = portname1;
                dataRowPIN1["Dir"] = "SG";
            }

            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort1.CDHolding && o_cd1 == false)
            {
                o_cd1 = true;
                outStr += "<" + portname1 + "_DCD^>";
            }
            else if (serialPort1.CDHolding == false && o_cd1)
            {
                o_cd1 = false;
                outStr += "<" + portname1 + "_DCDv>";
            }
            //else outStr += "<" + portname1 + "_DCD?>";

            if (serialPort1.DsrHolding && o_dsr1 == false)
            {
                o_dsr1 = true;
                outStr += "<" + portname1 + "_DSR^>";
            }
            else if (serialPort1.DsrHolding == false && o_dsr1)
            {
                o_dsr1 = false;
                outStr += "<" + portname1 + "_DSRv>";
            }
            //else outStr += "<" + portname1 + "_DSR?>";

            if (serialPort1.CtsHolding && o_cts1 == false)
            {
                o_cts1 = true;
                outStr += "<" + portname1 + "_CTS^>";
            }
            else if (serialPort1.CtsHolding == false && o_cts1)
            {
                o_cts1 = false;
                outStr += "<" + portname1 + "_CTSv>";
            }
            //else outStr += "<" + portname1 + "_CTS?>";

            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRING1(true);
                outStr += "<" + portname1 + "_RINGv>";
                SetPinRING1(false);
            }

            if (outStr != "")
            {
                if (checkBox_insPin.Checked)
                    CollectBuffer(outStr, Port1SignalIn,
                        dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
                dataRowPIN1["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN1);
                if (autosaveCSVToolStripMenuItem1.Checked)
                    CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] +
                                     "," + dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] +
                                     "," + dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
            }
        }

        private void SerialPort2_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD2(serialPort2.CDHolding);
            SetPinDSR2(serialPort2.DsrHolding);
            SetPinCTS2(serialPort2.CtsHolding);
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN2["Port"] = portname2;
                dataRowPIN2["Dir"] = "SG";
            }

            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            var outStr = "";

            if (serialPort2.CDHolding && o_cd2 == false)
            {
                o_cd2 = true;
                outStr += "<" + portname2 + "_DCD^>";
            }
            else if (serialPort2.CDHolding == false && o_cd2)
            {
                o_cd2 = false;
                outStr += "<" + portname2 + "_DCDv>";
            }
            //else outStr += "<" + portname2 + "_DCD?>";

            if (serialPort2.DsrHolding && o_dsr2 == false)
            {
                o_dsr2 = true;
                outStr += "<" + portname2 + "_DSR^>";
            }
            else if (serialPort2.DsrHolding == false && o_dsr2)
            {
                o_dsr2 = false;
                outStr += "<" + portname2 + "_DSRv>";
            }
            //else outStr += "<" + portname2 + "_DSR?>";

            if (serialPort2.CtsHolding && o_cts2 == false)
            {
                o_cts2 = true;
                outStr += "<" + portname2 + "_CTS^>";
            }
            else if (serialPort2.CtsHolding == false && o_cts2)
            {
                o_cts2 = false;
                outStr += "<" + portname2 + "_CTSv>";
            }
            //else outStr += "<" + portname2 + "_CTS?>";

            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRING1(true);
                outStr += "<" + portname2 + "_RINGv>";
                SetPinRING1(false);
            }

            if (outStr != "")
            {
                if (checkBox_insPin.Checked)
                    CollectBuffer(outStr, Port2SignalIn,
                        dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
                dataRowPIN2["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN2);
                if (autosaveCSVToolStripMenuItem1.Checked)
                    CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] +
                                     "," + dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] +
                                     "," + dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
            }
        }

        private void SerialPort3_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD3(serialPort3.CDHolding);
            SetPinDSR3(serialPort3.DsrHolding);
            SetPinCTS3(serialPort3.CtsHolding);
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN3["Port"] = portname3;
                dataRowPIN3["Dir"] = "SG";
            }

            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort3.CDHolding && o_cd3 == false)
            {
                o_cd3 = true;
                outStr += "<" + portname3 + "_DCD^>";
            }
            else if (serialPort3.CDHolding == false && o_cd3)
            {
                o_cd3 = false;
                outStr += "<" + portname3 + "_DCDv>";
            }
            //else outStr += "<" + portname3 + "_DCD?>";

            if (serialPort3.DsrHolding && o_dsr3 == false)
            {
                o_dsr3 = true;
                outStr += "<" + portname3 + "_DSR^>";
            }
            else if (serialPort3.DsrHolding == false && o_dsr3)
            {
                o_dsr3 = false;
                outStr += "<" + portname3 + "_DSRv>";
            }
            //else outStr += "<" + portname3 + "_DSR?>";

            if (serialPort3.CtsHolding && o_cts3 == false)
            {
                o_cts3 = true;
                outStr += "<" + portname3 + "_CTS^>";
            }
            else if (serialPort3.CtsHolding == false && o_cts3)
            {
                o_cts3 = false;
                outStr += "<" + portname3 + "_CTSv>";
            }
            //else outStr += "<" + portname3 + "_CTS?>";

            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRING1(true);
                outStr += "<" + portname3 + "_RINGv>";
                SetPinRING1(false);
            }

            if (outStr != "")
            {
                if (checkBox_insPin.Checked)
                    CollectBuffer(outStr, Port3SignalIn,
                        dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
                dataRowPIN3["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN3);
                if (autosaveCSVToolStripMenuItem1.Checked)
                    CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] +
                                     "," + dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] +
                                     "," + dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
            }
        }

        private void SerialPort4_PinChanged(object sender, SerialPinChangedEventArgs e) ////
        {
            SetPinCD4(serialPort4.CDHolding);
            SetPinDSR4(serialPort4.DsrHolding);
            SetPinCTS4(serialPort4.CtsHolding);
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN4["Port"] = portname4;
                dataRowPIN4["Dir"] = "SG";
            }

            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort4.CDHolding && o_cd4 == false)
            {
                o_cd4 = true;
                outStr += "<" + portname4 + "_DCD^>";
            }
            else if (serialPort4.CDHolding == false && o_cd4)
            {
                o_cd4 = false;
                outStr += "<" + portname4 + "_DCDv>";
            }
            //else outStr += "<" + portname4 + "_DCD?>";

            if (serialPort4.DsrHolding && o_dsr4 == false)
            {
                o_dsr4 = true;
                outStr += "<" + portname4 + "_DSR^>";
            }
            else if (serialPort4.DsrHolding == false && o_dsr4)
            {
                o_dsr4 = false;
                outStr += "<" + portname4 + "_DSRv>";
            }
            //else outStr += "<" + portname4 + "_DSR?>";

            if (serialPort4.CtsHolding && o_cts4 == false)
            {
                o_cts4 = true;
                outStr += "<" + portname4 + "_CTS^>";
            }
            else if (serialPort4.CtsHolding == false && o_cts4)
            {
                o_cts4 = false;
                outStr += "<" + portname4 + "_CTSv>";
            }
            //else outStr += "<" + portname4 + "_CTS?>";

            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRING1(true);
                outStr += "<" + portname4 + "_RINGv>";
                SetPinRING1(false);
            }

            if (outStr != "")
            {
                if (checkBox_insPin.Checked)
                    CollectBuffer(outStr, Port4SignalIn,
                        dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
                dataRowPIN4["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN4);
                if (autosaveCSVToolStripMenuItem1.Checked)
                    CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] +
                                     "," + dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] +
                                     "," + dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
            }
        }

        private void SerialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port1 error: " + e.EventType);
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN1["Port"] = portname1;
                dataRowPIN1["Dir"] = "ER";
            }

            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            var outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port1SignalIn,
                    dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," +
                                 dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," +
                                 dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void SerialPort2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN2["Port"] = portname2;
                dataRowPIN2["Dir"] = "ER";
            }

            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            var outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port2SignalIn,
                    dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," +
                                 dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," +
                                 dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void SerialPort3_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN3["Port"] = portname3;
                dataRowPIN3["Dir"] = "ER";
            }

            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            var outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port3SignalIn,
                    dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," +
                                 dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," +
                                 dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void SerialPort4_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN4["Port"] = portname4;
                dataRowPIN4["Dir"] = "ER";
            }

            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            var outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port4SignalIn,
                    dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," +
                                 dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," +
                                 dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void CheckBox_DTR1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = checkBox_DTR1.Checked;
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN1["Port"] = portname1;
                dataRowPIN1["Dir"] = "User";
            }

            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort1.DtrEnable && o_dtr1 == false)
            {
                o_dtr1 = true;
                outStr += "<" + portname1 + "_DTR^>";
            }
            else if (serialPort1.DtrEnable == false && o_dtr1)
            {
                o_dtr1 = false;
                outStr += "<" + portname1 + "_DTRv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port1SignalOut,
                    dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," +
                                 dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," +
                                 dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void CheckBox_DTR2_CheckedChanged(object sender, EventArgs e)
        {
            serialPort2.DtrEnable = checkBox_DTR2.Checked;
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN2["Port"] = portname2;
                dataRowPIN2["Dir"] = "User";
            }

            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort2.DtrEnable && o_dtr2 == false)
            {
                o_dtr2 = true;
                outStr += "<" + portname2 + "_DTR^>";
            }

            if (serialPort2.DtrEnable == false && o_dtr2)
            {
                o_dtr2 = false;
                outStr += "<" + portname2 + "_DTRv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port2SignalOut,
                    dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," +
                                 dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," +
                                 dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void CheckBox_DTR3_CheckedChanged(object sender, EventArgs e)
        {
            serialPort3.DtrEnable = checkBox_DTR3.Checked;
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN3["Port"] = portname3;
                dataRowPIN3["Dir"] = "User";
            }

            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort3.DtrEnable && o_dtr3 == false)
            {
                o_dtr3 = true;
                outStr += "<" + portname3 + "_DTR^>";
            }

            if (serialPort3.DtrEnable == false && o_dtr3)
            {
                o_dtr3 = false;
                outStr += "<" + portname3 + "_DTRv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port3SignalOut,
                    dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," +
                                 dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," +
                                 dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void CheckBox_DTR4_CheckedChanged(object sender, EventArgs e)
        {
            serialPort4.DtrEnable = checkBox_DTR4.Checked;
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN4["Port"] = portname4;
                dataRowPIN4["Dir"] = "User";
            }

            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort4.DtrEnable && o_dtr4 == false)
            {
                o_dtr4 = true;
                outStr += "<" + portname4 + "_DTR^>";
            }

            if (serialPort4.DtrEnable == false && o_dtr4)
            {
                o_dtr4 = false;
                outStr += "<" + portname4 + "_DTRv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port4SignalOut,
                    dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," +
                                 dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," +
                                 dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void CheckBox_RTS1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.RtsEnable = checkBox_RTS1.Checked;
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN1["Port"] = portname1;
                dataRowPIN1["Dir"] = "User";
            }

            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort1.RtsEnable && o_rts1 == false && serialPort1.Handshake != Handshake.RequestToSend &&
                serialPort1.Handshake != Handshake.RequestToSendXOnXOff)
            {
                o_rts1 = true;
                outStr += "<" + portname1 + "_RTS^>";
            }
            else if (serialPort1.RtsEnable == false && o_rts1)
            {
                o_rts1 = false;
                outStr += "<" + portname1 + "_RTSv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port1SignalOut,
                    dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," +
                                 dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," +
                                 dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void CheckBox_RTS2_CheckedChanged(object sender, EventArgs e)
        {
            serialPort2.RtsEnable = checkBox_RTS2.Checked;
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN2["Port"] = portname2;
                dataRowPIN2["Dir"] = "User";
            }

            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort2.RtsEnable && o_rts2 == false)
            {
                o_rts2 = true;
                outStr += "<" + portname2 + "_RTS^>";
            }

            if (serialPort2.RtsEnable == false && o_rts2)
            {
                o_rts2 = false;
                outStr += "<" + portname2 + "_RTSv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port2SignalOut,
                    dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," +
                                 dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," +
                                 dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void CheckBox_RTS3_CheckedChanged(object sender, EventArgs e)
        {
            serialPort3.RtsEnable = checkBox_RTS3.Checked;
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN3["Port"] = portname3;
                dataRowPIN3["Dir"] = "User";
            }

            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort3.RtsEnable && o_rts3 == false)
            {
                o_rts3 = true;
                outStr += "<" + portname3 + "_RTS^>";
            }

            if (serialPort3.RtsEnable == false && o_rts3)
            {
                o_rts3 = false;
                outStr += "<" + portname3 + "_RTSv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port3SignalOut,
                    dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," +
                                 dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," +
                                 dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void CheckBox_RTS4_CheckedChanged(object sender, EventArgs e)
        {
            serialPort4.RtsEnable = checkBox_RTS4.Checked;
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            if (checkBox_insTime.Checked)
            {
                dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
                dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
                dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            }

            if (checkBox_insDir.Checked)
            {
                dataRowPIN4["Port"] = portname4;
                dataRowPIN4["Dir"] = "User";
            }

            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            var outStr = "";
            if (serialPort4.RtsEnable && o_rts4 == false)
            {
                o_rts4 = true;
                outStr += "<" + portname4 + "_RTS^>";
            }

            if (serialPort4.RtsEnable == false && o_rts4)
            {
                o_rts4 = false;
                outStr += "<" + portname4 + "_RTSv>";
            }

            if (checkBox_insPin.Checked)
                CollectBuffer(outStr, Port4SignalOut,
                    dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked)
                CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," +
                                 dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," +
                                 dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void TextBox_custom_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox_commandhex.Checked)
            {
                var c = e.KeyChar;
                if (c != '\b' && !(c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f' || c >= '0' && c <= '9' || c == 0x08 ||
                                   c == ' ')) e.Handled = true;
            }
        }

        private void TextBox_params_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox_paramhex.Checked)
            {
                var c = e.KeyChar;
                if (c != '\b' && !(c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f' || c >= '0' && c <= '9' || c == 0x08 ||
                                   c == ' ')) e.Handled = true;
            }
        }

        private void TextBox_suff_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c != '\b' && !(c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f' || c >= '0' && c <= '9' || c == 0x08 ||
                               c == ' ')) e.Handled = true;
        }

        private void CheckBox_suff_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_suff.Checked) textBox_suff.Enabled = false;
            else textBox_suff.Enabled = true;
            SendStringCollect();
        }

        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            SerialPopulate();
        }

        private void Button_clear1_Click(object sender, EventArgs e)
        {
            textBox_terminal.Clear();
            CSVdataTable.Rows.Clear();
        }

        private void ComboBox_portname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname1.SelectedIndex != 0 &&
                comboBox_portname1.SelectedIndex == comboBox_portname2.SelectedIndex)
                comboBox_portname1.SelectedIndex = 0;
            if (comboBox_portname1.SelectedIndex != 0 &&
                comboBox_portname1.SelectedIndex == comboBox_portname3.SelectedIndex)
                comboBox_portname1.SelectedIndex = 0;
            if (comboBox_portname1.SelectedIndex != 0 &&
                comboBox_portname1.SelectedIndex == comboBox_portname4.SelectedIndex)
                comboBox_portname1.SelectedIndex = 0;
            if (comboBox_portname1.SelectedIndex == 0)
            {
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;
                checkBox_sendPort1.Enabled = false;
                checkBox_sendPort1.Checked = false;
                checkBox_displayPort1hex.Enabled = false;
            }
            else
            {
                if (comboBox_portname2.SelectedIndex > 0)
                {
                    comboBox_portspeed1.SelectedIndex = comboBox_portspeed2.SelectedIndex;
                    comboBox_handshake1.SelectedIndex = comboBox_handshake2.SelectedIndex;
                    comboBox_databits1.SelectedIndex = comboBox_databits2.SelectedIndex;
                    comboBox_parity1.SelectedIndex = comboBox_parity2.SelectedIndex;
                    comboBox_stopbits1.SelectedIndex = comboBox_stopbits2.SelectedIndex;
                }
                else
                {
                    comboBox_portspeed1.SelectedIndex = 0;
                    comboBox_handshake1.SelectedIndex = 0;
                    comboBox_databits1.SelectedIndex = 0;
                    comboBox_parity1.SelectedIndex = 2;
                    comboBox_stopbits1.SelectedIndex = 1;
                }

                comboBox_portspeed1.Enabled = true;
                comboBox_handshake1.Enabled = true;
                comboBox_databits1.Enabled = true;
                comboBox_parity1.Enabled = true;
                comboBox_stopbits1.Enabled = true;
                checkBox_sendPort1.Enabled = true;
                checkBox_displayPort1hex.Enabled = true;
            }

            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 &&
                comboBox_portname3.SelectedIndex == 0 &&
                comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void ComboBox_portname2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname2.SelectedIndex != 0 &&
                comboBox_portname2.SelectedIndex == comboBox_portname1.SelectedIndex)
                comboBox_portname2.SelectedIndex = 0;
            if (comboBox_portname2.SelectedIndex != 0 &&
                comboBox_portname2.SelectedIndex == comboBox_portname3.SelectedIndex)
                comboBox_portname2.SelectedIndex = 0;
            if (comboBox_portname2.SelectedIndex != 0 &&
                comboBox_portname2.SelectedIndex == comboBox_portname4.SelectedIndex)
                comboBox_portname2.SelectedIndex = 0;
            if (comboBox_portname2.SelectedIndex == 0)
            {
                comboBox_portspeed2.Enabled = false;
                comboBox_handshake2.Enabled = false;
                comboBox_databits2.Enabled = false;
                comboBox_parity2.Enabled = false;
                comboBox_stopbits2.Enabled = false;
                checkBox_sendPort2.Enabled = false;
                checkBox_sendPort2.Checked = false;
                checkBox_displayPort2hex.Enabled = false;
            }
            else
            {
                if (comboBox_portname1.SelectedIndex > 0)
                {
                    comboBox_portspeed2.SelectedIndex = comboBox_portspeed1.SelectedIndex;
                    comboBox_handshake2.SelectedIndex = comboBox_handshake1.SelectedIndex;
                    comboBox_databits2.SelectedIndex = comboBox_databits1.SelectedIndex;
                    comboBox_parity2.SelectedIndex = comboBox_parity1.SelectedIndex;
                    comboBox_stopbits2.SelectedIndex = comboBox_stopbits1.SelectedIndex;
                }
                else
                {
                    comboBox_portspeed2.SelectedIndex = 0;
                    comboBox_handshake2.SelectedIndex = 0;
                    comboBox_databits2.SelectedIndex = 0;
                    comboBox_parity2.SelectedIndex = 2;
                    comboBox_stopbits2.SelectedIndex = 1;
                }

                comboBox_portspeed2.Enabled = true;
                comboBox_handshake2.Enabled = true;
                comboBox_databits2.Enabled = true;
                comboBox_parity2.Enabled = true;
                comboBox_stopbits2.Enabled = true;
                checkBox_sendPort2.Enabled = true;
                checkBox_displayPort2hex.Enabled = true;
            }

            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 &&
                comboBox_portname3.SelectedIndex == 0 &&
                comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void ComboBox_portname3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname3.SelectedIndex != 0 &&
                comboBox_portname3.SelectedIndex == comboBox_portname1.SelectedIndex)
                comboBox_portname3.SelectedIndex = 0;
            if (comboBox_portname3.SelectedIndex != 0 &&
                comboBox_portname3.SelectedIndex == comboBox_portname2.SelectedIndex)
                comboBox_portname3.SelectedIndex = 0;
            if (comboBox_portname3.SelectedIndex != 0 &&
                comboBox_portname3.SelectedIndex == comboBox_portname4.SelectedIndex)
                comboBox_portname3.SelectedIndex = 0;
            if (comboBox_portname3.SelectedIndex == 0)
            {
                comboBox_portspeed3.Enabled = false;
                comboBox_handshake3.Enabled = false;
                comboBox_databits3.Enabled = false;
                comboBox_parity3.Enabled = false;
                comboBox_stopbits3.Enabled = false;
                checkBox_sendPort3.Enabled = false;
                checkBox_sendPort3.Checked = false;
                checkBox_displayPort3hex.Enabled = false;
            }
            else
            {
                if (comboBox_portname4.SelectedIndex > 0)
                {
                    comboBox_portspeed3.SelectedIndex = comboBox_portspeed4.SelectedIndex;
                    comboBox_handshake3.SelectedIndex = comboBox_handshake4.SelectedIndex;
                    comboBox_databits3.SelectedIndex = comboBox_databits4.SelectedIndex;
                    comboBox_parity3.SelectedIndex = comboBox_parity4.SelectedIndex;
                    comboBox_stopbits3.SelectedIndex = comboBox_stopbits4.SelectedIndex;
                }
                else
                {
                    comboBox_portspeed3.SelectedIndex = 0;
                    comboBox_handshake3.SelectedIndex = 0;
                    comboBox_databits3.SelectedIndex = 0;
                    comboBox_parity3.SelectedIndex = 2;
                    comboBox_stopbits3.SelectedIndex = 1;
                }

                comboBox_portspeed3.Enabled = true;
                comboBox_handshake3.Enabled = true;
                comboBox_databits3.Enabled = true;
                comboBox_parity3.Enabled = true;
                comboBox_stopbits3.Enabled = true;
                checkBox_sendPort3.Enabled = true;
                checkBox_displayPort3hex.Enabled = true;
            }

            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 &&
                comboBox_portname3.SelectedIndex == 0 &&
                comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void ComboBox_portname4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname4.SelectedIndex != 0 &&
                comboBox_portname4.SelectedIndex == comboBox_portname1.SelectedIndex)
                comboBox_portname4.SelectedIndex = 0;
            if (comboBox_portname4.SelectedIndex != 0 &&
                comboBox_portname4.SelectedIndex == comboBox_portname2.SelectedIndex)
                comboBox_portname4.SelectedIndex = 0;
            if (comboBox_portname4.SelectedIndex != 0 &&
                comboBox_portname4.SelectedIndex == comboBox_portname3.SelectedIndex)
                comboBox_portname4.SelectedIndex = 0;
            if (comboBox_portname4.SelectedIndex == 0)
            {
                comboBox_portspeed4.Enabled = false;
                comboBox_handshake4.Enabled = false;
                comboBox_databits4.Enabled = false;
                comboBox_parity4.Enabled = false;
                comboBox_stopbits4.Enabled = false;
                checkBox_sendPort4.Enabled = false;
                checkBox_sendPort4.Checked = false;
                checkBox_displayPort4hex.Enabled = false;
            }
            else
            {
                if (comboBox_portname3.SelectedIndex > 0)
                {
                    comboBox_portspeed4.SelectedIndex = comboBox_portspeed3.SelectedIndex;
                    comboBox_handshake4.SelectedIndex = comboBox_handshake3.SelectedIndex;
                    comboBox_databits4.SelectedIndex = comboBox_databits3.SelectedIndex;
                    comboBox_parity4.SelectedIndex = comboBox_parity3.SelectedIndex;
                    comboBox_stopbits4.SelectedIndex = comboBox_stopbits3.SelectedIndex;
                }
                else
                {
                    comboBox_portspeed4.SelectedIndex = 0;
                    comboBox_handshake4.SelectedIndex = 0;
                    comboBox_databits4.SelectedIndex = 0;
                    comboBox_parity4.SelectedIndex = 2;
                    comboBox_stopbits4.SelectedIndex = 1;
                }

                comboBox_portspeed4.Enabled = true;
                comboBox_handshake4.Enabled = true;
                comboBox_databits4.Enabled = true;
                comboBox_parity4.Enabled = true;
                comboBox_stopbits4.Enabled = true;
                checkBox_sendPort4.Enabled = true;
                checkBox_displayPort4hex.Enabled = true;
            }

            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 &&
                comboBox_portname3.SelectedIndex == 0 &&
                comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void CheckBox_commandhex_CheckedChanged(object sender, EventArgs e)
        {
            var tmpstr = textBox_command.Text;
            if (checkBox_commandhex.Checked) textBox_command.Text = Accessory.ConvertStringToHex(tmpstr);
            else textBox_command.Text = Accessory.ConvertHexToString(tmpstr);
        }

        private void CheckBox_paramhex_CheckedChanged(object sender, EventArgs e)
        {
            var tmpstr = textBox_params.Text;
            if (checkBox_paramhex.Checked) textBox_params.Text = Accessory.ConvertStringToHex(tmpstr);
            else textBox_params.Text = Accessory.ConvertHexToString(tmpstr);
        }

        private void CheckBox_send_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_sendPort1.Checked == false && checkBox_sendPort2.Checked == false &&
                checkBox_sendPort3.Checked == false && checkBox_sendPort4.Checked == false) button_send.Enabled = false;
            else if (serialPort1.IsOpen || serialPort2.IsOpen || serialPort3.IsOpen || serialPort4.IsOpen)
                button_send.Enabled = true;
        }

        private void TextBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_commandhex.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
            SendStringCollect();
        }

        private void TextBox_params_Leave(object sender, EventArgs e)
        {
            if (checkBox_paramhex.Checked) textBox_params.Text = Accessory.CheckHexString(textBox_params.Text);
            SendStringCollect();
        }

        private void TextBox_suff_Leave(object sender, EventArgs e)
        {
            if (checkBox_suffhex.Checked) textBox_suff.Text = Accessory.CheckHexString(textBox_suff.Text);
            SendStringCollect();
        }

        private void CheckBox_cr_CheckedChanged(object sender, EventArgs e)
        {
            SendStringCollect();
        }

        private void CheckBox_lf_CheckedChanged(object sender, EventArgs e)
        {
            SendStringCollect();
        }

        private void CheckBox_suffhex_CheckedChanged(object sender, EventArgs e)
        {
            var tmpstr = textBox_suff.Text;
            if (checkBox_suffhex.Checked) textBox_suff.Text = Accessory.ConvertStringToHex(tmpstr);
            else textBox_suff.Text = Accessory.ConvertHexToString(tmpstr);
        }

        private void CheckBox_portName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_portName.Checked)
            {
                if (button_closeport.Enabled)
                {
                    textBox_port1Name.Enabled = false;
                    textBox_port2Name.Enabled = false;
                    textBox_port3Name.Enabled = false;
                    textBox_port4Name.Enabled = false;
                }

                checkBox_sendPort1.Text = textBox_port1Name.Text;
                checkBox_sendPort2.Text = textBox_port2Name.Text;
                checkBox_sendPort3.Text = textBox_port3Name.Text;
                checkBox_sendPort4.Text = textBox_port4Name.Text;
                checkBox_displayPort1hex.Text = textBox_port1Name.Text;
                checkBox_displayPort2hex.Text = textBox_port2Name.Text;
                checkBox_displayPort3hex.Text = textBox_port3Name.Text;
                checkBox_displayPort4hex.Text = textBox_port4Name.Text;
                portname1 = textBox_port1Name.Text;
                portname2 = textBox_port2Name.Text;
                portname3 = textBox_port3Name.Text;
                portname4 = textBox_port4Name.Text;
            }
            else
            {
                textBox_port1Name.Enabled = true;
                textBox_port2Name.Enabled = true;
                textBox_port3Name.Enabled = true;
                textBox_port4Name.Enabled = true;
                checkBox_sendPort1.Text = comboBox_portname1.Text;
                checkBox_sendPort2.Text = comboBox_portname2.Text;
                checkBox_sendPort3.Text = comboBox_portname3.Text;
                checkBox_sendPort4.Text = comboBox_portname4.Text;
                checkBox_displayPort1hex.Text = comboBox_portname1.Text;
                checkBox_displayPort2hex.Text = comboBox_portname2.Text;
                checkBox_displayPort3hex.Text = comboBox_portname3.Text;
                checkBox_displayPort4hex.Text = comboBox_portname4.Text;
                portname1 = comboBox_portname1.Text;
                portname2 = comboBox_portname2.Text;
                portname3 = comboBox_portname3.Text;
                portname4 = comboBox_portname4.Text;
            }
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (saveFileDialog.Title == "Save .TXT log as...")
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox_terminal.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to file " + saveFileDialog.FileName + ": " + ex.Message);
                }

            if (saveFileDialog.Title == "Save .CSV log as...")
            {
                var columnCount = dataGridView.ColumnCount;
                var output = "";
                for (var i = 0; i < columnCount; i++) output += dataGridView.Columns[i].Name + ",";
                output += "\r\n";
                for (var i = 1; i - 1 < dataGridView.RowCount; i++)
                {
                    for (var j = 0; j < columnCount; j++) output += dataGridView.Rows[i - 1].Cells[j].Value + ",";
                    output += "\r\n";
                }

                try
                {
                    File.WriteAllText(saveFileDialog.FileName, output, Encoding.GetEncoding(Settings.Default.CodePage));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to file " + saveFileDialog.FileName + ": " + ex.Message);
                }
            }
        }

        private void SaveTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save .TXT log as...";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text files|*.txt|All files|*.*";
            saveFileDialog.FileName = "terminal_" + DateTime.Today.ToShortDateString().Replace("/", "_") + ".txt";
            saveFileDialog.ShowDialog();
        }

        private void SaveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save .CSV log as...";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            saveFileDialog.FileName = "terminal_" + DateTime.Today.ToShortDateString().Replace("/", "_") + ".csv";
            saveFileDialog.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RS232 Monitor\r\n(c) Kalugin Andrey\r\nContact: jekyll@mail.ru");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveParametersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.Default.DefaultCommand = textBox_command.Text;
            Settings.Default.DefaultCommandHex = checkBox_commandhex.Checked;
            Settings.Default.DefaultParameter = textBox_params.Text;
            Settings.Default.DefaultParamHex = checkBox_paramhex.Checked;
            Settings.Default.addCR = checkBox_cr.Checked;
            Settings.Default.addLF = checkBox_lf.Checked;
            Settings.Default.addSuff = checkBox_suff.Checked;
            Settings.Default.SuffText = textBox_suff.Text;
            Settings.Default.DefaultSuffHex = checkBox_suffhex.Checked;
            Settings.Default.LogSignal = checkBox_insPin.Checked;
            Settings.Default.LogTime = checkBox_insTime.Checked;
            Settings.Default.LogDir = checkBox_insDir.Checked;
            Settings.Default.LogPortName = checkBox_portName.Checked;
            Settings.Default.HexPort1 = checkBox_displayPort1hex.Checked;
            Settings.Default.HexPort2 = checkBox_displayPort2hex.Checked;
            Settings.Default.HexPort3 = checkBox_displayPort3hex.Checked;
            Settings.Default.HexPort4 = checkBox_displayPort4hex.Checked;
            Settings.Default.Port1Name = textBox_port1Name.Text;
            Settings.Default.Port2Name = textBox_port2Name.Text;
            Settings.Default.Port3Name = textBox_port3Name.Text;
            Settings.Default.Port4Name = textBox_port4Name.Text;
            Settings.Default.LogGrid = logToGridToolStripMenuItem.Checked;
            Settings.Default.LogText = logToTextToolStripMenuItem.Checked;
            Settings.Default.AutoScroll = autoscrollToolStripMenuItem.Checked;
            Settings.Default.LineWrap = lineWrapToolStripMenuItem.Checked;
            Settings.Default.AutoLogTXT = autosaveTXTToolStripMenuItem1.Checked;
            Settings.Default.TXTlogFile = terminaltxtToolStripMenuItem1.Text;
            Settings.Default.AutoLogCSV = autosaveCSVToolStripMenuItem1.Checked;
            Settings.Default.LineBreakTimeout = limitTick / 10000;
            Settings.Default.CSVLineNumber = CSVLineNumberLimit;
            Settings.Default.Save();
        }

        private void AutosaveTXTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            autosaveTXTToolStripMenuItem1.Checked = !autosaveTXTToolStripMenuItem1.Checked;
            terminaltxtToolStripMenuItem1.Enabled = !autosaveTXTToolStripMenuItem1.Checked;
        }

        private void LineWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineWrapToolStripMenuItem.Checked = !lineWrapToolStripMenuItem.Checked;
            textBox_terminal.WordWrap = lineWrapToolStripMenuItem.Checked;
        }

        private void AutoscrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoscrollToolStripMenuItem.Checked = !autoscrollToolStripMenuItem.Checked;
        }

        private void LogToTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logToTextToolStripMenuItem.Checked)
            {
                logToTextToolStripMenuItem.Checked = false;
                textBox_terminal.Enabled = false;
                ((Control) tabPage2).Enabled = false;
                if (logToGridToolStripMenuItem.Checked == false)
                {
                    tabControl1.Enabled = false;
                    tabControl1.Visible = false;
                }
            }
            else
            {
                logToTextToolStripMenuItem.Checked = true;
                textBox_terminal.Enabled = true;
                ((Control) tabPage2).Enabled = true;
                tabControl1.Enabled = true;
                tabControl1.Visible = true;
            }
        }

        private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage1 && logToTextToolStripMenuItem.Checked == false)
                e.Cancel = true;
            if (e.TabPage == tabPage2 && logToGridToolStripMenuItem.Checked == false)
                e.Cancel = true;
        }

        private void LogToGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logToGridToolStripMenuItem.Checked)
            {
                logToGridToolStripMenuItem.Checked = false;
                dataGridView.Enabled = false;
                ((Control) tabPage1).Enabled = false;
                if (logToTextToolStripMenuItem.Checked == false)
                {
                    tabControl1.Enabled = false;
                    tabControl1.Visible = false;
                }
            }
            else
            {
                logToGridToolStripMenuItem.Checked = true;
                dataGridView.Enabled = true;
                ((Control) tabPage1).Enabled = true;
                tabControl1.Enabled = true;
                tabControl1.Visible = true;
            }
        }

        private void CheckBox_Mark_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Mark.Checked) checkBox_Mark.Font = new Font(checkBox_Mark.Font, FontStyle.Bold);
            else checkBox_Mark.Font = new Font(checkBox_Mark.Font, FontStyle.Regular);
        }

        private void TextBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_send.Enabled)
                if (e.KeyData == Keys.Return)
                    Button_send_Click(textBox_command, EventArgs.Empty);
        }

        private void ToolStripMenuItem_onlyData_Click(object sender, EventArgs e)
        {
            toolStripMenuItem_onlyData.Checked = !toolStripMenuItem_onlyData.Checked;

            if (toolStripMenuItem_onlyData.Checked == false)
            {
                serialPort1.ErrorReceived += SerialPort1_ErrorReceived;
                serialPort2.ErrorReceived += SerialPort2_ErrorReceived;
                serialPort3.ErrorReceived += SerialPort3_ErrorReceived;
                serialPort4.ErrorReceived += SerialPort4_ErrorReceived;
                checkBox_insPin.Checked = true;
            }
            else
            {
                serialPort1.ErrorReceived -= SerialPort1_ErrorReceived;
                serialPort2.ErrorReceived -= SerialPort2_ErrorReceived;
                serialPort3.ErrorReceived -= SerialPort3_ErrorReceived;
                serialPort4.ErrorReceived -= SerialPort4_ErrorReceived;
                checkBox_insPin.Checked = false;
            }
        }

        private void SerialPopulate()
        {
            comboBox_portname1.Items.Clear();
            comboBox_handshake1.Items.Clear();
            comboBox_parity1.Items.Clear();
            comboBox_stopbits1.Items.Clear();

            comboBox_portname2.Items.Clear();
            comboBox_handshake2.Items.Clear();
            comboBox_parity2.Items.Clear();
            comboBox_stopbits2.Items.Clear();

            comboBox_portname3.Items.Clear();
            comboBox_handshake3.Items.Clear();
            comboBox_parity3.Items.Clear();
            comboBox_stopbits3.Items.Clear();

            comboBox_portname4.Items.Clear();
            comboBox_handshake4.Items.Clear();
            comboBox_parity4.Items.Clear();
            comboBox_stopbits4.Items.Clear();

            //Serial settings populate
            comboBox_portname1.Items.Add("-None-");
            comboBox_portname2.Items.Add("-None-");
            comboBox_portname3.Items.Add("-None-");
            comboBox_portname4.Items.Add("-None-");

            //Add ports
            foreach (var s in SerialPort.GetPortNames())
            {
                comboBox_portname1.Items.Add(s);
                comboBox_portname2.Items.Add(s);
                comboBox_portname3.Items.Add(s);
                comboBox_portname4.Items.Add(s);
            }

            //Add handshake methods
            foreach (var s in Enum.GetNames(typeof(Handshake)))
            {
                comboBox_handshake1.Items.Add(s);
                comboBox_handshake2.Items.Add(s);
                comboBox_handshake3.Items.Add(s);
                comboBox_handshake4.Items.Add(s);
            }

            //Add parity
            foreach (var s in Enum.GetNames(typeof(Parity)))
            {
                comboBox_parity1.Items.Add(s);
                comboBox_parity2.Items.Add(s);
                comboBox_parity3.Items.Add(s);
                comboBox_parity4.Items.Add(s);
            }

            //Add stopbits
            foreach (var s in Enum.GetNames(typeof(StopBits)))
            {
                comboBox_stopbits1.Items.Add(s);
                comboBox_stopbits2.Items.Add(s);
                comboBox_stopbits3.Items.Add(s);
                comboBox_stopbits4.Items.Add(s);
            }

            if (comboBox_portname1.Items.Count > 1)
            {
                comboBox_portname1.SelectedIndex = 1;
                comboBox_portspeed1.SelectedIndex = 0;
                comboBox_handshake1.SelectedIndex = 0;
                comboBox_databits1.SelectedIndex = 0;
                comboBox_parity1.SelectedIndex = 2;
                comboBox_stopbits1.SelectedIndex = 1;
                checkBox_sendPort1.Enabled = true;
                checkBox_displayPort1hex.Enabled = true;
            }
            else
            {
                comboBox_portname1.SelectedIndex = 0;
                checkBox_sendPort1.Enabled = false;
                checkBox_displayPort1hex.Enabled = false;
            }

            if (comboBox_portname2.Items.Count > 2)
            {
                comboBox_portname2.SelectedIndex = 2;
                comboBox_portspeed2.SelectedIndex = 0;
                comboBox_handshake2.SelectedIndex = 0;
                comboBox_databits2.SelectedIndex = 0;
                comboBox_parity2.SelectedIndex = 2;
                comboBox_stopbits2.SelectedIndex = 1;
                checkBox_sendPort2.Enabled = true;
                checkBox_displayPort2hex.Enabled = true;
            }
            else
            {
                comboBox_portname2.SelectedIndex = 0;
                checkBox_sendPort2.Enabled = false;
                checkBox_displayPort2hex.Enabled = false;
            }

            if (comboBox_portname3.Items.Count > 3)
            {
                comboBox_portname3.SelectedIndex = 3;
                comboBox_portspeed3.SelectedIndex = 0;
                comboBox_handshake3.SelectedIndex = 0;
                comboBox_databits3.SelectedIndex = 0;
                comboBox_parity3.SelectedIndex = 2;
                comboBox_stopbits3.SelectedIndex = 1;
                checkBox_sendPort3.Enabled = true;
                checkBox_displayPort3hex.Enabled = true;
            }
            else
            {
                comboBox_portname3.SelectedIndex = 0;
                checkBox_sendPort3.Enabled = false;
                checkBox_displayPort3hex.Enabled = false;
            }

            if (comboBox_portname4.Items.Count > 4)
            {
                comboBox_portname4.SelectedIndex = 4;
                comboBox_portspeed4.SelectedIndex = 0;
                comboBox_handshake4.SelectedIndex = 0;
                comboBox_databits4.SelectedIndex = 0;
                comboBox_parity4.SelectedIndex = 2;
                comboBox_stopbits4.SelectedIndex = 1;
                checkBox_sendPort4.Enabled = true;
                checkBox_displayPort4hex.Enabled = true;
            }
            else
            {
                comboBox_portname4.SelectedIndex = 0;
                checkBox_sendPort4.Enabled = false;
                checkBox_displayPort4hex.Enabled = false;
            }

            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 &&
                comboBox_portname3.SelectedIndex == 0 &&
                comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
            CheckBox_portName_CheckedChanged(this, EventArgs.Empty);
        }

        private delegate void SetTextCallback1(string text);

        private void SetText(string text)
        {
            text = Accessory.FilterZeroChar(text);
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (textBox_terminal1.InvokeRequired)
            if (textBox_terminal.InvokeRequired)
            {
                var d = new SetTextCallback1(SetText);
                BeginInvoke(d, text);
            }
            else
            {
                var pos = textBox_terminal.SelectionStart;
                textBox_terminal.AppendText(text);
                if (textBox_terminal.Lines.Length > LogLinesLimit)
                {
                    var tmp = new StringBuilder();
                    for (var i = textBox_terminal.Lines.Length - LogLinesLimit; i < textBox_terminal.Lines.Length; i++)
                    {
                        tmp.Append(textBox_terminal.Lines[i]);
                        tmp.Append("\r\n");
                    }

                    textBox_terminal.Text = tmp.ToString();
                }

                if (autoscrollToolStripMenuItem.Checked)
                {
                    textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
                    textBox_terminal.ScrollToCaret();
                }
                else
                {
                    textBox_terminal.SelectionStart = pos;
                    textBox_terminal.ScrollToCaret();
                }
            }
        }

        private void SendStringCollect()
        {
            string tmpStr;
            if (checkBox_commandhex.Checked) tmpStr = textBox_command.Text.Trim();
            else tmpStr = Accessory.ConvertStringToHex(textBox_command.Text).Trim();
            if (checkBox_paramhex.Checked) tmpStr += " " + textBox_params.Text.Trim();
            else tmpStr += " " + Accessory.ConvertStringToHex(textBox_params.Text).Trim();
            if (checkBox_cr.Checked) tmpStr += " 0D";
            if (checkBox_lf.Checked) tmpStr += " 0A";
            if (checkBox_suff.Checked)
            {
                if (checkBox_suffhex.Checked) tmpStr += " " + textBox_suff.Text.Trim();
                else tmpStr += " " + Accessory.ConvertStringToHex(textBox_suff.Text).Trim();
            }

            textBox_senddata.Text = Accessory.CheckHexString(tmpStr);
        }

        private readonly object threadLock = new object();

        private void CheckBox_insPin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_insPin.Checked)
            {
                serialPort1.PinChanged += SerialPort1_PinChanged;
                serialPort2.PinChanged += SerialPort2_PinChanged;
                serialPort3.PinChanged += SerialPort3_PinChanged;
                serialPort4.PinChanged += SerialPort4_PinChanged;
            }
            else
            {
                serialPort1.PinChanged -= SerialPort1_PinChanged;
                serialPort2.PinChanged -= SerialPort2_PinChanged;
                serialPort3.PinChanged -= SerialPort3_PinChanged;
                serialPort4.PinChanged -= SerialPort4_PinChanged;
            }
        }

        private void ToolStripTextBox_CSVLinesNumber_Leave(object sender, EventArgs e)
        {
            int.TryParse(toolStripTextBox_CSVLinesNumber.Text, out CSVLineNumberLimit);
            if (CSVLineNumberLimit < 1) CSVLineNumberLimit = 1;
            toolStripTextBox_CSVLinesNumber.Text = CSVLineNumberLimit.ToString();
        }

        private void LineBreakToolStripTextBox1_Leave(object sender, EventArgs e)
        {
            long.TryParse(LineBreakToolStripTextBox1.Text, out limitTick);
            limitTick = limitTick * 10000;
            LineBreakToolStripTextBox1.Text = (limitTick / 10000).ToString();
        }

        private void AutosaveCSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            autosaveCSVToolStripMenuItem1.Checked = !autosaveCSVToolStripMenuItem1.Checked;
        }

        public void CollectBuffer(string tmpBuffer, int state, string time)
        {
            if (tmpBuffer != "")
                lock (threadLock)
                {
                    if (!(txtOutState == state && DateTime.Now.Ticks - oldTicks < limitTick && state != Port1DataOut &&
                          state != Port2DataOut && state != Port3DataOut && state != Port4DataOut))
                    {
                        if (state == Port1DataIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname1 + "<< " + tmpBuffer;
                        }
                        else if (state == Port1DataOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname1 + ">> " + tmpBuffer;
                        }
                        else if (state == Port1SignalIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname1 + "<< " + tmpBuffer;
                        }
                        else if (state == Port1SignalOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname1 + ">> " + tmpBuffer;
                        }
                        else if (state == Port1Error)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname1 + tmpBuffer;
                        }

                        else if (state == Port2DataIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname2 + "<< " + tmpBuffer;
                        }
                        else if (state == Port2DataOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname2 + ">> " + tmpBuffer;
                        }
                        else if (state == Port2SignalIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname2 + "<< " + tmpBuffer;
                        }
                        else if (state == Port2SignalOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname2 + ">> " + tmpBuffer;
                        }
                        else if (state == Port2Error)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname2 + tmpBuffer;
                        }

                        else if (state == Port3DataIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname3 + "<< " + tmpBuffer;
                        }
                        else if (state == Port3DataOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname3 + ">> " + tmpBuffer;
                        }
                        else if (state == Port3SignalIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname3 + "<< " + tmpBuffer;
                        }
                        else if (state == Port3SignalOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname3 + ">> " + tmpBuffer;
                        }
                        else if (state == Port3Error)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname3 + tmpBuffer;
                        }

                        else if (state == Port4DataIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname4 + "<< " + tmpBuffer;
                        }
                        else if (state == Port4DataOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname4 + ">> " + tmpBuffer;
                        }
                        else if (state == Port4SignalIn)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname4 + "<< " + tmpBuffer;
                        }
                        else if (state == Port4SignalOut)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname4 + ">> " + tmpBuffer;
                        }
                        else if (state == Port4Error)
                        {
                            if (checkBox_insDir.Checked) tmpBuffer = portname4 + tmpBuffer;
                        }

                        if (checkBox_insTime.Checked) tmpBuffer = time + " " + tmpBuffer;
                        tmpBuffer = "\r\n" + tmpBuffer;
                        txtOutState = state;
                    }

                    if (autosaveTXTToolStripMenuItem1.Checked)
                        try
                        {
                            File.AppendAllText(terminaltxtToolStripMenuItem1.Text, tmpBuffer,
                                Encoding.GetEncoding(Settings.Default.CodePage));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + terminaltxtToolStripMenuItem1.Text + ": " +
                                            ex.Message);
                        }

                    if (logToTextToolStripMenuItem.Checked) SetText(tmpBuffer);

                    oldTicks = DateTime.Now.Ticks;
                }
        }

        public void CSVcollectBuffer(string tmpBuffer)
        {
            if (tmpBuffer != "")
                lock (threadLock)
                {
                    if (CSVLineCount >= CSVLineNumberLimit)
                    {
                        CSVFileName = DateTime.Today.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString() + "_" +
                                      DateTime.Now.Millisecond.ToString("D3") + ".csv";
                        CSVFileName = CSVFileName.Replace(':', '-').Replace('\\', '-').Replace('/', '-');
                        CSVLineCount = 0;
                    }

                    try
                    {
                        File.AppendAllText(CSVFileName, tmpBuffer, Encoding.GetEncoding(Settings.Default.CodePage));
                        CSVLineCount++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\r\nError opening file " + CSVFileName + ": " + ex.Message);
                    }
                }
        }

        public void CSVcollectGrid(DataRow tmpDataRow)
        {
            lock (threadLock)
            {
                CSVdataTable.Rows.Add(tmpDataRow);
            }
        }

        private delegate void SetPinCallback1(bool setPin);

        private void SetPinCD1(bool setPin)
        {
            if (checkBox_CD1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCD1);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CD1.Checked = setPin;
            }
        }

        private void SetPinDSR1(bool setPin)
        {
            if (checkBox_DSR1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinDSR1);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_DSR1.Checked = setPin;
            }
        }

        private void SetPinCTS1(bool setPin)
        {
            if (checkBox_CTS1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCTS1);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CTS1.Checked = setPin;
            }
        }

        private void SetPinRING1(bool setPin)
        {
            if (checkBox_RI1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinRING1);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_RI1.Checked = setPin;
            }
        }

        private void SetPinCD2(bool setPin)
        {
            if (checkBox_CD2.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCD2);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CD2.Checked = setPin;
            }
        }

        private void SetPinDSR2(bool setPin)
        {
            if (checkBox_DSR2.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinDSR2);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_DSR2.Checked = setPin;
            }
        }

        private void SetPinCTS2(bool setPin)
        {
            if (checkBox_CTS2.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCTS2);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CTS2.Checked = setPin;
            }
        }

        private void SetPinRING2(bool setPin)
        {
            if (checkBox_RI2.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinRING2);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_RI2.Checked = setPin;
            }
        }

        private void SetPinCD3(bool setPin)
        {
            if (checkBox_CD3.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCD3);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CD3.Checked = setPin;
            }
        }

        private void SetPinDSR3(bool setPin)
        {
            if (checkBox_DSR3.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinDSR3);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_DSR3.Checked = setPin;
            }
        }

        private void SetPinCTS3(bool setPin)
        {
            if (checkBox_CTS3.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCTS3);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CTS3.Checked = setPin;
            }
        }

        private void SetPinRING3(bool setPin)
        {
            if (checkBox_RI3.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinRING3);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_RI3.Checked = setPin;
            }
        }

        private void SetPinCD4(bool setPin)
        {
            if (checkBox_CD4.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCD4);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CD4.Checked = setPin;
            }
        }

        private void SetPinDSR4(bool setPin)
        {
            if (checkBox_DSR4.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinDSR4);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_DSR4.Checked = setPin;
            }
        }

        private void SetPinCTS4(bool setPin)
        {
            if (checkBox_CTS4.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCTS4);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_CTS4.Checked = setPin;
            }
        }

        private void SetPinRING4(bool setPin)
        {
            if (checkBox_RI4.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinRING4);
                BeginInvoke(d, setPin);
            }
            else
            {
                checkBox_RI4.Checked = setPin;
            }
        }
    }
}
