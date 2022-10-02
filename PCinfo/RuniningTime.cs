using System.Runtime.InteropServices;

namespace PCinfo;

public static class RuniningTime
{
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




  public static readonly string Uptime = GetUpTime();


    
}