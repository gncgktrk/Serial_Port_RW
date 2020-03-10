using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace serial_port_read_data_send
{
    public partial class Form1 : Form
    {

        string dataOUT;
        string data;
        int dataint;
        Char datachar;
        string datastring;
        public Form1()
        {
            InitializeComponent();


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                int Br = Convert.ToInt16(comboBox2.Text);
                serialPort1.BaudRate = Br;
                serialPort1.Open();
                button2.Enabled = false;
                button3.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Port not Found");
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);


            string[] bauds = { "9600", "14400", "19200", "38400", "57600", "115200", "128000" };
            foreach (string baud in bauds)
                comboBox2.Items.Add(baud);

            button3.Enabled = false;

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            dataint = Convert.ToInt16(data);
            datachar = Convert.ToChar(data);
            datastring = Convert.ToString(datachar);
            
            this.Invoke(new EventHandler(displayData_event));
        }
        private void displayData_event(object sender, EventArgs e)
        {

            richTextBox1.Text = datastring;
            label3.Text = datastring;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Load += new EventHandler(Form1_Load);
            serialPort1.Close();
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {


            if (serialPort1.IsOpen)
            {
                dataOUT = textBox1.Text;
                serialPort1.Write(dataOUT);
                textBox1.Clear();
            }

            else
            {
                MessageBox.Show("connection lost");
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}


