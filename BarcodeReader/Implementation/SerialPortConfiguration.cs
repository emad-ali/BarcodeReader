namespace BarcodeReader.Defaults;

public class SerialPortConfiguration
{
    public SerialPortConfiguration(string portName, BaudRate portBaudRate)
    {
        PortName = portName.Trim().ToUpper();
        PortBaudRate = portBaudRate;
    }
    public string PortName { get; }
    public BaudRate PortBaudRate { get; }
}