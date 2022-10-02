using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PCinfo;

public static class IpAddressInformation
{
    static string[] GetAllLocalIPv4Addresses(NetworkInterfaceType interfaceType)
    {
        List<string> ipAddrList = new List<string>();
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (item.NetworkInterfaceType == interfaceType && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddrList.Add(ip.Address.ToString());
                    }
                }
            }
        }
        return ipAddrList.ToArray();
    }
    
   public static readonly string? FirstOrDefaultWirelessAddress = GetAllLocalIPv4Addresses(NetworkInterfaceType.Wireless80211).FirstOrDefault();
    public static readonly string? FirstOrDefaultLan = GetAllLocalIPv4Addresses(NetworkInterfaceType.Ethernet).FirstOrDefault();



}