using System.IO.Ports;

namespace BarcodeReader.Defaults;

public class DefaultSerialPortBarcodeScanner : IBarcodeReader, IDisposable
{
    public event Action<Barcode> OnBarcodeDetected;
    public event Action<Exception> OnError;
    public bool CanOperate => canOperate;

    private readonly SerialPort _serialPort;
    private bool canOperate = false;
    private readonly IBarcodeValidator? _barcodeValidator;

    public DefaultSerialPortBarcodeScanner(SerialPortConfiguration config, IBarcodeValidator? barcodeValidator = null)
    {
        _barcodeValidator = barcodeValidator;
        _serialPort = new SerialPort(config.PortName, (int)config.PortBaudRate, Parity.None, 8, StopBits.One);
        _serialPort.DataReceived += OnDataReceived;
        TryOpenPort();
    }

    private void TryOpenPort()
    {
        try
        {
            _serialPort.Open();
            canOperate = true;
        }
        catch (Exception ex)
        {
            canOperate = false;
            OnError?.Invoke(ex);
        }
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort port = (SerialPort)sender;
        Thread.Sleep(100);
        var data = port.ReadExisting();
        var isValidBarcode = _barcodeValidator?.Validate(data.Trim()) ?? true;
        if (!isValidBarcode) return;
        Barcode barcode = CreateBarcode(data);
        OnBarcodeDetected?.Invoke(barcode);
    }

    public virtual Barcode CreateBarcode(string data)
    {
        return new(data.Trim());
    }

    public void Dispose()
    {
        try
        {
            _serialPort.DataReceived -= OnDataReceived;
            _serialPort.Close();
            _serialPort.Dispose();
        }
        catch (Exception)
        {
        }
    }
}