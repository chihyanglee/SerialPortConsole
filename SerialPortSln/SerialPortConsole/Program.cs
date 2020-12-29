using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace SerialPortConsole
{
    class Program
    {
        #region serial port setting
        private static readonly string portName = "COM3";
        private static readonly int baudRate = 115200;
        private static readonly Parity parity = Parity.None;
        private static readonly int dataBits = 8;
        private static readonly StopBits stopBits = StopBits.None;
        private static readonly SerialPort port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        #endregion
        private static readonly int BUFFER_SIZE = 1024;
        private static readonly int SLEEP_INTERVAL = 16;
        private static byte[] buffer = new byte[BUFFER_SIZE];
        static void Main(string[] _)
        {
            port.ReadBufferSize = 1024;
            port.ReadTimeout = 0;
            port.WriteBufferSize = 1024;
            port.WriteTimeout = 0;

            port.Open();
            if (port.IsOpen)
            {
                while(true)
                {
                    if (port.BytesToRead > 0)
                    {
                        var readLength = port.Read(buffer, 0, buffer.Length);
                        Array.Resize(ref buffer, readLength);
                        var content = Encoding.ASCII.GetString(buffer);
                        Console.WriteLine(content);
                    }

                    Thread.Sleep(SLEEP_INTERVAL);
                }
            }
        }
    }
}
