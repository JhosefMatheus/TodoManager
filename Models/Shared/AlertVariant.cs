namespace TodoManager.Models.Shared;

public class AlertVariant
{
    private string Value { get; }

    public static AlertVariant Success
    {
        get => new AlertVariant("success");
    }

    public static AlertVariant Info
    {
        get => new AlertVariant("info");
    }

    public static AlertVariant Error
    {
        get => new AlertVariant("error");
    }

    public static AlertVariant Warning
    {
        get => new AlertVariant("warning");
    }

    public static AlertVariant Default
    {
        get => new AlertVariant("default");
    }

    private AlertVariant(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}