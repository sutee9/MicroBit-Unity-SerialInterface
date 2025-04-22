using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;
using UnityEngine.Events;

public class SerialReader : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "/dev/cu.usbmodem1102";
    public int baudRate = 115200;

    [Space]
    public bool log = false;

    private SerialPort serialPort;
    private Thread readThread;
    private bool isRunning;

    public UnityEvent<string> OnLineReceived;

    // Thread-safe queue to pass data to main thread
    private ConcurrentQueue<string> serialQueue = new ConcurrentQueue<string>();

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 500;  // Adjust as needed
            serialPort.Open();

            isRunning = true;
            readThread = new Thread(ReadSerial);
            readThread.Start();

            Debug.Log($"Opened serial port {portName} at {baudRate} baud.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error opening serial port: {e.Message}");
        }
    }

    void ReadSerial()
    {
        while (isRunning && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string line = serialPort.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    serialQueue.Enqueue(line);  // Pass to main thread
                }
            }
            catch (System.TimeoutException) { }
            catch (System.Exception e)
            {
                Debug.LogError($"Serial read error: {e.Message}");
            }
        }
    }

    void Update()
    {
        // Drain the queue and log on the main thread
        while (serialQueue.TryDequeue(out string line))
        {
            OnLineReceived.Invoke(line);
            if (log){
                Debug.Log($"Serial: {line}");
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;

        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed.");
        }
    }
}