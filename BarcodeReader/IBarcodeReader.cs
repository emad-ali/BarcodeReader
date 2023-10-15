namespace BarcodeReader;

public interface IBarcodeReader
{
    public event Action<Barcode> OnBarcodeDetected;
    public event Action<Exception> OnError;
}
