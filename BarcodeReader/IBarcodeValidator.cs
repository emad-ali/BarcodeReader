namespace BarcodeReader
{
    public interface IBarcodeValidator
    {
        bool Validate<T>(T data);
    }
}