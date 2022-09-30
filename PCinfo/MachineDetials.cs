namespace PCinfo;

public static class MachineDetials
{
    private static string? _pcName;

    public static string pcName
    {
        get => _pcName;
        set => value=_pcName = Environment.MachineName;
    }


    private static string? _username;

    public static string? username
    {
        get => _username;
        set => _username=value = Environment.UserName;
    }

    private static string? _os;

    public static string os
    {
        get => _os;
        set => _os = value= Environment.OSVersion.ToString();
    }
}