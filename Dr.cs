import com.zkteco.biometric.FingerprintSensorEx;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;

public class ZKTLogRetriever {
    private long deviceHandle = 0;

    // MySQL connection parameters
    private static final String DB_URL = "jdbc:mysql://localhost:3306/attendance_logs";
    private static final String USER = "root";  // Replace with your MySQL username
    private static final String PASS = "password";  // Replace with your MySQL password

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
                saveLogToDB(logData);  // Save logs to database
            } else {
                System.out.println("Failed to retrieve logs or no logs available.");
            }
        } else {
            System.out.println("Device is not connected.");
        }
    }

    // Save logs to the database
    public void saveLogToDB(String logData) {
        try (Connection conn = DriverManager.getConnection(DB_URL, USER, PASS)) {
            String sql = "INSERT INTO logs (log_data) VALUES (?)";
            PreparedStatement pstmt = conn.prepareStatement(sql);
            pstmt.setString(1, logData);
            pstmt.executeUpdate();
            System.out.println("Log saved to database.");
        } catch (Exception e) {
            e.printStackTrace();
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
        if (logRetriever.connectDevice("10.101.13.100", 4370)) {  // Your device's IP
            logRetriever.retrieveLogs();
            logRetriever.disconnectDevice();
        }
    }
}
