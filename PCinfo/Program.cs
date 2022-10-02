// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Spectre.Console;
using System.Diagnostics;


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




string uptime = GetUpTime();



#endregion

#region CPU and RAM

PerformanceCounter cpuCounter;
PerformanceCounter ramCounter;
#pragma warning disable
cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
ramCounter = new PerformanceCounter("Memory", "Available MBytes");


float CupValue()
{
    cpuCounter.NextValue();
    Thread.Sleep(500);
   return cpuCounter.NextValue();
}



#endregion


#region Free drive space

long GetTotalFreeSpace(string driveName = "C:\\")
{
    foreach (DriveInfo drive in DriveInfo.GetDrives())
    {
        if (drive.IsReady && drive.Name == driveName)
        {
            return drive.AvailableFreeSpace;
        }
    }
    return -1;
}


//--
 static string FormatFileSize(long bytes)
{
    var unit = 1024;
    if (bytes < unit) { return $"{bytes} B"; }

    var exp = (int)(Math.Log(bytes) / Math.Log(unit));
    return $"{bytes / Math.Pow(unit, exp):F2} {("KMGTPE")[exp - 1]}B";
}




#endregion


// create table 

var table = new Table();

//setup for the columns
table.AddColumn("InfoType");
table.AddColumn(new TableColumn("Value").Centered());





//change table apearance
table.Border(TableBorder.Rounded);
table.BorderColor(Color.Yellow);
table.Columns[1].PadLeft(3).PadRight(6);
table.Centered();
table.Collapse();


Console.WriteLine("\n");
// AnsiConsole.Write(table);

  AnsiConsole.Live(table).Start( x =>
{
//row setup

    table.AddRow(new Markup("PC name "), new Markup($"[green] {pcName} [/]"));
    table.AddRow(new Markup("Username"), new Markup($"[green] {username} [/]"));
    table.AddRow(new Markup("Wifi"), new Markup($"[green]{firstOrDefaultWirelessAddress} [/]"));
    table.AddRow(new Markup("LAN"), new Markup($"[green]{firstOrDefaultLan} [/]"));

    table.AddRow(new Markup("connected"), new Markup($"[green] {connected} [/]"));

    
    table.AddRow(new Markup("Uptime"), new Markup($"[green]{uptime}[/]"));

    table.AddRow(new Markup("Used Storage Space"), new Markup($"[green]Available storage space is {FormatFileSize(GetTotalFreeSpace())} [/]"));
    table.AddRow(new Markup("Ram usage"), new Markup($"[green] {ramCounter.NextValue()} MB [/]"));
    table.AddRow(new Markup("CPU usage"), new Markup($"[green] {Math.Round(CupValue(), 1)} % [/]"));
    table.AddRow(new Markup("Window version"), new Markup($"[green] {os} [/]"));
    x.Refresh();
    Thread.Sleep(1000);
});



// Console.ReadLine();