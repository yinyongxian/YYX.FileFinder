using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace YYX.FileFinder.Tools
{
    public static class IpaddressHelper
    {
        public static void DisplayIpaddress()
        {
            try
            {
                const string linkDescription = @"您可以通过以下链接访问：";
                Log4Log.Info(linkDescription);
                ContentLog.WriteSite(linkDescription);

                var hostName = Dns.GetHostName();
                var ipHostEntry = Dns.GetHostEntry(hostName);
                ipHostEntry
                    .AddressList
                    .Where(item => item.AddressFamily == AddressFamily.InterNetwork)
                    .ToList()
                    .ForEach(item =>
                    {
                        var url = $@"http://{item}:12321";
                        Log4Log.Info(url);
                        ContentLog.WriteSite(url);
                    });
            }
            catch (Exception ex)
            {
                var exceptionMessage = @"获取本机IP出错:" + ex.Message;
                Log4Log.Info(exceptionMessage);
                ContentLog.WriteLine(exceptionMessage);
            }
        }
    }
}
