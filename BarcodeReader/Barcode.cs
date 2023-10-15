namespace BarcodeReader;

public class Barcode
{
    public Barcode(string value)
    {
        Value = value;

    }
    public string Value { get; }
    public int Length => Value?.Length ?? 0;
}