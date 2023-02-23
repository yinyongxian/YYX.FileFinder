using System;
using System.Linq;
using NetFwTypeLib;

namespace YYX.FileFinder.Tools
{
 
    public static class INetFwManger
    {
        public static void NetFwAddPorts(string name, int port, string protocol)
        {
            try
            {
                INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
                INetFwOpenPort objPort = (INetFwOpenPort)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwOpenPort"));
                objPort.Name = name;
                objPort.Port = port;
                objPort.Protocol = protocol.ToUpper() == "TCP"
                    ? NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP
                    : NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                objPort.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
                objPort.Enabled = true;
                bool exist = netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Cast<INetFwOpenPort>().Any(item => item == objPort);
                if (!exist)
                {
                    netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(objPort);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void NetFwAddApps(string name, string executablePath)
        {
            try
            {
                INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
                INetFwAuthorizedApplication app = (INetFwAuthorizedApplication)Activator.CreateInstance(
                    Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication"));
                app.Name = name;
                app.ProcessImageFileName = executablePath;
                app.Enabled = true;
                netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
                bool exist = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Cast<INetFwAuthorizedApplication>().Any(mApp => mApp == app);
                if (!exist)
                {
                    netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
                }
            }
            catch { }
        }

        public static void NetFwDelApps(int port, string protocol)
        {
            try
            {
                INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
                var netFwIpProtocol = protocol == "TCP"
                    ? NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP
                    : NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(port, netFwIpProtocol);
            }
            catch { }
        }

        public static void NetFwDelApps(string executablePath)
        {
            try
            {
                INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
                netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(executablePath);
            }
            catch { }
        }
    }
}
