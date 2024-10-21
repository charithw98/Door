import com.zkteco.biometric.FingerprintSensorEx;

public class ZKTLogRetriever {
    private long deviceHandle = 0;

    // Method to connect to device using IP address and port number (default 4370)
    public boolean connectDevice(String deviceIP, int port) {
        try {
            deviceHandle = FingerprintSensorEx.OpenDeviceEx(deviceIP, port);  // Hypothetical connection method
            if (deviceHandle != 0) {
                System.out.println("Successfully connected to device: " + deviceIP);
                return true;
            } else {
                System.out.println("Failed to connect to device.");
                return false;
            }
        } catch (Exception e) {
            System.out.println("Error while trying to connect to device.");
            e.printStackTrace();
            return false;
        }
    }

    // Method to retrieve logs from the device
    public void retrieveLogs() {
        try {
            if (deviceHandle != 0) {
                byte[] logBuffer = new byte[2048];  // Hypothetical buffer for log data
                int[] logLength = new int[1];  // Length of the log data retrieved
                int ret = FingerprintSensorEx.GetLogData(deviceHandle, logBuffer, logLength);  // Hypothetical method
                if (ret == 0 && logLength[0] > 0) {
                    String logData = new String(logBuffer, 0, logLength[0]);
                    System.out.println("Logs retrieved successfully: " + logData);
                } else {
                    System.out.println("No logs available or failed to retrieve logs.");
                }
            } else {
                System.out.println("Device is not connected.");
            }
        } catch (Exception e) {
            System.out.println("Error while retrieving logs.");
            e.printStackTrace();
        }
    }

    // Method to disconnect from the device
    public void disconnectDevice() {
        try {
            if (deviceHandle != 0) {
                FingerprintSensorEx.CloseDeviceEx(deviceHandle);  // Hypothetical method for closing connection
                System.out.println("Disconnected from device.");
            }
        } catch (Exception e) {
            System.out.println("Error while disconnecting from the device.");
            e.printStackTrace();
        }
    }

    public static void main(String[] args) {
        ZKTLogRetriever logRetriever = new ZKTLogRetriever();

        // IP and Port of your ZKTeco device (set to actual values)
        String deviceIP = "10.101.13.100";
        int port = 4370;

        // Connect to the device and retrieve logs
        if (logRetriever.connectDevice(deviceIP, port)) {
            logRetriever.retrieveLogs();  // Attempt to retrieve logs
            logRetriever.disconnectDevice();  // Disconnect after retrieving logs
        }
    }
}
