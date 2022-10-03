namespace PCinfo;

public static class DriveSpace
{
    public static long GetTotalFreeSpace()
    {
        foreach (var drive in DriveInfo.GetDrives())
            if (drive.IsReady && drive.Name == "C:\\")
                return drive.AvailableFreeSpace;

        return -1;
    }

    public static string FormatFileSize(long bytes)
    {
        var unit = 1024;
        if (bytes < unit) return $"{bytes} B";

        var exp = (int)(Math.Log(bytes) / Math.Log(unit));
        return $"{bytes / Math.Pow(unit, exp):F2} {"KMGTPE"[exp - 1]}B";
    }
}