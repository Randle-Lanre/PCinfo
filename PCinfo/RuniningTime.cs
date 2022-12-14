using System.Runtime.InteropServices;

namespace PCinfo;

public static class RuniningTime
{
    public static string Uptime = GetUpTime();

    private static string GetUpTime()
    {
        var timeInMilliseconds = TimeSpan.FromMilliseconds(GetTickCount64());
        var hr = timeInMilliseconds.Hours;
        var minutes = timeInMilliseconds.Minutes;
        var seconds = timeInMilliseconds.Seconds;
        var newDateFormat = $"{hr}:{minutes}:{seconds}";

        return newDateFormat;
    }


    [DllImport("kernel32")]
    private static extern ulong GetTickCount64();
}