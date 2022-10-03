using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PCinfo;

public static class IpAddressInformation
{
    public static readonly string? FirstOrDefaultWirelessAddress =
        GetAllLocalIPv4Addresses(NetworkInterfaceType.Wireless80211).FirstOrDefault();

    public static readonly string? FirstOrDefaultLan =
        GetAllLocalIPv4Addresses(NetworkInterfaceType.Ethernet).FirstOrDefault();

    private static string[] GetAllLocalIPv4Addresses(NetworkInterfaceType interfaceType)
    {
        var ipAddrList = new List<string>();
        foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            if (item.NetworkInterfaceType == interfaceType && item.OperationalStatus == OperationalStatus.Up)
                foreach (var ip in item.GetIPProperties().UnicastAddresses)
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        ipAddrList.Add(ip.Address.ToString());
        return ipAddrList.ToArray();
    }
}