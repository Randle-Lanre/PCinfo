// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.NetworkInformation;
using PCinfo;
using Spectre.Console;

var pcName = Environment.MachineName;
var username = Environment.UserName;
var os = Environment.OSVersion.ToString();


#region DNS detials

var connected = NetworkInterface.GetIsNetworkAvailable();

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





// create table 
var table = new Table();

//setup for the columns
table.AddColumn("Info");
table.AddColumn(new TableColumn("Value").Centered());


//change table appearance
table.Border(TableBorder.Double);
table.BorderColor(Color.Yellow);
table.Columns[1].PadLeft(3).PadRight(6);
table.Centered();
table.Collapse();
table.Title(new TableTitle("[bold red] MY PC INFO [/]"));


Console.WriteLine("\n");


 AnsiConsole.Live(table).AutoClear(false).Overflow(VerticalOverflow.Ellipsis).Cropping(VerticalOverflowCropping.Top).Start(  x =>
{
//row setup

    table.AddRow(new Markup("PC name "), new Markup($"[green] {pcName} [/]"));
    table.AddRow(new Markup("Username"), new Markup($"[green] {username} [/]"));
    table.AddRow(new Markup("Wifi"), new Markup($"[green]{IpAddressInformation.FirstOrDefaultWirelessAddress} [/]"));
    table.AddRow(new Markup("LAN"), new Markup($"[green]{IpAddressInformation.FirstOrDefaultLan} [/]"));

    table.AddRow(new Markup("connected"), new Markup($"[green] {connected} [/]"));

    table.AddRow(new Markup("Uptime"), new Markup($"[green]{RuniningTime.Uptime}[/]"));
    x.Refresh();
    Thread.Sleep(1000);
   
    
    table.AddRow(new Markup("Used Storage Space"),
        new Markup(
            $"[green]Available storage space is {DriveSpace.FormatFileSize(DriveSpace.GetTotalFreeSpace())} [/]"));
    table.AddRow(new Markup("Ram usage"), new Markup($"[green] {ramCounter.NextValue()} MB [/]"));
    table.AddRow(new Markup("CPU usage"), new Markup($"[green] {Math.Round(CupValue(), 1)} % [/]"));
    table.AddRow(new Markup("Window version"), new Markup($"[green] {os} [/]"));
    
    
});

// Console.WriteLine("running time is: {0} ", RuniningTime.TimesRun());


// while (true)
// {
//     Timer timer = null;
//     new Timer(RuniningTime.GetUpTime, null,0, 2000);
//    
// }