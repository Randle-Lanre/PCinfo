// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using PCinfo;
using Spectre.Console;



var pcName= Environment.MachineName;
var username= Environment.UserName;
var os=  Environment.OSVersion.ToString();

#region DNS detials

var connected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

#endregion-DNS dtails for Ip address


#region uptime



 static TimeSpan GetUpTime()
{
    return TimeSpan.FromMilliseconds(GetTickCount64());
}

[DllImport("kernel32")]
extern static UInt64 GetTickCount64();


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
table.AddRow(new Markup("My IP address"), new Markup("[green] 0.0.0.0[/]"));
table.AddRow(new Markup("connected"), new Markup($"[green] {connected} [/]"));
table.AddRow(new Markup("UP Time"), new Markup("[green] 3hrs 45 mins [/]"));
table.AddRow(new Markup("used storage Space"), new Markup("[green]Available storage space is 25% [/]"));
table.AddRow(new Markup("Ram usage"), new Markup("[green] 60% [/]"));
table.AddRow(new Markup("CPU usage"), new Markup("[green] 60% [/]"));
table.AddRow(new Markup("Window version"), new Markup($"[green] {os} [/]"));
table.AddRow(new Markup("test uptime"), new Markup($"{uptime.ToString().TrimEnd('.')}"));


//change table apearance
table.Border(TableBorder.Rounded);
table.BorderColor(Color.Yellow);
table.Columns[1].PadLeft(3).PadRight(6);



AnsiConsole.Write(table);
Console.ReadKey();



