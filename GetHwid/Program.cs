using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace GetHwid
{
    internal class Program
    {
        public static string GetProcessorID()
        {
            string IDNumber = "";
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                IDNumber += managementObject["ProcessorID"].ToString().Replace(" ", "");
            }
            return IDNumber;
        }

        public static string GetMotherBoardSerial()
        {
            string SerialNumber = "";
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                SerialNumber += managementObject["SerialNumber"].ToString().Replace(" ", "");
            }
            return SerialNumber;
        }

        public static string GetHDDSerial()
        {
            string SerialNumber = "";
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                SerialNumber += managementObject["SerialNumber"].ToString().Replace(" ", "") + "|";
            }
            return SerialNumber;
        }

        public static string GetCustomHwid()
        {
            string CustomHwid = GetProcessorID() + "(!*!)" + GetMotherBoardSerial() + "(!*!)" + GetHDDSerial();
            var stringBuilder = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(CustomHwid);
            foreach (var i in bytes) {
                stringBuilder.Append(i.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Custom Hwid: " + GetCustomHwid());
        }
    }
}
