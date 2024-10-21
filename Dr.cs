import com.zkteco.biometric.FingerprintSensorEx;

public class ZKTLogRetriever {
    private long deviceHandle = 0;

    // Connect to device by IP and port (default is 4370 for ZKTeco devices)
    public boolean connectDevice(String deviceIP, int port) {
        deviceHandle = FingerprintSensorEx.OpenDeviceEx(deviceIP, port);
        if (deviceHandle != 0) {
            System.out.println("Connected to device: " + deviceIP);
            return true;
        } else {
            System.out.println("Failed to connect to device.");
            return false;
        }
    }

    // Retrieve attendance logs
    public void retrieveLogs() {
        if (deviceHandle != 0) {
            byte[] buffer = new byte[2048]; // Adjust buffer size as needed
            int[] logLen = new int[1];
            int ret = FingerprintSensorEx.GetLogData(deviceHandle, buffer, logLen);
            if (ret == 0 && logLen[0] > 0) {
                String logData = new String(buffer, 0, logLen[0]);
                System.out.println("Retrieved logs: " + logData);
            } else {
                System.out.println("Failed to retrieve logs or no logs available.");
            }
        } else {
            System.out.println("Device is not connected.");
        }
    }

    // Disconnect from device
    public void disconnectDevice() {
        if (deviceHandle != 0) {
            FingerprintSensorEx.CloseDeviceEx(deviceHandle);
            System.out.println("Disconnected from device.");
        }
    }

    public static void main(String[] args) {
        ZKTLogRetriever logRetriever = new ZKTLogRetriever();
        // Connect to your device with the provided IP (10.101.13.100) and default port (4370)
        if (logRetriever.connectDevice("10.101.13.100", 4370)) {
            logRetriever.retrieveLogs();  // Retrieve logs
            logRetriever.disconnectDevice();  // Disconnect from the device after retrieving logs
        }
    }
}
