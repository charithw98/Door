import com.zkteco.biometric.FingerprintSensorEx;

public class ZKFPDemo {

    private long deviceHandle = 0;
    private String deviceIP = "10.101.13.100";  // Replace with the actual IP of your device
    private int port = 4370;                    // Default ZKTeco port

    public void connectDevice() {
        try {
            System.out.println("Connecting to device at IP: " + deviceIP);

            // Open the device connection
            deviceHandle = FingerprintSensorEx.OpenDeviceEx(deviceIP, port);

            if (deviceHandle != 0) {
                System.out.println("Successfully connected to device at IP: " + deviceIP);

                // Call the method to get logs after successful connection
                getLogs();
            } else {
                System.out.println("Failed to connect to the device.");
            }

        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
            e.printStackTrace();
        }
    }

    public void getLogs() {
        try {
            System.out.println("Retrieving logs...");

            byte[] buffer = new byte[1024];  // Adjust buffer size as needed
            int logLen = buffer.length;
            int ret = FingerprintSensorEx.GetLogData(deviceHandle, buffer, logLen);

            if (ret == 0) {
                System.out.println("Logs retrieved successfully.");

                // Convert the byte buffer to a string for demonstration
                String logs = new String(buffer, "UTF-8");
                System.out.println("Logs: " + logs);
            } else {
                System.out.println("Failed to retrieve logs. Error code: " + ret);
            }

        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
            e.printStackTrace();
        } finally {
            // Close the device connection after retrieving logs
            FingerprintSensorEx.CloseDeviceEx(deviceHandle);
            System.out.println("Device connection closed.");
        }
    }

    public static void main(String[] args) {
        ZKFPDemo demo = new ZKFPDemo();
        demo.connectDevice();
    }
}
