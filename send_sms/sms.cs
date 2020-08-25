using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.IO.Ports;

namespace send_sms
{
    class sms
    {
        private string port;
        private string number;
        private string message;

        public sms (string port, string number, string message)
        {
            this.port = port;
            this.number = number;
            this.message = message;
        }

        public SerialPort OpenPort()
        {
            SerialPort sp = new SerialPort();
            sp.PortName = port;
            sp.Open();
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            return sp;
        }

        public bool SendSMS()
        {
            try
            {
                SerialPort sp = OpenPort();
                /*if (sp.IsOpen)
                {
                    sp.Close();
                    return true;
                }
                else
                {
                    sp.Close();
                    return false;
                }*/
                sp.WriteLine("AT" + Environment.NewLine);
                Thread.Sleep(100);
                sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
                Thread.Sleep(100);
                sp.WriteLine("AT+CMGS=\"" + number + "\"" + Environment.NewLine);
                Thread.Sleep(100);
                sp.WriteLine(message + Environment.NewLine);
                Thread.Sleep(100);
                sp.Write(new byte[] { 26 }, 0, 1);
                Thread.Sleep(100);
                var response = sp.ReadExisting();
                if (response.Contains("ERROR"))
                {
                    sp.Close();
                    return false;
                }
                else
                {
                    sp.Close();
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
