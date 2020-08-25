using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO.Ports;
using System.Data.OleDb;

namespace send_sms
{
    public partial class Form1 : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/Message.accdb");
        SerialPort sp = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:/Programming/GitHub/Send_SMS/send_sms/send_sms/Message.accdb");
            string sql = "Insert into Comm (Receiver,Contenu) Values ('"+ number.Text +"','"+ message.Text +"')";
            sms SMS = new sms(com.Text, number.Text, message.Text);
            if (SMS.SendSMS())
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.InsertCommand = cmd;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Record Submitted");
                con.Close();
            }
            else MessageBox.Show("com not open !");
        }
    }
}
