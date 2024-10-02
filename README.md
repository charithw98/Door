using System;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace FingerprintLogRetriever
{
    class Program
    {
        // Import the ActiveX control
        [ComImport]
        [Guid("Your-ActiveX-Control-GUID")] // Replace with actual GUID from SDK
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        public interface IBiokeyControl
        {
            bool InitEngine();
            bool EngineValid { get; }
            void SetDeviceIP(string ip);
            byte[] GetTemplate();
            void CloseDevice();
        }

        static void Main(string[] args)
        {
            IBiokeyControl fp = new BiokeyControl(); // Adjust based on actual class name

            // Initialize the fingerprint engine
            if (!fp.InitEngine())
            {
                Console.WriteLine("Failed to initialize fingerprint engine.");
                return;
            }

            // Set device IP
            string deviceIP = "10.101.13.100";
            fp.SetDeviceIP(deviceIP);

            if (!fp.EngineValid)
            {
                Console.WriteLine("Fingerprint device is not valid.");
                return;
            }

            // Retrieve fingerprint logs
            byte[] logData = fp.GetTemplate();
            if (logData == null || logData.Length == 0)
            {
                Console.WriteLine("No logs available.");
                return;
            }

            // Print log data to console
            Console.WriteLine("Logs retrieved successfully:");
            foreach (var log in logData)
            {
                Console.WriteLine(log);
            }

            // Close device connection
            fp.CloseDevice();
        }
    }
}
