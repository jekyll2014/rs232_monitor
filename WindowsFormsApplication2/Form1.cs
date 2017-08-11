﻿using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dual_RS232_terminal
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = CSVdataTable;
            //create columns
            DataColumn colDate;
            colDate = new DataColumn("Date", typeof(string));
            DataColumn colTime;
            colTime = new DataColumn("Time", typeof(string));
            DataColumn colMilis;
            colMilis = new DataColumn("Milis", typeof(string));
            DataColumn colPort;
            colPort = new DataColumn("Port", typeof(string));
            DataColumn colDir;
            colDir = new DataColumn("Dir", typeof(string));
            DataColumn colData;
            colData = new DataColumn("Data", typeof(string));
            DataColumn colSig;
            colSig = new DataColumn("Signal", typeof(string));
            DataColumn colMark;
            colMark = new DataColumn("Mark", typeof(bool));
            //add columns to the table
            CSVdataTable.Columns.AddRange(new DataColumn[] { colDate, colTime, colMilis, colPort, colDir, colData, colSig, colMark });

            DataGridViewColumn column = dataGridView.Columns[0];
            //column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 70;
            column.Width = 70;

            column = dataGridView.Columns[1];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 55;
            column.Width = 55;

            column = dataGridView.Columns[2];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            column = dataGridView.Columns[3];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 40;

            column = dataGridView.Columns[4];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            column = dataGridView.Columns[5];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 200;
            column.Width = 250;

            column = dataGridView.Columns[6];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 60;
            column.Width = 60;

            column = dataGridView.Columns[7];
            column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            column.Resizable = DataGridViewTriState.True;
            column.MinimumWidth = 30;
            column.Width = 30;

            //load settings
            textBox_command.Text = RS232_monitor.Properties.Settings.Default.DefaultCommand;
            checkBox_commandhex.Checked = RS232_monitor.Properties.Settings.Default.DefaultCommandHex;
            textBox_params.Text = RS232_monitor.Properties.Settings.Default.DefaultParameter;
            checkBox_paramhex.Checked = RS232_monitor.Properties.Settings.Default.DefaultParamHex;
            checkBox_cr.Checked = RS232_monitor.Properties.Settings.Default.addCR;
            checkBox_lf.Checked = RS232_monitor.Properties.Settings.Default.addLF;
            checkBox_suff.Checked = RS232_monitor.Properties.Settings.Default.addSuff;
            textBox_suff.Text = RS232_monitor.Properties.Settings.Default.SuffText;
            checkBox_suffhex.Checked = RS232_monitor.Properties.Settings.Default.DefaultSuffHex;
            checkBox_insPin.Checked = RS232_monitor.Properties.Settings.Default.LogSignal;
            checkBox_insTime.Checked = RS232_monitor.Properties.Settings.Default.LogTime;
            checkBox_insDir.Checked = RS232_monitor.Properties.Settings.Default.LogDir;
            checkBox_portName.Checked = RS232_monitor.Properties.Settings.Default.LogPortName;
            checkBox_displayPort1hex.Checked = RS232_monitor.Properties.Settings.Default.HexPort1;
            checkBox_displayPort2hex.Checked = RS232_monitor.Properties.Settings.Default.HexPort2;
            checkBox_displayPort3hex.Checked = RS232_monitor.Properties.Settings.Default.HexPort3;
            checkBox_displayPort4hex.Checked = RS232_monitor.Properties.Settings.Default.HexPort4;
            textBox_port1Name.Text = RS232_monitor.Properties.Settings.Default.Port1Name;
            textBox_port2Name.Text = RS232_monitor.Properties.Settings.Default.Port2Name;
            textBox_port3Name.Text = RS232_monitor.Properties.Settings.Default.Port3Name;
            textBox_port4Name.Text = RS232_monitor.Properties.Settings.Default.Port4Name;
            logToGridToolStripMenuItem.Checked = RS232_monitor.Properties.Settings.Default.LogGrid;
            autoscrollToolStripMenuItem.Checked = RS232_monitor.Properties.Settings.Default.AutoScroll;
            lineWrapToolStripMenuItem.Checked = RS232_monitor.Properties.Settings.Default.LineWrap;
            autosaveTXTToolStripMenuItem1.Checked = RS232_monitor.Properties.Settings.Default.AutoLogTXT;
            terminaltxtToolStripMenuItem1.Text = RS232_monitor.Properties.Settings.Default.TXTlogFile;
            autosaveCSVToolStripMenuItem1.Checked = RS232_monitor.Properties.Settings.Default.AutoLogCSV;
            terminalcsvToolStripMenuItem1.Text = RS232_monitor.Properties.Settings.Default.CSVlogFile;
            LineBreakToolStripTextBox1.Text = RS232_monitor.Properties.Settings.Default.LineBreakTimeout.ToString();
            limitTick = RS232_monitor.Properties.Settings.Default.LineBreakTimeout * 10000;

            if (autosaveTXTToolStripMenuItem1.Checked == true) terminaltxtToolStripMenuItem1.Enabled = true;
            else terminaltxtToolStripMenuItem1.Enabled = false;

            //set the codepage to COM-port
            serialPort1.Encoding = Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage);
            serialPort2.Encoding = Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage);
            serialPort3.Encoding = Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage);
            serialPort4.Encoding = Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage);
            SerialPopulate();
        }

        private void button_openport_Click(object sender, EventArgs e)
        {
            checkBox_DTR1.Checked = false;
            checkBox_DTR2.Checked = false;
            checkBox_DTR3.Checked = false;
            checkBox_DTR4.Checked = false;
            checkBox_RTS1.Checked = false;
            checkBox_RTS2.Checked = false;
            checkBox_RTS3.Checked = false;
            checkBox_RTS4.Checked = false;

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
                serialPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox_handshake1.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_parity1.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_stopbits1.Text);
                serialPort1.ReadTimeout = RS232_monitor.Properties.Settings.Default.ReceiveTimeOut;
                serialPort1.WriteTimeout = RS232_monitor.Properties.Settings.Default.SendTimeOut;
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

                if (serialPort1.Handshake == Handshake.RequestToSend || serialPort1.Handshake == Handshake.RequestToSendXOnXOff)
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
                serialPort2.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox_handshake2.Text);
                serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_parity2.Text);
                serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_stopbits2.Text);
                serialPort2.ReadTimeout = RS232_monitor.Properties.Settings.Default.ReceiveTimeOut;
                serialPort2.WriteTimeout = RS232_monitor.Properties.Settings.Default.SendTimeOut;
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
                if (serialPort2.Handshake == Handshake.RequestToSend || serialPort2.Handshake == Handshake.RequestToSendXOnXOff)
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
                serialPort3.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox_handshake3.Text);
                serialPort3.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_parity3.Text);
                serialPort3.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_stopbits3.Text);
                serialPort3.ReadTimeout = RS232_monitor.Properties.Settings.Default.ReceiveTimeOut;
                serialPort3.WriteTimeout = RS232_monitor.Properties.Settings.Default.SendTimeOut;
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
                if (serialPort3.Handshake == Handshake.RequestToSend || serialPort3.Handshake == Handshake.RequestToSendXOnXOff)
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
                serialPort4.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox_handshake4.Text);
                serialPort4.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_parity4.Text);
                serialPort4.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_stopbits4.Text);
                serialPort4.ReadTimeout = RS232_monitor.Properties.Settings.Default.ReceiveTimeOut;
                serialPort4.WriteTimeout = RS232_monitor.Properties.Settings.Default.SendTimeOut;
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
                if (serialPort4.Handshake == Handshake.RequestToSend || serialPort4.Handshake == Handshake.RequestToSendXOnXOff)
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

            if (checkBox_sendPort1.Checked == false && checkBox_sendPort2.Checked == false && checkBox_sendPort3.Checked == false && checkBox_sendPort4.Checked == false) button_send.Enabled = false;
            else if (serialPort1.IsOpen == true || serialPort2.IsOpen == true || serialPort3.IsOpen == true || serialPort4.IsOpen == true) button_send.Enabled = true;
            checkBox_portName_CheckedChanged(this, EventArgs.Empty);
        }

        private void button_closeport_Click(object sender, EventArgs e)
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

        private void button_send_Click(object sender, EventArgs e)
        {
            if (textBox_senddata.Text != "")
            {
                string outStr = "";
                if (checkBox_sendPort1.Checked == true && serialPort1.IsOpen)
                {
                    DataRow dataRowTX1 = null;
                    //create new row in datatable
                    dataRowTX1 = CSVdataTable.NewRow();
                    dataRowTX1["Date"] = DateTime.Today.ToShortDateString();
                    dataRowTX1["Time"] = DateTime.Now.ToLongTimeString();
                    dataRowTX1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    dataRowTX1["Port"] = portname1;
                    dataRowTX1["Dir"] = "TX";
                    try
                    {
                        serialPort1.Write(ConvertHexToByte(textBox_senddata.Text), 0, textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                        dataRowTX1["Signal"] = "Error sending to port " + serialPort1.PortName + ": " + ex.Message;
                    }
                    dataRowTX1["Data"] = textBox_senddata.Text;
                    dataRowTX1["Mark"] = checkBox_Mark.Checked;
                    if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowTX1);
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX1["Date"] + " " + dataRowTX1["Time"] + "." + dataRowTX1["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname1 + ">> ";
                    if (checkBox_displayPort1hex.Checked == true) outStr += textBox_senddata.Text;
                    else outStr += ConvertHexToString(textBox_senddata.Text);
                    if (outStr != "") collectBuffer(outStr, Port1DataOut, dataRowTX1["Date"] + " " + dataRowTX1["Time"] + "." + dataRowTX1["Milis"]);
                    if (autosaveCSVToolStripMenuItem1.Checked == true && dataRowTX1["Data"].ToString() != "") CSVcollectBuffer(dataRowTX1["Date"] + "," + dataRowTX1["Time"] + "," + dataRowTX1["Milis"] + "," + dataRowTX1["Port"] + "," + dataRowTX1["Dir"] + "," + dataRowTX1["Data"] + "," + dataRowTX1["Signal"] + "," + dataRowTX1["Mark"] + "\r\n");
                }

                if (checkBox_sendPort2.Checked == true && serialPort2.IsOpen)
                {
                    DataRow dataRowTX2 = null;
                    //создаём новую строку
                    dataRowTX2 = CSVdataTable.NewRow();
                    dataRowTX2["Date"] = DateTime.Today.ToShortDateString();
                    dataRowTX2["Time"] = DateTime.Now.ToLongTimeString();
                    dataRowTX2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    dataRowTX2["Port"] = portname2;
                    dataRowTX2["Dir"] = "TX";
                    try
                    {
                        serialPort2.Write(ConvertHexToByte(textBox_senddata.Text), 0, textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX2["Signal"] = "Error sending to port " + serialPort2.PortName + ": " + ex.Message;

                    }
                    dataRowTX2["Data"] = textBox_senddata.Text;
                    dataRowTX2["Mark"] = checkBox_Mark.Checked;
                    if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowTX2);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort2hex.Checked == true) outStr += textBox_senddata.Text;
                    else outStr += ConvertHexToString(textBox_senddata.Text);
                    collectBuffer(outStr, Port2DataOut, dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"]);
                    if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowTX2["Date"] + "," + dataRowTX2["Time"] + "," + dataRowTX2["Milis"] + "," + dataRowTX2["Port"] + "," + dataRowTX2["Dir"] + "," + dataRowTX2["Data"] + "," + dataRowTX2["Signal"] + "," + dataRowTX2["Mark"] + "\r\n");
                }

                if (checkBox_sendPort3.Checked == true && serialPort3.IsOpen)
                {
                    DataRow dataRowTX3 = null;
                    //создаём новую строку
                    dataRowTX3 = CSVdataTable.NewRow();
                    dataRowTX3["Date"] = DateTime.Today.ToShortDateString();
                    dataRowTX3["Time"] = DateTime.Now.ToLongTimeString();
                    dataRowTX3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    dataRowTX3["Port"] = portname3;
                    dataRowTX3["Dir"] = "TX";
                    try
                    {
                        serialPort3.Write(ConvertHexToByte(textBox_senddata.Text), 0, textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX3["Signal"] = "Error sending to port " + serialPort3.PortName + ": " + ex.Message;

                    }
                    dataRowTX3["Data"] = textBox_senddata.Text;
                    dataRowTX3["Mark"] = checkBox_Mark.Checked;
                    if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowTX3);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort3hex.Checked == true) outStr += textBox_senddata.Text;
                    else outStr += ConvertHexToString(textBox_senddata.Text);
                    collectBuffer(outStr, Port3DataOut, dataRowTX3["Date"] + " " + dataRowTX3["Time"] + "." + dataRowTX3["Milis"]);
                    if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowTX3["Date"] + "," + dataRowTX3["Time"] + "," + dataRowTX3["Milis"] + "," + dataRowTX3["Port"] + "," + dataRowTX3["Dir"] + "," + dataRowTX3["Data"] + "," + dataRowTX3["Signal"] + "," + dataRowTX3["Mark"] + "\r\n");
                }

                if (checkBox_sendPort4.Checked == true && serialPort4.IsOpen)
                {
                    DataRow dataRowTX4 = null;
                    //создаём новую строку
                    dataRowTX4 = CSVdataTable.NewRow();
                    dataRowTX4["Date"] = DateTime.Today.ToShortDateString();
                    dataRowTX4["Time"] = DateTime.Now.ToLongTimeString();
                    dataRowTX4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
                    dataRowTX4["Port"] = portname4;
                    dataRowTX4["Dir"] = "TX";
                    try
                    {
                        serialPort4.Write(ConvertHexToByte(textBox_senddata.Text), 0, textBox_senddata.Text.Length / 3);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error sending to port " + serialPort2.PortName + ": " + ex.Message);
                        dataRowTX4["Signal"] = "Error sending to port " + serialPort4.PortName + ": " + ex.Message;

                    }
                    dataRowTX4["Data"] = textBox_senddata.Text;
                    dataRowTX4["Mark"] = checkBox_Mark.Checked;
                    if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowTX4);
                    outStr = "";
                    //if (checkBox_insTime.Checked == true) outStr += dataRowTX2["Date"] + " " + dataRowTX2["Time"] + "." + dataRowTX2["Milis"] + " ";
                    //if (checkBox_insDir.Checked == true) outStr += portname2 + ">> ";
                    if (checkBox_displayPort4hex.Checked == true) outStr += textBox_senddata.Text;
                    else outStr += ConvertHexToString(textBox_senddata.Text);
                    collectBuffer(outStr, Port4DataOut, dataRowTX4["Date"] + " " + dataRowTX4["Time"] + "." + dataRowTX4["Milis"]);
                    if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowTX4["Date"] + "," + dataRowTX4["Time"] + "," + dataRowTX4["Milis"] + "," + dataRowTX4["Port"] + "," + dataRowTX4["Dir"] + "," + dataRowTX4["Data"] + "," + dataRowTX4["Signal"] + "," + dataRowTX4["Mark"] + "\r\n");
                }
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] rx1 = new byte[RS232_monitor.Properties.Settings.Default.rxBuffer];
            DataRow dataRowRX1 = null;
            dataRowRX1 = CSVdataTable.NewRow();
            dataRowRX1["Date"] = DateTime.Today.ToShortDateString();
            dataRowRX1["Time"] = DateTime.Now.ToLongTimeString();
            dataRowRX1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowRX1["Port"] = portname1;
            dataRowRX1["Dir"] = "RX";
            dataRowRX1["Mark"] = checkBox_Mark.Checked;
            int i = 0;
            while (serialPort1.BytesToRead > 0)
            {
                try
                {
                    rx1[i] = (byte)serialPort1.ReadByte();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading port " + serialPort1.PortName + ": " + ex.Message);
                    dataRowRX1["Signal"] = "Error reading port " + serialPort1.PortName + ": " + ex.Message;
                }
                i++;
                if (i >= RS232_monitor.Properties.Settings.Default.rxBuffer)
                {
                    //MessageBox.Show(serialPort1.PortName + " receive buffer is full.\r\nFlushing data...");
                    dataRowRX1["Signal"] = "Receive buffer overflow";
                    //serialPort1.DiscardInBuffer();
                    break;
                }
            }
            dataRowRX1["Data"] = ConvertByteArrToHex(rx1, i);
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowRX1);
            string outStr1 = "";
            //if (checkBox_insTime.Checked == true) outStr1 += dataRowRX1["Date"] + " " + dataRowRX1["Time"] + "." + dataRowRX1["Milis"] + " ";
            //if (checkBox_insDir.Checked == true) outStr1 += portname1 + "<< ";
            if (checkBox_displayPort1hex.Checked == true) outStr1 += dataRowRX1["Data"];
            else outStr1 += System.Text.Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage).GetString(rx1, 0, i);
            collectBuffer(outStr1, Port1DataIn, dataRowRX1["Date"] + " " + dataRowRX1["Time"] + "." + dataRowRX1["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowRX1["Date"] + "," + dataRowRX1["Time"] + "," + dataRowRX1["Milis"] + "," + dataRowRX1["Port"] + "," + dataRowRX1["Dir"] + "," + dataRowRX1["Data"] + "," + dataRowRX1["Signal"] + "," + dataRowRX1["Mark"] + "\r\n");
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] rx2 = new byte[RS232_monitor.Properties.Settings.Default.rxBuffer];
            DataRow dataRowRX2 = null;
            dataRowRX2 = CSVdataTable.NewRow();
            dataRowRX2["Date"] = DateTime.Today.ToShortDateString();
            dataRowRX2["Time"] = DateTime.Now.ToLongTimeString();
            dataRowRX2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowRX2["Port"] = portname2;
            dataRowRX2["Dir"] = "RX";
            int i = 0;
            while (serialPort2.BytesToRead > 0)
            {
                try
                {
                    rx2[i] = (byte)serialPort2.ReadByte();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                    dataRowRX2["Signal"] = "Error reading port " + serialPort2.PortName + ": " + ex.Message;
                }
                i++;
                if (i >= RS232_monitor.Properties.Settings.Default.rxBuffer)
                {
                    //MessageBox.Show(serialPort2.PortName + " receive buffer is full.\r\nFlushing data...");
                    dataRowRX2["Signal"] = "Receive buffer overflow";
                    //serialPort2.DiscardInBuffer();
                    break;
                }
            }
            dataRowRX2["Data"] = ConvertByteArrToHex(rx2, i);
            dataRowRX2["Mark"] = checkBox_Mark.Checked;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowRX2);
            string outStr2 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //kif (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort2hex.Checked == true) outStr2 += dataRowRX2["Data"];
            else outStr2 += System.Text.Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage).GetString(rx2, 0, i);
            collectBuffer(outStr2, Port2DataIn, dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowRX2["Date"] + "," + dataRowRX2["Time"] + "," + dataRowRX2["Milis"] + "," + dataRowRX2["Port"] + "," + dataRowRX2["Dir"] + "," + dataRowRX2["Data"] + "," + dataRowRX2["Signal"] + "," + dataRowRX2["Mark"] + "\r\n");
        }

        private void serialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] rx3 = new byte[RS232_monitor.Properties.Settings.Default.rxBuffer];
            DataRow dataRowRX3 = null;
            dataRowRX3 = CSVdataTable.NewRow();
            dataRowRX3["Date"] = DateTime.Today.ToShortDateString();
            dataRowRX3["Time"] = DateTime.Now.ToLongTimeString();
            dataRowRX3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowRX3["Port"] = portname3;
            dataRowRX3["Dir"] = "RX";
            int i = 0;
            while (serialPort3.BytesToRead > 0)
            {
                try
                {
                    rx3[i] = (byte)serialPort3.ReadByte();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                    dataRowRX3["Signal"] = "Error reading port " + serialPort3.PortName + ": " + ex.Message;
                }
                i++;
                if (i >= RS232_monitor.Properties.Settings.Default.rxBuffer)
                {
                    //MessageBox.Show(serialPort2.PortName + " receive buffer is full.\r\nFlushing data...");
                    dataRowRX3["Signal"] = "Receive buffer overflow";
                    //serialPort3.DiscardInBuffer();
                    break;
                }
            }
            dataRowRX3["Data"] = ConvertByteArrToHex(rx3, i);
            dataRowRX3["Mark"] = checkBox_Mark.Checked;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowRX3);
            string outStr3 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //kif (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort3hex.Checked == true) outStr3 += dataRowRX3["Data"];
            else outStr3 += System.Text.Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage).GetString(rx3, 0, i);
            collectBuffer(outStr3, Port3DataIn, dataRowRX3["Date"] + " " + dataRowRX3["Time"] + "." + dataRowRX3["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowRX3["Date"] + "," + dataRowRX3["Time"] + "," + dataRowRX3["Milis"] + "," + dataRowRX3["Port"] + "," + dataRowRX3["Dir"] + "," + dataRowRX3["Data"] + "," + dataRowRX3["Signal"] + "," + dataRowRX3["Mark"] + "\r\n");
        }

        private void serialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] rx4 = new byte[RS232_monitor.Properties.Settings.Default.rxBuffer];
            DataRow dataRowRX4 = null;
            dataRowRX4 = CSVdataTable.NewRow();
            dataRowRX4["Date"] = DateTime.Today.ToShortDateString();
            dataRowRX4["Time"] = DateTime.Now.ToLongTimeString();
            dataRowRX4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowRX4["Port"] = portname4;
            dataRowRX4["Dir"] = "RX";
            int i = 0;
            while (serialPort4.BytesToRead > 0)
            {
                try
                {
                    rx4[i] = (byte)serialPort4.ReadByte();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error reading port " + serialPort2.PortName + ": " + ex.Message);
                    dataRowRX4["Signal"] = "Error reading port " + serialPort4.PortName + ": " + ex.Message;
                }
                i++;
                if (i >= RS232_monitor.Properties.Settings.Default.rxBuffer)
                {
                    //MessageBox.Show(serialPort2.PortName + " receive buffer is full.\r\nFlushing data...");
                    dataRowRX4["Signal"] = "Receive buffer overflow";
                    //serialPort4.DiscardInBuffer();
                    break;
                }
            }
            dataRowRX4["Data"] = ConvertByteArrToHex(rx4, i);
            dataRowRX4["Mark"] = checkBox_Mark.Checked;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowRX4);
            string outStr4 = "";
            //if (checkBox_insTime.Checked == true) outStr2 += dataRowRX2["Date"] + " " + dataRowRX2["Time"] + "." + dataRowRX2["Milis"] + " ";
            //kif (checkBox_insDir.Checked == true) outStr2 += portname2 + "<< ";
            if (checkBox_displayPort4hex.Checked == true) outStr4 += dataRowRX4["Data"];
            else outStr4 += System.Text.Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage).GetString(rx4, 0, i);
            collectBuffer(outStr4, Port4DataIn, dataRowRX4["Date"] + " " + dataRowRX4["Time"] + "." + dataRowRX4["Milis"]);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowRX4["Date"] + "," + dataRowRX4["Time"] + "," + dataRowRX4["Milis"] + "," + dataRowRX4["Port"] + "," + dataRowRX4["Dir"] + "," + dataRowRX4["Data"] + "," + dataRowRX4["Signal"] + "," + dataRowRX4["Mark"] + "\r\n");
        }

        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD1(serialPort1.CDHolding);
            SetPinDSR1(serialPort1.DsrHolding);
            SetPinCTS1(serialPort1.CtsHolding);
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN1["Port"] = portname1;
            dataRowPIN1["Dir"] = "SG";
            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort1.CDHolding == true && o_cd1 == false)
            {
                o_cd1 = true;
                outStr += "<" + portname1 + "_DCD^>";
            }
            else if (serialPort1.CDHolding == false && o_cd1 == true)
            {
                o_cd1 = false;
                outStr += "<" + portname1 + "_DCDv>";
            }
            //else outStr += "<" + portname1 + "_DCD?>";

            if (serialPort1.DsrHolding == true && o_dsr1 == false)
            {
                o_dsr1 = true;
                outStr += "<" + portname1 + "_DSR^>";
            }
            else if (serialPort1.DsrHolding == false && o_dsr1 == true)
            {
                o_dsr1 = false;
                outStr += "<" + portname1 + "_DSRv>";
            }
            //else outStr += "<" + portname1 + "_DSR?>";

            if (serialPort1.CtsHolding == true && o_cts1 == false)
            {
                o_cts1 = true;
                outStr += "<" + portname1 + "_CTS^>";
            }
            else if (serialPort1.CtsHolding == false && o_cts1 == true)
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
            if (outStr!="")
            {
                if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port1SignalIn, dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
                dataRowPIN1["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN1);
                if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," + dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," + dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
            }
        }

        private void serialPort2_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD2(serialPort2.CDHolding);
            SetPinDSR2(serialPort2.DsrHolding);
            SetPinCTS2(serialPort2.CtsHolding);
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN2["Port"] = portname2;
            dataRowPIN2["Dir"] = "SG";
            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            string outStr = "";

            if (serialPort2.CDHolding == true && o_cd2 == false)
            {
                o_cd2 = true;
                outStr += "<" + portname2 + "_DCD^>";
            }
            else if (serialPort2.CDHolding == false && o_cd2 == true)
            {
                o_cd2 = false;
                outStr += "<" + portname2 + "_DCDv>";
            }
            //else outStr += "<" + portname2 + "_DCD?>";

            if (serialPort2.DsrHolding == true && o_dsr2 == false)
            {
                o_dsr2 = true;
                outStr += "<" + portname2 + "_DSR^>";
            }
            else if (serialPort2.DsrHolding == false && o_dsr2 == true)
            {
                o_dsr2 = false;
                outStr += "<" + portname2 + "_DSRv>";
            }
            //else outStr += "<" + portname2 + "_DSR?>";

            if (serialPort2.CtsHolding == true && o_cts2 == false)
            {
                o_cts2 = true;
                outStr += "<" + portname2 + "_CTS^>";
            }
            else if (serialPort2.CtsHolding == false && o_cts2 == true)
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
                if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port2SignalIn, dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
                dataRowPIN2["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN2);
                if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," + dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," + dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
            }
        }

        private void serialPort3_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD3(serialPort3.CDHolding);
            SetPinDSR3(serialPort3.DsrHolding);
            SetPinCTS3(serialPort3.CtsHolding);
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN3["Port"] = portname3;
            dataRowPIN3["Dir"] = "SG";
            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort3.CDHolding == true && o_cd3 == false)
            {
                o_cd3 = true;
                outStr += "<" + portname3 + "_DCD^>";
            }
            else if (serialPort3.CDHolding == false && o_cd3 == true)
            {
                o_cd3 = false;
                outStr += "<" + portname3 + "_DCDv>";
            }
            //else outStr += "<" + portname3 + "_DCD?>";

            if (serialPort3.DsrHolding == true && o_dsr3 == false)
            {
                o_dsr3 = true;
                outStr += "<" + portname3 + "_DSR^>";
            }
            else if (serialPort3.DsrHolding == false && o_dsr3 == true)
            {
                o_dsr3 = false;
                outStr += "<" + portname3 + "_DSRv>";
            }
            //else outStr += "<" + portname3 + "_DSR?>";

            if (serialPort3.CtsHolding == true && o_cts3 == false)
            {
                o_cts3 = true;
                outStr += "<" + portname3 + "_CTS^>";
            }
            else if (serialPort3.CtsHolding == false && o_cts3 == true)
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
                if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port3SignalIn, dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
                dataRowPIN3["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN3);
                if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," + dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," + dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
            }
        }

        private void serialPort4_PinChanged(object sender, SerialPinChangedEventArgs e)////
        {
            SetPinCD4(serialPort4.CDHolding);
            SetPinDSR4(serialPort4.DsrHolding);
            SetPinCTS4(serialPort4.CtsHolding);
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN4["Port"] = portname4;
            dataRowPIN4["Dir"] = "SG";
            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort4.CDHolding == true && o_cd4 == false)
            {
                o_cd4 = true;
                outStr += "<" + portname4 + "_DCD^>";
            }
            else if (serialPort4.CDHolding == false && o_cd4 == true)
            {
                o_cd4 = false;
                outStr += "<" + portname4 + "_DCDv>";
            }
            //else outStr += "<" + portname4 + "_DCD?>";

            if (serialPort4.DsrHolding == true && o_dsr4 == false)
            {
                o_dsr4 = true;
                outStr += "<" + portname4 + "_DSR^>";
            }
            else if (serialPort4.DsrHolding == false && o_dsr4 == true)
            {
                o_dsr4 = false;
                outStr += "<" + portname4 + "_DSRv>";
            }
            //else outStr += "<" + portname4 + "_DSR?>";

            if (serialPort4.CtsHolding == true && o_cts4 == false)
            {
                o_cts4 = true;
                outStr += "<" + portname4 + "_CTS^>";
            }
            else if (serialPort4.CtsHolding == false && o_cts4 == true)
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
                if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port4SignalIn, dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
                dataRowPIN4["Signal"] = outStr;
                if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN4);
                if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," + dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," + dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
            }
        }

        private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port1 error: " + e.EventType);
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN1["Port"] = portname1;
            dataRowPIN1["Dir"] = "ER";
            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            string outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port1SignalIn, dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," + dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," + dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void serialPort2_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN2["Port"] = portname2;
            dataRowPIN2["Dir"] = "ER";
            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            string outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port2SignalIn, dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," + dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," + dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void serialPort3_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN3["Port"] = portname3;
            dataRowPIN3["Dir"] = "ER";
            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            string outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port3SignalIn, dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," + dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," + dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void serialPort4_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Port2 error: " + e.EventType);
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN4["Port"] = portname4;
            dataRowPIN4["Dir"] = "ER";
            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            string outStr = "<!" + portname1 + " error: " + e.EventType + "!>";
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port4SignalIn, dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," + dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," + dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void checkBox_DTR1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = checkBox_DTR1.Checked;
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN1["Port"] = portname1;
            dataRowPIN1["Dir"] = "User";
            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort1.DtrEnable == true && o_dtr1 == false)
            {
                o_dtr1 = true;
                outStr += "<" + portname1 + "_DTR^>";
            }
            else if (serialPort1.DtrEnable == false && o_dtr1 == true)
            {
                o_dtr1 = false;
                outStr += "<" + portname1 + "_DTRv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port1SignalOut, dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," + dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," + dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void checkBox_DTR2_CheckedChanged(object sender, EventArgs e)
        {
            serialPort2.DtrEnable = checkBox_DTR2.Checked;
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN2["Port"] = portname2;
            dataRowPIN2["Dir"] = "User";
            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort2.DtrEnable == true && o_dtr2 == false)
            {
                o_dtr2 = true;
                outStr += "<" + portname2 + "_DTR^>";
            }
            if (serialPort2.DtrEnable == false && o_dtr2 == true)
            {
                o_dtr2 = false;
                outStr += "<" + portname2 + "_DTRv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port2SignalOut, dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," + dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," + dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void checkBox_DTR3_CheckedChanged(object sender, EventArgs e)
        {
            serialPort3.DtrEnable = checkBox_DTR3.Checked;
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN3["Port"] = portname3;
            dataRowPIN3["Dir"] = "User";
            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort3.DtrEnable == true && o_dtr3 == false)
            {
                o_dtr3 = true;
                outStr += "<" + portname3 + "_DTR^>";
            }
            if (serialPort3.DtrEnable == false && o_dtr3 == true)
            {
                o_dtr3 = false;
                outStr += "<" + portname3 + "_DTRv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port3SignalOut, dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," + dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," + dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void checkBox_DTR4_CheckedChanged(object sender, EventArgs e)
        {
            serialPort4.DtrEnable = checkBox_DTR4.Checked;
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN4["Port"] = portname4;
            dataRowPIN4["Dir"] = "User";
            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort4.DtrEnable == true && o_dtr4 == false)
            {
                o_dtr4 = true;
                outStr += "<" + portname4 + "_DTR^>";
            }
            if (serialPort4.DtrEnable == false && o_dtr4 == true)
            {
                o_dtr4 = false;
                outStr += "<" + portname4 + "_DTRv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port4SignalOut, dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," + dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," + dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void checkBox_RTS1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.RtsEnable = checkBox_RTS1.Checked;
            DataRow dataRowPIN1 = null;
            dataRowPIN1 = CSVdataTable.NewRow();
            dataRowPIN1["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN1["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN1["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN1["Port"] = portname1;
            dataRowPIN1["Dir"] = "User";
            dataRowPIN1["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort1.RtsEnable == true && o_rts1 == false && serialPort1.Handshake != Handshake.RequestToSend && serialPort1.Handshake != Handshake.RequestToSendXOnXOff)
            {
                o_rts1 = true;
                outStr += "<" + portname1 + "_RTS^>";
            }
            else if (serialPort1.RtsEnable == false && o_rts1 == true)
            {
                o_rts1 = false;
                outStr += "<" + portname1 + "_RTSv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port1SignalOut, dataRowPIN1["Date"] + " " + dataRowPIN1["Time"] + "." + dataRowPIN1["Milis"]);
            dataRowPIN1["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN1);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN1["Date"] + "," + dataRowPIN1["Time"] + "," + dataRowPIN1["Milis"] + "," + dataRowPIN1["Port"] + "," + dataRowPIN1["Dir"] + "," + dataRowPIN1["Data"] + "," + dataRowPIN1["Signal"] + "," + dataRowPIN1["Mark"] + "\r\n");
        }

        private void checkBox_RTS2_CheckedChanged(object sender, EventArgs e)
        {
            serialPort2.RtsEnable = checkBox_RTS2.Checked;
            DataRow dataRowPIN2 = null;
            dataRowPIN2 = CSVdataTable.NewRow();
            dataRowPIN2["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN2["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN2["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN2["Port"] = portname2;
            dataRowPIN2["Dir"] = "User";
            dataRowPIN2["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort2.RtsEnable == true && o_rts2 == false)
            {
                o_rts2 = true;
                outStr += "<" + portname2 + "_RTS^>";
            }
            if (serialPort2.RtsEnable == false && o_rts2 == true)
            {
                o_rts2 = false;
                outStr += "<" + portname2 + "_RTSv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port2SignalOut, dataRowPIN2["Date"] + " " + dataRowPIN2["Time"] + "." + dataRowPIN2["Milis"]);
            dataRowPIN2["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN2);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN2["Date"] + "," + dataRowPIN2["Time"] + "," + dataRowPIN2["Milis"] + "," + dataRowPIN2["Port"] + "," + dataRowPIN2["Dir"] + "," + dataRowPIN2["Data"] + "," + dataRowPIN2["Signal"] + "," + dataRowPIN2["Mark"] + "\r\n");
        }

        private void checkBox_RTS3_CheckedChanged(object sender, EventArgs e)
        {
            serialPort3.RtsEnable = checkBox_RTS3.Checked;
            DataRow dataRowPIN3 = null;
            dataRowPIN3 = CSVdataTable.NewRow();
            dataRowPIN3["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN3["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN3["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN3["Port"] = portname3;
            dataRowPIN3["Dir"] = "User";
            dataRowPIN3["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort3.RtsEnable == true && o_rts3 == false)
            {
                o_rts3 = true;
                outStr += "<" + portname3 + "_RTS^>";
            }
            if (serialPort3.RtsEnable == false && o_rts3 == true)
            {
                o_rts3 = false;
                outStr += "<" + portname3 + "_RTSv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port3SignalOut, dataRowPIN3["Date"] + " " + dataRowPIN3["Time"] + "." + dataRowPIN3["Milis"]);
            dataRowPIN3["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN3);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN3["Date"] + "," + dataRowPIN3["Time"] + "," + dataRowPIN3["Milis"] + "," + dataRowPIN3["Port"] + "," + dataRowPIN3["Dir"] + "," + dataRowPIN3["Data"] + "," + dataRowPIN3["Signal"] + "," + dataRowPIN3["Mark"] + "\r\n");
        }

        private void checkBox_RTS4_CheckedChanged(object sender, EventArgs e)
        {
            serialPort4.RtsEnable = checkBox_RTS4.Checked;
            DataRow dataRowPIN4 = null;
            dataRowPIN4 = CSVdataTable.NewRow();
            dataRowPIN4["Date"] = DateTime.Today.ToShortDateString();
            dataRowPIN4["Time"] = DateTime.Now.ToLongTimeString();
            dataRowPIN4["Milis"] = DateTime.Now.Millisecond.ToString("D3");
            dataRowPIN4["Port"] = portname4;
            dataRowPIN4["Dir"] = "User";
            dataRowPIN4["Mark"] = checkBox_Mark.Checked;
            string outStr = "";
            if (serialPort4.RtsEnable == true && o_rts4 == false)
            {
                o_rts4 = true;
                outStr += "<" + portname4 + "_RTS^>";
            }
            if (serialPort4.RtsEnable == false && o_rts4 == true)
            {
                o_rts4 = false;
                outStr += "<" + portname4 + "_RTSv>";
            }
            if (checkBox_insPin.Checked == true) collectBuffer(outStr, Port4SignalOut, dataRowPIN4["Date"] + " " + dataRowPIN4["Time"] + "." + dataRowPIN4["Milis"]);
            dataRowPIN4["Signal"] = outStr;
            if (logToGridToolStripMenuItem.Checked == true) CSVcollectGrid(dataRowPIN4);
            if (autosaveCSVToolStripMenuItem1.Checked == true) CSVcollectBuffer(dataRowPIN4["Date"] + "," + dataRowPIN4["Time"] + "," + dataRowPIN4["Milis"] + "," + dataRowPIN4["Port"] + "," + dataRowPIN4["Dir"] + "," + dataRowPIN4["Data"] + "," + dataRowPIN4["Signal"] + "," + dataRowPIN4["Mark"] + "\r\n");
        }

        private void textBox_custom_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox_commandhex.Checked == true)
            {
                char c = e.KeyChar;
                if (c != '\b' && !((c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f') || (c >= '0' && c <= '9') || c == 0x08 || c == ' '))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox_params_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox_paramhex.Checked == true)
            {
                char c = e.KeyChar;
                if (c != '\b' && !((c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f') || (c >= '0' && c <= '9') || c == 0x08 || c == ' '))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox_suff_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (c != '\b' && !((c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f') || (c >= '0' && c <= '9') || c == 0x08 || c == ' '))
            {
                e.Handled = true;
            }
        }

        private void checkBox_suff_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_suff.Checked == true) textBox_suff.Enabled = false;
            else textBox_suff.Enabled = true;
            SendStringCollect();
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            SerialPopulate();
        }

        private void button_clear1_Click(object sender, EventArgs e)
        {
            textBox_terminal1.Clear();
            CSVdataTable.Rows.Clear();
        }

        /*private void textBox_terminal1_TextChanged(object sender, EventArgs e)
        {
            if (autoscrollToolStripMenuItem.Checked == true)
            {
                textBox_terminal1.SelectionStart = textBox_terminal1.Text.Length;
                textBox_terminal1.ScrollToCaret();
            }
        }*/

        private void comboBox_portname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname1.SelectedIndex != 0 && comboBox_portname1.SelectedIndex == comboBox_portname2.SelectedIndex) comboBox_portname1.SelectedIndex = 0;
            if (comboBox_portname1.SelectedIndex != 0 && comboBox_portname1.SelectedIndex == comboBox_portname3.SelectedIndex) comboBox_portname1.SelectedIndex = 0;
            if (comboBox_portname1.SelectedIndex != 0 && comboBox_portname1.SelectedIndex == comboBox_portname4.SelectedIndex) comboBox_portname1.SelectedIndex = 0;
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
            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 && comboBox_portname3.SelectedIndex == 0 && comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void comboBox_portname2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname2.SelectedIndex != 0 && comboBox_portname2.SelectedIndex == comboBox_portname1.SelectedIndex) comboBox_portname2.SelectedIndex = 0;
            if (comboBox_portname2.SelectedIndex != 0 && comboBox_portname2.SelectedIndex == comboBox_portname3.SelectedIndex) comboBox_portname2.SelectedIndex = 0;
            if (comboBox_portname2.SelectedIndex != 0 && comboBox_portname2.SelectedIndex == comboBox_portname4.SelectedIndex) comboBox_portname2.SelectedIndex = 0;
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
            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 && comboBox_portname3.SelectedIndex == 0 && comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void comboBox_portname3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname3.SelectedIndex != 0 && comboBox_portname3.SelectedIndex == comboBox_portname1.SelectedIndex) comboBox_portname3.SelectedIndex = 0;
            if (comboBox_portname3.SelectedIndex != 0 && comboBox_portname3.SelectedIndex == comboBox_portname2.SelectedIndex) comboBox_portname3.SelectedIndex = 0;
            if (comboBox_portname3.SelectedIndex != 0 && comboBox_portname3.SelectedIndex == comboBox_portname4.SelectedIndex) comboBox_portname3.SelectedIndex = 0;
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
            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 && comboBox_portname3.SelectedIndex == 0 && comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void comboBox_portname4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname4.SelectedIndex != 0 && comboBox_portname4.SelectedIndex == comboBox_portname1.SelectedIndex) comboBox_portname4.SelectedIndex = 0;
            if (comboBox_portname4.SelectedIndex != 0 && comboBox_portname4.SelectedIndex == comboBox_portname2.SelectedIndex) comboBox_portname4.SelectedIndex = 0;
            if (comboBox_portname4.SelectedIndex != 0 && comboBox_portname4.SelectedIndex == comboBox_portname3.SelectedIndex) comboBox_portname4.SelectedIndex = 0;
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
            if (comboBox_portname1.SelectedIndex == 0 && comboBox_portname2.SelectedIndex == 0 && comboBox_portname3.SelectedIndex == 0 && comboBox_portname4.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void checkBox_commandhex_CheckedChanged(object sender, EventArgs e)
        {
            string tmpstr = textBox_command.Text;
            if (checkBox_commandhex.Checked == true) textBox_command.Text = ConvertStringToHex(tmpstr);
            else textBox_command.Text = ConvertHexToString(tmpstr);
        }

        private void checkBox_paramhex_CheckedChanged(object sender, EventArgs e)
        {
            string tmpstr = textBox_params.Text;
            if (checkBox_paramhex.Checked == true) textBox_params.Text = ConvertStringToHex(tmpstr);
            else textBox_params.Text = ConvertHexToString(tmpstr);
        }

        private void checkBox_send_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_sendPort1.Checked == false && checkBox_sendPort2.Checked == false && checkBox_sendPort3.Checked == false && checkBox_sendPort4.Checked == false) button_send.Enabled = false;
            else if (serialPort1.IsOpen == true || serialPort2.IsOpen == true || serialPort3.IsOpen == true || serialPort4.IsOpen == true) button_send.Enabled = true;
        }

        private void textBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_commandhex.Checked == true) textBox_command.Text = checkHexString(textBox_command.Text);
            SendStringCollect();
        }

        private void textBox_params_Leave(object sender, EventArgs e)
        {
            if (checkBox_paramhex.Checked == true) textBox_params.Text = checkHexString(textBox_params.Text);
            SendStringCollect();
        }

        private void textBox_suff_Leave(object sender, EventArgs e)
        {
            if (checkBox_suffhex.Checked == true) textBox_suff.Text = checkHexString(textBox_suff.Text);
            SendStringCollect();
        }

        private void checkBox_cr_CheckedChanged(object sender, EventArgs e)
        {
            SendStringCollect();
        }

        private void checkBox_lf_CheckedChanged(object sender, EventArgs e)
        {
            SendStringCollect();
        }

        private void checkBox_suffhex_CheckedChanged(object sender, EventArgs e)
        {
            string tmpstr = textBox_suff.Text;
            if (checkBox_suffhex.Checked == true) textBox_suff.Text = ConvertStringToHex(tmpstr);
            else textBox_suff.Text = ConvertHexToString(tmpstr);
        }

        private void checkBox_portName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_portName.Checked == true)
            {
                if (button_closeport.Enabled == true)
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

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (saveFileDialog.Title == "Save .TXT log as...")
            {
                try
                {
                    File.AppendAllText(saveFileDialog.FileName, textBox_terminal1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to file " + saveFileDialog.FileName + ": " + ex.Message);
                }
            }
            if (saveFileDialog.Title == "Save .CSV log as...")
            {
                int columnCount = dataGridView.ColumnCount;
                string output = "";
                for (int i = 0; i < columnCount; i++)
                {
                    output += dataGridView.Columns[i].Name.ToString() + ",";
                }
                output += "\r\n";
                for (int i = 1; (i - 1) < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        output += dataGridView.Rows[i - 1].Cells[j].Value.ToString() + ",";
                    }
                    output += "\r\n";
                }
                try
                {
                    File.AppendAllText(saveFileDialog.FileName, output, Encoding.GetEncoding(RS232_monitor.Properties.Settings.Default.CodePage));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to file " + saveFileDialog.FileName + ": " + ex.Message);
                }
            }
        }

        private void saveTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save .TXT log as...";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text files|*.txt|All files|*.*";
            saveFileDialog.FileName = "terminal_" + DateTime.Today.ToShortDateString().Replace("/", "_") + ".txt";
            saveFileDialog.ShowDialog();
        }

        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save .CSV log as...";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            saveFileDialog.FileName = "terminal_" + DateTime.Today.ToShortDateString().Replace("/", "_") + ".csv";
            saveFileDialog.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RS232 Monitor\r\n(c) Kalugin Andrey\r\nContact: jekyll@mail.ru");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveParametersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RS232_monitor.Properties.Settings.Default.DefaultCommand = textBox_command.Text;
            RS232_monitor.Properties.Settings.Default.DefaultCommandHex = checkBox_commandhex.Checked;
            RS232_monitor.Properties.Settings.Default.DefaultParameter = textBox_params.Text;
            RS232_monitor.Properties.Settings.Default.DefaultParamHex = checkBox_paramhex.Checked;
            RS232_monitor.Properties.Settings.Default.addCR = checkBox_cr.Checked;
            RS232_monitor.Properties.Settings.Default.addLF = checkBox_lf.Checked;
            RS232_monitor.Properties.Settings.Default.addSuff = checkBox_suff.Checked;
            RS232_monitor.Properties.Settings.Default.SuffText = textBox_suff.Text;
            RS232_monitor.Properties.Settings.Default.DefaultSuffHex = checkBox_suffhex.Checked;
            RS232_monitor.Properties.Settings.Default.LogSignal = checkBox_insPin.Checked;
            RS232_monitor.Properties.Settings.Default.LogTime = checkBox_insTime.Checked;
            RS232_monitor.Properties.Settings.Default.LogDir = checkBox_insDir.Checked;
            RS232_monitor.Properties.Settings.Default.LogPortName = checkBox_portName.Checked;
            RS232_monitor.Properties.Settings.Default.HexPort1 = checkBox_displayPort1hex.Checked;
            RS232_monitor.Properties.Settings.Default.HexPort2 = checkBox_displayPort2hex.Checked;
            RS232_monitor.Properties.Settings.Default.HexPort3 = checkBox_displayPort3hex.Checked;
            RS232_monitor.Properties.Settings.Default.HexPort4 = checkBox_displayPort4hex.Checked;
            RS232_monitor.Properties.Settings.Default.Port1Name = textBox_port1Name.Text;
            RS232_monitor.Properties.Settings.Default.Port2Name = textBox_port2Name.Text;
            RS232_monitor.Properties.Settings.Default.Port3Name = textBox_port3Name.Text;
            RS232_monitor.Properties.Settings.Default.Port4Name = textBox_port4Name.Text;
            RS232_monitor.Properties.Settings.Default.LogGrid = logToGridToolStripMenuItem.Checked;
            RS232_monitor.Properties.Settings.Default.LogText = logToTextToolStripMenuItem.Checked;
            RS232_monitor.Properties.Settings.Default.AutoScroll = autoscrollToolStripMenuItem.Checked;
            RS232_monitor.Properties.Settings.Default.LineWrap = lineWrapToolStripMenuItem.Checked;
            RS232_monitor.Properties.Settings.Default.AutoLogTXT = autosaveTXTToolStripMenuItem1.Checked;
            RS232_monitor.Properties.Settings.Default.TXTlogFile = terminaltxtToolStripMenuItem1.Text;
            RS232_monitor.Properties.Settings.Default.AutoLogCSV = autosaveCSVToolStripMenuItem1.Checked;
            RS232_monitor.Properties.Settings.Default.CSVlogFile = terminalcsvToolStripMenuItem1.Text;
            RS232_monitor.Properties.Settings.Default.LineBreakTimeout = limitTick / 10000;
            RS232_monitor.Properties.Settings.Default.Save();
        }

        private void autosaveTXTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (autosaveTXTToolStripMenuItem1.Checked == true)
            {
                autosaveTXTToolStripMenuItem1.Checked = false;
                terminaltxtToolStripMenuItem1.Enabled = true;
            }
            else
            {
                autosaveTXTToolStripMenuItem1.Checked = true;
                terminaltxtToolStripMenuItem1.Enabled = false;
            }
        }

        private void autosaveCSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (autosaveCSVToolStripMenuItem1.Checked == true)
            {
                autosaveCSVToolStripMenuItem1.Checked = false;
                terminalcsvToolStripMenuItem1.Enabled = true;
            }
            else
            {
                autosaveCSVToolStripMenuItem1.Checked = true;
                terminalcsvToolStripMenuItem1.Enabled = false;
            }
        }

        private void lineWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lineWrapToolStripMenuItem.Checked == true) lineWrapToolStripMenuItem.Checked = false;
            else lineWrapToolStripMenuItem.Checked = true;
            textBox_terminal1.WordWrap = lineWrapToolStripMenuItem.Checked;
        }

        private void autoscrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autoscrollToolStripMenuItem.Checked == true) autoscrollToolStripMenuItem.Checked = false;
            else autoscrollToolStripMenuItem.Checked = true;
        }

        private void logToTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logToTextToolStripMenuItem.Checked == true)
            {
                logToTextToolStripMenuItem.Checked = false;
                ((Control)this.tabPage2).Enabled = false;
                if (logToGridToolStripMenuItem.Checked == false)
                {
                    tabControl1.Enabled = false;
                    tabControl1.Visible = false;
                }
            }
            else
            {
                logToTextToolStripMenuItem.Checked = true;
                ((Control)this.tabPage2).Enabled = true;
                tabControl1.Enabled = true;
                tabControl1.Visible = true;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage1 && logToTextToolStripMenuItem.Checked == false)
                e.Cancel = true;
            if (e.TabPage == tabPage2 && logToGridToolStripMenuItem.Checked == false)
                e.Cancel = true;
        }

        private void logToGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logToGridToolStripMenuItem.Checked == true)
            {
                logToGridToolStripMenuItem.Checked = false;
                ((Control)this.tabPage1).Enabled = false;
                if (logToTextToolStripMenuItem.Checked == false)
                {
                    tabControl1.Enabled = false;
                    tabControl1.Visible = false;
                }
            }
            else
            {
                logToGridToolStripMenuItem.Checked = true;
                ((Control)this.tabPage1).Enabled = true;
                tabControl1.Enabled = true;
                tabControl1.Visible = true;
            }
        }

        private void checkBox_Mark_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Mark.Checked == true) checkBox_Mark.Font = new Font(checkBox_Mark.Font, FontStyle.Bold);
            else checkBox_Mark.Font = new Font(checkBox_Mark.Font, FontStyle.Regular);
        }

        private void textBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_send.Enabled == true)
                if (e.KeyData == Keys.Return)
                    button_send_Click(textBox_command, EventArgs.Empty);
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            long.TryParse(LineBreakToolStripTextBox1.Text, out limitTick);
            limitTick = limitTick * 10000;
        }

        private void toolStripMenuItem_onlyData_Click(object sender, EventArgs e)
        {
            toolStripMenuItem_onlyData.Checked = !toolStripMenuItem_onlyData.Checked;

            if (toolStripMenuItem_onlyData.Checked == false)
            {
                this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
                this.serialPort1.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort1_PinChanged);

                this.serialPort2.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort2_ErrorReceived);
                this.serialPort2.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort2_PinChanged);

                this.serialPort3.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort3_ErrorReceived);
                this.serialPort3.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort3_PinChanged);

                this.serialPort4.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort4_ErrorReceived);
                this.serialPort4.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort4_PinChanged);
            }
            else
            {
                this.serialPort1.ErrorReceived -= this.serialPort1_ErrorReceived;
                this.serialPort1.PinChanged -= this.serialPort1_PinChanged;

                this.serialPort2.ErrorReceived -= this.serialPort2_ErrorReceived;
                this.serialPort2.PinChanged -= this.serialPort2_PinChanged;

                this.serialPort3.ErrorReceived -= this.serialPort3_ErrorReceived;
                this.serialPort3.PinChanged -= this.serialPort3_PinChanged;

                this.serialPort4.ErrorReceived -= this.serialPort4_ErrorReceived;
                this.serialPort4.PinChanged -= this.serialPort4_PinChanged;
            }
        }
    }
}