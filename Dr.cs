using System;
using System.Collections.Generic;
using PullSDK_core;  // Ensure this is the correct namespace

class ZKTecoDeviceConnection
{
    static void Main(string[] args)
    {
        string deviceIp = "10.101.13.100";  // Your device's IP address
        int port = 4370;  // The default port for ZKTeco devices
        int password = 123456;  // Device password
        int timeout = 5000;  // Connection timeout in milliseconds

        // Create an instance of the device class
        AccessPanel device = new AccessPanel();

        // Attempt to connect to the device
        if (!device.Connect(deviceIp, port, password, timeout))
        {
            Console.WriteLine($"Failed to connect to the device at {deviceIp}:{port}.");
            return;
        }

        Console.WriteLine($"Successfully connected to the device at {deviceIp}:{port}.");

        // Read users from the device
        List<User> users = device.ReadUsers();
        if (users == null)
        {
            Console.WriteLine("Failed to read users from the device.");
            return;
        }

        Console.WriteLine($"Successfully read {users.Count} users from the device.");

        // Open door 1 for 5 seconds (for access control systems)
        if (!device.OpenDoor(1, 5))
        {
            Console.WriteLine("Failed to open door.");
            return;
        }

        Console.WriteLine("Door opened successfully.");

        // After operations, disconnect from the device
        device.Disconnect();
        Console.WriteLine("Disconnected from the device.");
    }
}
