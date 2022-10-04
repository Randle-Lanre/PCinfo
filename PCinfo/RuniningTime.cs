using System.Runtime.InteropServices;

namespace PCinfo;

public static class RuniningTime
{
    // public static readonly string Uptime = GetUpTime();
    public static  string Uptime= GetUpTime();

    public static string GetUpTime()
    {
        var timeInMilliseconds = TimeSpan.FromMilliseconds(GetTickCount64());
        var hr = timeInMilliseconds.Hours;
        var minutes = timeInMilliseconds.Minutes;
        var seconds = timeInMilliseconds.Seconds;
        var newDateFormat = $"{hr}:{minutes}:{seconds}";

        return newDateFormat;
        // Thread.Sleep(500);
        // Uptime = newDateFormat;
        // Console.WriteLine("method calls{0}", Uptime + "\n");

    }

    


    [DllImport("kernel32")]
    private static extern ulong GetTickCount64();

   
}