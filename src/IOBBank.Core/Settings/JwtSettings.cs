namespace IOBBank.Core.Settings;

public class JwtSettings
{
    public static string Section => nameof(JwtSettings);

    public string? SecretKey { get; set; }
}
