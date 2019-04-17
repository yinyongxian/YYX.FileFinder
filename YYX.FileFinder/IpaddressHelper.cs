using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace YYX.FileFinder
{
    public static class IpaddressHelper
    {
        public static void DisplayIpaddress()
        {
            try
            {
                ContentLog.WriteLine(@"您可以通过以下链接访问：");
                var hostName = Dns.GetHostName();
                var ipHostEntry = Dns.GetHostEntry(hostName);
                ipHostEntry
                    .AddressList
                    .Where(item => item.AddressFamily == AddressFamily.InterNetwork)
                    .ToList()
                    .ForEach(item => ContentLog.WriteLine(string.Format(@"http://{0}:12321", item.ToString())));
            }
            catch (Exception ex)
            {
                ContentLog.WriteLine(@"获取本机IP出错:" + ex.Message);
            }
        }
    }
}
