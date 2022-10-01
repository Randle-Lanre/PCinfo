// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Spectre.Console;

var pcName = Environment.MachineName;
var username = Environment.UserName;
var os = Environment.OSVersion.ToString();

#region DNS detials

var connected = NetworkInterface.GetIsNetworkAvailable();

#endregion-DNS dtails for Ip address

#region ipaddress

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

var firstOrDefaultWirelessAddress = GetAllLocalIPv4Addresses(NetworkInterfaceType.Wireless80211).FirstOrDefault();
var firstOrDefaultLan = GetAllLocalIPv4Addresses(NetworkInterfaceType.Ethernet).FirstOrDefault();

// IEnumerable<string> allwifiEnumerable = GetAllLocalIPv4Addresses(NetworkInterfaceType.Ethernet).ToList();
//
// foreach (var wifi in allwifiEnumerable)
// {
//     Console.WriteLine(wifi);
// }




#endregion


#region uptime

static string GetUpTime()
{
    var timeInMilliseconds=  TimeSpan.FromMilliseconds(GetTickCount64());
    var hr = timeInMilliseconds.Hours;
    var minutes = timeInMilliseconds.Minutes;
    var seconds = timeInMilliseconds.Seconds;
    string newDateFormat = $"{hr}:{minutes}:{seconds}";

    return newDateFormat;
}

[DllImport("kernel32")]
static extern ulong GetTickCount64();


var uptime = GetUpTime();



#endregion


// create table 

var table = new Table();

//setup for the columns
table.AddColumn("InfoType");
table.AddColumn(new TableColumn("Value").Centered());


//row setup

table.AddRow(new Markup("PC name "), new Markup($"[green] {pcName} [/]"));
table.AddRow(new Markup("Username"), new Markup($"[green] {username} [/]"));
table.AddRow(new Markup("Wifi"), new Markup($"[green]{firstOrDefaultWirelessAddress} [/]"));
table.AddRow(new Markup("LAN"), new Markup($"[green]{firstOrDefaultLan} [/]"));
// table.AddRow(new Markup("My IP address"), new Markup("[green] 0.0.0.0[/]"));
table.AddRow(new Markup("connected"), new Markup($"[green] {connected} [/]"));
// table.AddRow(new Markup("UP Time"), new Markup("[green] 3hrs 45 mins [/]"));
table.AddRow(new Markup("Uptime"), new Markup($"[green]{uptime}[/]"));
table.AddRow(new Markup("Used Storage Space"), new Markup("[green]Available storage space is 25% [/]"));
table.AddRow(new Markup("Ram usage"), new Markup("[green] 60% [/]"));
table.AddRow(new Markup("CPU usage"), new Markup("[green] 60% [/]"));
table.AddRow(new Markup("Window version"), new Markup($"[green] {os} [/]"));


//change table apearance
table.Border(TableBorder.Rounded);
table.BorderColor(Color.Yellow);
table.Columns[1].PadLeft(3).PadRight(6);
table.Centered();


Console.WriteLine("\n");
AnsiConsole.Write(table);
Console.ReadKey();